using System.ComponentModel.DataAnnotations;

namespace ItemManagementService.Business.ModelDto.Category;

public class CategoryDto
{
    public long? Id { get; set; }
    
    [Required(ErrorMessage = "Name required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "From 3 to 100 character")]
    public string Name { get; set; } = null!;
}