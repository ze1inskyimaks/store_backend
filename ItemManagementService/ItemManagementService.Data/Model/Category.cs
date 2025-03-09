namespace ItemManagementService.Data.Model;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Item>? Items { get; set; }
}