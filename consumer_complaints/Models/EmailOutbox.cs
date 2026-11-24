using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("email_outbox_tb")]
public class EmailOutbox

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("email_id")]
    public long EmailId { get; set; }

    [Required]
    [StringLength(150)]
    [Column("to_email")]
    public string ToEmail { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [Column("subject")]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [Column("body_html")]
    public string BodyHtml { get; set; } = string.Empty;


    [Column("template")]
    public EmailTemplate Template { get; set; }

    [Column("related_complaint_id")]
    public int? RelatedComplaintId { get; set; }
    [ForeignKey(nameof(RelatedComplaintId))]
    public Complaint? RelatedComplaint { get; set; }


    [Column("status")]
    public EmailStatus Status { get; set; } = EmailStatus.Pending;

    [Column("attempts")]
    public int Attempts { get; set; }

    [StringLength(500)]
    [Column("last_error")]
    public string? LastError { get; set; }

    [Column("next_attempt_at")]
    public DateTime? NextAttemptAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("sent_at")]
    public DateTime? SentAt { get; set; }


}

