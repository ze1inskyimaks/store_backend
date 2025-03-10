using ItemManagementService.Data.Model;

namespace ItemManagementService.Data.Interface;

public interface ICategoryRepository
{
    public Task CreateCategory(Category category);
    public Task DeleteCategory(Category category);
    public Task<Category?> GetCategoryById(long id);
    public Task<List<Item>?> GetAllItemByCategories(long id);
}