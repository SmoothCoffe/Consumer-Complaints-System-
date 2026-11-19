using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace consumer_complaints.Models;

[Table("complaints_consumers_tb")]
public class Consumer
{
    [Key]
    [StringLength(7)]
    [Column("meter_num")]
    public string MeterNumber { get; set; }= string.Empty;


    [StringLength(15)]
    [Column("name")]
    public string Name { get; set; }= string.Empty;

    [StringLength(12)]
    [Column("phone_num")]
    public string PhoneNumber { get; set; }= string.Empty;

    [StringLength(10)]
    [Column("area")]
    public string Area { get; set; }= string.Empty;

    [StringLength(12)]
    [Column("city")]
    public string City { get; set; }= string.Empty;

    [StringLength(12)]
    [Column("state")]
    public string State { get; set; }= string.Empty;


    public ICollection<NewComplaint> Complaints { get; set; } = new List<NewComplaint>();

}