using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("complaint_assignments_tb")]
public class ComplaintAssignment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("assignment_id")]
    public int AssignmentId { get; set; }

    [Column("complaint_id")]
    public int ComplaintId { get; set; }

    [ForeignKey(nameof(ComplaintId))]
    public Complaint? Complaint { get; set; }


    [Column("assigned_to_user_id")]
    public int AssignedToUserId { get; set; }

    [ForeignKey(nameof(AssignedToUserId))]
    public AppUser? AssignedToUser { get; set; }

    [Column("assigned_by_user_id")]
    public int? AssignedByUserId { get; set; }

    [ForeignKey(nameof(AssignedByUserId))]
    public AppUser? AssignedByUser { get; set; }


    [Column("assigned_at")]
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    [Column("accepted_at")]
    public DateTime? AcceptedAt { get; set; }

    [Column("ended_at")]
    public DateTime? EndedAt { get; set; }


    [Column("status")]
    public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;

    [StringLength(500)]
    [Column("delay_reason")]
    public string? DelayReason { get; set; }

    [StringLength(1000)]
    [Column("notes")]
    public string? Notes { get; set; }


}
