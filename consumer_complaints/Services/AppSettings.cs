namespace consumer_complaints.Services;

public sealed class AppSettings
{
    public string PublicBaseUrl { get; set; } = "https://localhost:5001";
    public int TrackingSessionMinutes { get; set; } = 30;
    public string CompanyName { get; set; } = "Customer Service";
}