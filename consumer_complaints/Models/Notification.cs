using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumer_complaints.Models;

[Table("notifications_tb")]
public class Notification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("notification_id")]
    public int NotificationId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public AppUser User { get; set; } = null!;

    [Required]
    [StringLength(200)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    [Column("message")]
    public string Message { get; set; } = string.Empty;

    [Column("type")]
    public NotificationType Type { get; set; } = NotificationType.System;

    [Column("related_complaint_id")]
    public int? RelatedComplaintId { get; set; }

    [ForeignKey(nameof(RelatedComplaintId))]
    public Complaint? RelatedComplaint { get; set; }

    [Column("is_read")]
    public bool IsRead { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("read_at")]
    public DateTime? ReadAt { get; set; }
}