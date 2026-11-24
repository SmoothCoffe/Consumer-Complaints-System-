using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("sla_configs_tb")]
public class SlaConfig
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("sla_config_id")]
    public int SlaConfigId { get; set; }

    [Column("department_id")]
    public int DepartmentId { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    public Department? Department { get; set; }

    [Column("target_resolution_minutes")]
    public int TargetResolutionMinutes { get; set; } = 2880;

    [Column("escalation_grace_minutes")]
    public int EscalationGraceMinutes { get; set; } = 0;

    [Column("max_escalation_level")]
    public byte MaxEscalationLevel { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}