using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("complaints_tb")]
public class Complaint
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("complaint_id")]
    public int ComplaintId { get; set; }

    [Required]
    [StringLength(20)]
    [Column("reference_number")]
    public string ReferenceNumber { get; set; } = string.Empty;

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [Column("department_id")]
    public int DepartmentId { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    public Department Department { get; set; } = null!;

    [Column("created_by_user_id")]
    public int? CreatedByUserId { get; set; }

    [ForeignKey(nameof(CreatedByUserId))]
    public AppUser? CreatedByUser { get; set; }

    [Required]
    [StringLength(200)]
    [Column("subject")]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [StringLength(200)]
    [Column("related_item")]
    public string? RelatedItem { get; set; }

    [Column("status")]
    public ComplaintStatus Status { get; set; } = ComplaintStatus.New;

    [Column("reported_at")]
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

    [Column("sla_config_id")]
    public int? SlaConfigId { get; set; }

    [ForeignKey(nameof(SlaConfigId))]
    public SlaConfig? SlaConfig { get; set; }

    [Column("sla_deadline")]
    public DateTime? SlaDeadline { get; set; }

    [Column("sla_paused_minutes")]
    public int SlaPausedMinutes { get; set; }

    [Column("info_requested_at")]
    public DateTime? InfoRequestedAt { get; set; }

    [Column("customer_responded_at")]
    public DateTime? CustomerRespondedAt { get; set; }
    [Column("escalation_level")]
    public byte EscalationLevel { get; set; }

    [Column("current_escalated_to_user_id")]
    public int? CurrentEscalatedToUserId { get; set; }

    [ForeignKey(nameof(CurrentEscalatedToUserId))]
    public AppUser? CurrentEscalatedToUser { get; set; }

    [Column("escalated_at")]
    public DateTime? EscalatedAt { get; set; }

    [Column("closed_at")]
    public DateTime? ClosedAt { get; set; }

    [Column("closed_by_user_id")]
    public int? ClosedByUserId { get; set; }

    [ForeignKey(nameof(ClosedByUserId))]
    public AppUser? ClosedByUser { get; set; }
    [StringLength(20)]
    [Column("closure_type")]
    public ClosureType? ClosureType { get; set; }

    [StringLength(2000)]
    [Column("resolution_notes")]
    public string? ResolutionNotes { get; set; }

    [Column("time_taken_minutes")]
    public int? TimeTakenMinutes { get; set; }

    [Timestamp]
    [Column("row_version")]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ComplaintAssignment> Assignments { get; set; } = new List<ComplaintAssignment>();
    public ICollection<ComplaintMessage> Messages { get; set; } = new List<ComplaintMessage>();
    public ICollection<EscalationLog> EscalationLogs { get; set; } = new List<EscalationLog>();

}