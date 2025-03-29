using System.ComponentModel.DataAnnotations;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Business.ModelDto.Item;

public class ItemInputDto
{
    public long? Id { get; set; }
    [Required(ErrorMessage = "Name required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "From 3 to 100 character")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Price required")]
    [Range(0.01, Double.MaxValue, ErrorMessage = "Price need to be bigger then 0.01")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Price required")]
    [Range(1, Int32.MaxValue, ErrorMessage = "Quantity need to be bigger then 1")]
    public int StockQuantity { get; set; }
    public Status Status { get; set; } = Status.Available;
    public long? CategoryId { get; set; }

    [Required(ErrorMessage = "Company required")]
    public string CompanyId { get; set; } = null!;
}