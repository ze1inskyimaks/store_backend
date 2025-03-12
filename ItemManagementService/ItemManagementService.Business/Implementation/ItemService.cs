using ItemManagementService.Business.Interface;
using ItemManagementService.Business.ModelDto.Item;
using ItemManagementService.Data.Interface;

namespace ItemManagementService.Business.Implementation;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    
    public async Task CreateItem(ItemInputDto itemInputDto, long companyId)
    {
        var item = ItemMapping.DoModelFromInputDto(itemInputDto);
        if (0 > companyId)
        {
            throw new Exception("Error with company id");
        }

        //Categories and companies also
        
        item.CompanyId = companyId;
        
        await _itemRepository.CreateItem(item);
    }

    public async Task DeleteItemById(long id)
    {
        await _itemRepository.DeleteItemById(id);
    }

    public async Task ChangeItem(long id, ItemInputDto itemInputDto)
    {
        if (id < 0)
        {
            throw new Exception("Error with item id!");
        }
        var item = await _itemRepository.GetItemById(id);

        if (item == null)
        {
            throw new Exception("Error with finding item by id!");
        }

        item.Name = itemInputDto.Name;
        item.Description = itemInputDto.Description;
        item.StockQuantity = itemInputDto.StockQuantity;
        item.Price = itemInputDto.Price;
        item.Status = itemInputDto.Status;
        item.CategoryId = itemInputDto.CategoryId; // Categories also
        
        item.UpdatedAt = DateTime.Now;

        await _itemRepository.ChangeItem(item);
    }

    public async Task<ItemOutputDto?> GetItem(long id)
    { 
        var result = await _itemRepository.GetItemById(id);
        return result != null ? ItemMapping.DoOutputDtoFromItem(result) : null;
    }
}