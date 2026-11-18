using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumer_complaints.Models;

[Table("complaints_users_tb")]
public class AppUser
{
    [Key]
    [StringLength(50)]
    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [StringLength(100)]
    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(100)]
    [Column("email")]
    public string? Email { get; set; }

    [StringLength(12)]
    [Column("passwd")]
    public string Password { get; set; } = string.Empty;

    [StringLength(20)]
    [Column("role")]
    public string Role { get; set; } = "Staff";
    [Column("department_id")]
    public int? DepartmentId { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    public Department? Department { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("incentive_earned")]
    [StringLength(6)]
    public string IncentiveEarned { get; set; } = "0";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [NotMapped]
    public bool IsAdmin => Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);

    [NotMapped]
    public bool IsManager => Role.Equals("Manager", StringComparison.OrdinalIgnoreCase);

    [NotMapped]
    public bool IsStaff => Role.Equals("Staff", StringComparison.OrdinalIgnoreCase);

    [NotMapped]
    public bool IsOldStyleManager => int.TryParse(UserId, out var n) && n <= 100;
}
