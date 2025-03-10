using ItemManagementService.Data.Model;

namespace ItemManagementService.Data.Interface;

public interface IItemRepository
{
    public Task CreateItem(Item item);
    public Task DeleteItem(Item item);
    public Task DeleteItemById(long id);
    public Task ChangeItem(Item item);
    public Task<Item?> GetItemById(long id);
}