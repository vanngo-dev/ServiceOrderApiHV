
using System.ComponentModel.DataAnnotations;

namespace ServiceOrderApi.Models;

public class Customer
{
    public int Id {get; set;}

    [StringLength(100)]
    [Required]
    public string Name {get; set;} = string.Empty;

    [StringLength(20)]
    [Required]    
    public string Phone {get;set;} = string.Empty;

    [EmailAddress]
    public string? Email {get;set;} 

    [StringLength(200)]
    public string? Address {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

    public ICollection<Equipment> Equipment {get;set;} = new List<Equipment>();

    public ICollection<ServiceOrder> ServiceOrders {get;set;} = new List<ServiceOrder>();


}