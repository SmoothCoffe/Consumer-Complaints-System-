using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumer_complaints.Models;

[Table("customers_tb")]
public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(150)]
    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(25)]
    [Column("phone")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    [Column("phone_normalized")]
    public string PhoneNormalized { get; set; } = string.Empty;

    [StringLength(200)]
    [Column("address")]
    public string? Address { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}