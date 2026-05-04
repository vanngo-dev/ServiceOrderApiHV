using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceOrderApi.Models;

public class ServiceOrder
{
    public int Id {get;set;}

    public int CustomerId {get;set;}

    public int EquipmentId {get;set;}

    [StringLength(100)]
    [Required]
    public string TechnicianName {get;set;} = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Description {get;set;} = string.Empty;


    [Required]
    [StringLength(20)]
    public string Status {get;set;} = "Pending";

    public DateTime ServiceDate {get;set;}

    [Column(TypeName = "decimal(10,2)")]
    public decimal Cost {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

    public Customer Customer {get;set;} = null!;
    public Equipment Equipment {get;set;} = null!;
}