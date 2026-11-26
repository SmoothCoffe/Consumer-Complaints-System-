namespace consumer_complaints.Services;

public sealed class EmailSettings
{
    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; } = 587;
    public string SmtpUser { get; set; } = string.Empty;
    public string SmtpPass { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = "Complaints System";
    public bool EnableSsl { get; set; } = true;
    public bool SilentFail { get; set; } = true;

    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(SmtpHost)
        && SmtpPort > 0
        && !string.IsNullOrWhiteSpace(SmtpUser)
        && !string.IsNullOrWhiteSpace(SmtpPass)
        && !string.IsNullOrWhiteSpace(FromEmail);
}
