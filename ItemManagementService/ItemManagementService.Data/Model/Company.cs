namespace ItemManagementService.Data.Model;

public class Company
{
    public long Id { get; set; }
    public ICollection<Item>? Items { get; set; }
}