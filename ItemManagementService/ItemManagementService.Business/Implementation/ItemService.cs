using ItemManagementService.Business.Interface;
using ItemManagementService.Business.ModelDto.Item;
using ItemManagementService.Data.Interface;

namespace ItemManagementService.Business.Implementation;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ICompanyService _companyService;
    private readonly ICategoryRepository _categoryRepository;

    public ItemService(IItemRepository itemRepository, ICompanyService companyService, ICategoryRepository categoryRepository)
    {
        _itemRepository = itemRepository;
        _companyService = companyService;
        _categoryRepository = categoryRepository;
    }
    
    public async Task CreateItem(ItemInputDto itemInputDto)
    {
        var item = ItemMapping.DoModelFromInputDto(itemInputDto);
        
        if (itemInputDto.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetCategoryById(itemInputDto.CategoryId.Value);
            item.CategoryId = itemInputDto.CategoryId;
            item.Categories = category;
        }
        
        var company = await _companyService.GetCompanyById(itemInputDto.CompanyId);
        
        item.CompanyId = itemInputDto.CompanyId;
        item.Companies = company!;
        
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