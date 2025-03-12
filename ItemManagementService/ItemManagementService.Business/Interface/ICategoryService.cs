using ItemManagementService.Business.ModelDto.Category;
using ItemManagementService.Business.ModelDto.Item;

namespace ItemManagementService.Business.Interface;

public interface ICategoryService
{
    public Task CreateCategory(CategoryDto category);
    public Task DeleteCategory(long id);
    public Task<CategoryDto?> GetCategoryById(long id);
    public Task<List<ItemOutputDto>?> GetAllItemByCategories(long id);
}