using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumer_complaints.Models;

[Table("complaints_assignment_tb")]
public class Assignment
{
    [Key]
    [StringLength(6)]
    [Column("ref_num")]
    public string ReferenceNumber { get; set; } = string.Empty;

    [StringLength(10)]
    [Column("assign_date")]
    public string? AssignDate { get; set; }

    [StringLength(50)]
    [Column("assigned_to_user_id")]
    public string? AssignedToUserId { get; set; }

    [ForeignKey(nameof(AssignedToUserId))]
    public AppUser? AssignedToUser { get; set; }

    [StringLength(35)]
    [Column("delay_reason")]
    public string? DelayReason { get; set; }

    [StringLength(8)]
    [Column("status")]
    public string? Status { get; set; } = "PENDING";

    [ForeignKey(nameof(ReferenceNumber))]
    public NewComplaint Complaint { get; set; } = null!;
}