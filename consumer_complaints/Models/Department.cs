using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumer_complaints.Models;

[Table("departments_tb")]
public class Department
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("department_id")]
    public int DepartmentId { get; set; }

    [StringLength(20)]
    [Column("code")]
    public string? Code { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Column("description")]
    public string? Description { get; set; }

    [Column("manager_id")]
    public int? ManagerId { get; set; }
    [ForeignKey(nameof(ManagerId))]
    public AppUser? Manager { get; set; }

    [Column("escalation_manager_id")]
    public int? EscalationManagerId { get; set; }

    [ForeignKey(nameof(EscalationManagerId))]
    public AppUser? EscalationManager { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    public ICollection<SlaConfig> SlaConfigs { get; set; } = new List<SlaConfig>();
    public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

}