using System.ComponentModel.DataAnnotations;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Business.ModelDto.Item;

public class ItemOutputDto
{
    public long? Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int StockQuantity { get; set; }
    
    public Status Status { get; set; } = Status.Available;
    
    public long? CategoryId { get; set; }
    
    public long CompanyId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}