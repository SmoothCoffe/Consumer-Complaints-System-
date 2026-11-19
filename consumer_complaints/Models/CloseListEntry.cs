using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("complaints_close_list_tb")]
public class CloseListEntry
{
    [Key]
    [StringLength(6)]
    [Column("ref_num")]
    public string ReferenceNumber { get; set; } = string.Empty;

    [StringLength(10)]
    [Column("close_date")]
    public string? CloseDate { get; set; }

    [StringLength(8)]
    [Column("close_time")]
    public string? CloseTime { get; set; }

    [StringLength(3)]
    [Column("time_taken_in_hrs")]
    public string TimeTakenInMinutes { get; set; } = string.Empty;

    [ForeignKey(nameof(ReferenceNumber))]
    public NewComplaint Complaint { get; set; }= null!;

}
