using System.ComponentModel.DataAnnotations;

namespace ServiceOrderApi.Models;

public class Equipment
{
    public int Id {get;set;}
    public int CustomerId {get;set;}

    [StringLength(50)]
    [Required]
    public string SerialNumber {get;set;} = string.Empty;

    [Required]
    [StringLength(100)]
    public string Model {get;set;} = string.Empty;

    [Required]
    [StringLength(20)]
    public string Type {get;set;} = string.Empty;

    public DateTime InstallDate {get;set;}

    public ICollection<ServiceOrder> ServiceOrders {get;set;} = new List<ServiceOrder>();

    public Customer Customer {get;set;} = null!;




}