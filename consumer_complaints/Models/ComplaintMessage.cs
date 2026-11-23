using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("complaint_messages_tb")]
public class ComplaintMessage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("message_id")]
    public int MessageId { get; set; }
    [Column("complaint_id")]
    public int ComplaintId { get; set; }
    [ForeignKey(nameof(ComplaintId))]
    public Complaint Complaint { get; set; } = null!;
    [Column("sender_type")]
    public SenderType SenderType { get; set; }

    [Column("sender_user_id")]
    public int? SenderUserId { get; set; }

    [ForeignKey(nameof(SenderUserId))]
    public AppUser? SenderUser { get; set; }

    [Required]
    [StringLength(2000)]
    [Column("body")]
    public string Body { get; set; } = string.Empty;
    [Column("is_internal")]
    public bool IsInternal { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Column("read_by_customer_at")]
    public DateTime? ReadByCustomerAt { get; set; }

    [Column("read_by_staff_at")]
    public DateTime? ReadByStaffAt { get; set; }




}