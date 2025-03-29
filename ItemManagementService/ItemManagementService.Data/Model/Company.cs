namespace ItemManagementService.Data.Model;

public class Company
{
    public required string Id { get; set; }
    public ICollection<Item>? Items { get; set; }
}