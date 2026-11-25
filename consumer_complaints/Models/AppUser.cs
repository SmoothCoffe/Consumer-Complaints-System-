using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("users_tb")]
public class AppUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("username")]
    public string Username { get; set; }=string.Empty;

    [Required]
    [StringLength(255)]
    [Column("password_hash")]
    public string PasswordHash { get; set; }=string.Empty;

    [Required]
    [StringLength(150)]
    [Column("full_name")]
    public string FullName { get; set; }=string.Empty;

    [StringLength(150)]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }

    [StringLength(25)]
    [Column("phone")]
    public string? Phone { get; set; }

    [Column("department_id")]
    public int? DepartmentId { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    public Department? Department { get; set; }

    [Column("role")]
    public UserRole Role { get; set; } = UserRole.Staff;

    [Column("is_assignable")]
    public bool IsAssignable { get; set; } = true;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("last_login_at")]
    public DateTime? LastLoginAt { get; set; }

    [Column("failed_login_count")]
    public int FailedLoginCount { get; set; }

    [Column("lockout_end")]
    public DateTime? LockoutEnd { get; set; }

    public ICollection<ComplaintAssignment> AssignmentsReceived { get; set; }= new List<ComplaintAssignment>();
    public ICollection<ComplaintAssignment> AssignmentsMade { get; set; }= new List<ComplaintAssignment>();
    public ICollection<ComplaintMessage> Messages { get; set; } = new List<ComplaintMessage>();
    public ICollection<EscalationLog> EscalationsReceived { get; set; }= new List<EscalationLog>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}