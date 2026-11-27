using System.Net;
using consumer_complaints.Models;

namespace consumer_complaints.Services;

public static class EmailTemplates
{
    public static (string Subject, string BodyHtml) ComplaintReceived(
        Complaint complaint, AppSettings app)
    {
        var body = Layout(app, "Your complaint has been received.", $"""
            <p>Hello {Html(complaint.Customer?.FullName ?? "Our Dear Customer")}،</p>
            <p>We have successfully received your complaint. You can track its status using the following details::</p>
            {Row("Reference Number", $"<strong>{Html(complaint.ReferenceNumber)}</strong>")}
            {Row("Subject", Html(complaint.Subject))}
            {Row("Status", Html(StatusLabel(complaint.Status)))}
            {(complaint.SlaDeadline is null ? "" : Row("Estimated Time", Html(FormatUtc(complaint.SlaDeadline.Value))))}
            <p>To view the details, open the tracking page and enter your reference number and phone number:</p>
            <p><a href="{Html(app.PublicBaseUrl.TrimEnd('/'))}/Track">Complaint Follow Up</a></p>
            """);

        return ($"Complaint Receive {complaint.ReferenceNumber}", body);
    }

    public static (string Subject, string BodyHtml) InfoRequested(
        Complaint complaint, AppSettings app, string reason)
    {
        var body = Layout(app, "We Need More information", $"""
            <p>Hello {Html(complaint.Customer?.FullName ?? "Our Dear Customer")}،</p>
            <p>for processing your complaint <strong>{Html(complaint.ReferenceNumber)}</strong> we need this:</p>
            <blockquote>{Html(reason)}</blockquote>
            <p>You can add a response from the tracking page. The time spent waiting for your response will not be counted toward the processing time..</p>
            <p><a href="{Html(app.PublicBaseUrl.TrimEnd('/'))}/Track">Adding information</a></p>
            """);

        return ($"Additional information required — {complaint.ReferenceNumber}", body);
    }

    public static (string Subject, string BodyHtml) ComplaintClosed(
        Complaint complaint, AppSettings app, string? resolutionNotes)
    {
        var body = Layout(app, "Your complaint has been closed.", $"""
            <p>Hello {Html(complaint.Customer?.FullName ?? "Our Dear Customer")}،</p>
            <p>Your complaint has been closed. <strong>{Html(complaint.ReferenceNumber)}</strong>.</p>
            {(string.IsNullOrWhiteSpace(resolutionNotes) ? "" : Row("Process Summary", Html(resolutionNotes)))}
            <p>Thank you for contacting us.</p>
            """);

        return ($"Closing the complaint {complaint.ReferenceNumber}", body);
    }


    private static string Layout(AppSettings app, string title, string inner) => $"""
        <div dir="rtl" style="font-family:Tahoma,Arial,sans-serif;font-size:14px;color:#222;line-height:1.7">
          <h2 style="color:#0d6efd;margin:0 0 12px">{Html(title)}</h2>
          {inner}
          <hr style="border:none;border-top:1px solid #ddd;margin:20px 0" />
          <p style="color:#777;font-size:12px">
            This is an automated message from {Html(app.CompanyName)} — Don't reply to it.
          </p>
        </div>
        """;

    private static string Row(string label, string valueHtml) =>
        $"""<p style="margin:4px 0"><span style="color:#666">{Html(label)}:</span> {valueHtml}</p>""";

    private static string Html(string? value) => WebUtility.HtmlEncode(value ?? string.Empty);

    private static string FormatUtc(DateTime value) =>
        value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm") + " UTC";

    private static string StatusLabel(ComplaintStatus status) => status switch
    {
        ComplaintStatus.New => "New",
        ComplaintStatus.Assigned => "Assgined",
        ComplaintStatus.InProgress => "In Process",
        ComplaintStatus.NeedsCustomerInfo => "Waiting for Details",
        ComplaintStatus.Escalated => "Escalated",
        ComplaintStatus.Closed => "Closed",
        ComplaintStatus.Cancelled => "Canceled",
        _ => status.ToString()
    };
}