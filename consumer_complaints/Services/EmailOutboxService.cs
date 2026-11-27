using consumer_complaints.Data;
using consumer_complaints.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace consumer_complaints.Services;

public sealed class EmailOutboxService
{
    public const int MaxAttempts = 5;

    private readonly ComplaintsDbContext _db;
    private readonly IEmailSender _sender;
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailOutboxService> _logger;

    public EmailOutboxService(
        ComplaintsDbContext db,
        IEmailSender sender,
        IOptions<EmailSettings> settings,
        ILogger<EmailOutboxService> logger)
    {
        _db = db;
        _sender = sender;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<EmailOutbox> EnqueueAsync(
        string toEmail,
        EmailTemplate template,
        string subject,
        string bodyHtml,
        int? complaintId = null,
        CancellationToken cancellationToken = default)
    {
        var item = new EmailOutbox
        {
            ToEmail = toEmail,
            Template = template,
            Subject = subject,
            BodyHtml = bodyHtml,
            RelatedComplaintId = complaintId,
            Status = EmailStatus.Pending,
            Attempts = 0,
            CreatedAt = DateTime.UtcNow,
            NextAttemptAt = DateTime.UtcNow
        };

        _db.EmailOutbox.Add(item);
        await _db.SaveChangesAsync(cancellationToken);

        return item;
    }

    public async Task<int> ProcessPendingAsync(
        int batchSize = 20,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var batch = await _db.EmailOutbox
            .Where(e => e.Status == EmailStatus.Pending
                        && e.Attempts < MaxAttempts
                        && (e.NextAttemptAt == null || e.NextAttemptAt <= now))
            .OrderBy(e => e.EmailId)
            .Take(batchSize)
            .ToListAsync(cancellationToken);

        if (batch.Count == 0) return 0;

        var sentCount = 0;

        foreach (var email in batch)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await _sender.SendAsync(
                    email.ToEmail, email.Subject, email.BodyHtml, cancellationToken);

                email.Status = EmailStatus.Sent;
                email.SentAt = DateTime.UtcNow;
                email.LastError = null;
                email.NextAttemptAt = null;
                sentCount++;
            }
            catch (Exception ex)
            {
                email.Attempts++;
                email.LastError = Truncate(ex.Message, 500);
                email.Status = email.Attempts >= MaxAttempts
                    ? EmailStatus.Failed
                    : EmailStatus.Pending;
                email.NextAttemptAt = DateTime.UtcNow.Add(Backoff(email.Attempts));

                var level = _settings.SilentFail ? LogLevel.Warning : LogLevel.Error;
                _logger.Log(level, ex,
                    "Mail delivery failed {EmailId} To {To} (Try {Attempts}/{Max})",
                    email.EmailId, email.ToEmail, email.Attempts, MaxAttempts);
            }
        }

        await _db.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Outbox: {Sent}/{Total} Sent in this batch",
            sentCount, batch.Count);

        return sentCount;
    }

    private static TimeSpan Backoff(int attempts)
    {
        // 2, 4, 8, 16, 32 minutes — up to a maximum of one hour
        var minutes = Math.Min(60, Math.Pow(2, Math.Max(1, attempts)));
        return TimeSpan.FromMinutes(minutes);
    }

    private static string Truncate(string? value, int max)
        => value is null ? string.Empty
         : value.Length <= max ? value
         : value[..max];
}