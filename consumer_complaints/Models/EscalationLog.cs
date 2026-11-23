using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;
[Table("escalation_logs_tb")]
public class EscalationLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("escalation_id")]
    public int EscalationId { get; set; }
    [Column("complaint_id")]
    [ForeignKey(nameof(ComplaintId))]
    public Complaint Complaint { get; set; } = null!;
    public int ComplaintId { get; set; }
    [Column("from_user_id")]
    public int? FromUserId { get; set; }
    [ForeignKey(nameof(FromUserId))]
    public AppUser? FromUser { get; set; }
    [Column("to_user_id")]
    public int ToUserId { get; set; }
    [ForeignKey(nameof(ToUserId))]
    public AppUser ToUser { get; set; } = null!;
    [Column("level")]
    public byte Level { get; set; }

    [Required]
    [StringLength(500)]
    [Column("reason")]
    public string Reason { get; set; } = string.Empty;

    [Column("escalated_at")]
    public DateTime EscalatedAt { get; set; } = DateTime.UtcNow;

    [Column("acknowledged_at")]
    public DateTime? AcknowledgedAt { get; set; }

    [Column("acknowledged_by_user_id")]
    public int? AcknowledgedByUserId { get; set; }

    [ForeignKey(nameof(AcknowledgedByUserId))]
    public AppUser? AcknowledgedByUser { get; set; }
}