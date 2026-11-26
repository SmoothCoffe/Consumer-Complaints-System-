namespace consumer_complaints.Services;

public interface IEmailSender
{
    Task SendAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default);
}
public sealed class LoggingEmailSender : IEmailSender
{
    private readonly ILogger<LoggingEmailSender> _logger;

    public LoggingEmailSender(ILogger<LoggingEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "[EMAIL-DEV] To={To} | Subject={Subject} | BodyLength={Length}",
            toEmail, subject, htmlBody.Length);

        return Task.CompletedTask;
    }
}