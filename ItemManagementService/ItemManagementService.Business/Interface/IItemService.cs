using ItemManagementService.Business.ModelDto.Item;

namespace ItemManagementService.Business.Interface;

public interface IItemService
{
    public Task CreateItem(ItemInputDto itemInputDto);
    public Task DeleteItemById(long id);
    public Task ChangeItem(long id, ItemInputDto itemInputDto);
    public Task<ItemOutputDto?> GetItem(long id);
}