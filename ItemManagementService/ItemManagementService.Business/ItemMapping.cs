using ItemManagementService.Business.ModelDto.Item;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Business;

public static class ItemMapping
{
    public static Item DoModelFromInputDto(ItemInputDto itemInputDto)
    {
        var item = new Item()
        {
            Name = itemInputDto.Name,
            Description = itemInputDto.Description,
            Price = itemInputDto.Price,
            StockQuantity = itemInputDto.StockQuantity,
            Status = itemInputDto.Status,
            CategoryId = itemInputDto.CategoryId,
        };
        return item;
    }

    public static ItemOutputDto DoOutputDtoFromItem(Item item)
    {
        var itemDto = new ItemOutputDto()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            Status = item.Status,
            CategoryId = item.CategoryId,
            CompanyId = item.CompanyId,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
        return itemDto;
    }
}