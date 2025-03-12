using ItemManagementService.Business.Interface;
using ItemManagementService.Business.ModelDto.Category;
using ItemManagementService.Business.ModelDto.Item;
using ItemManagementService.Data.Interface;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Business.Implementation;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task CreateCategory(CategoryDto categoryDto)
    {
        var category = new Category()
        {
            Name = categoryDto.Name
        };
        await _categoryRepository.CreateCategory(category);
    }

    public async Task DeleteCategory(long id)
    {
        var category = await _categoryRepository.GetCategoryById(id);
        if (category == null)
        {
            throw new Exception("Error with category id");
        }
        
        await _categoryRepository.DeleteCategory(category);
    }

    public async Task<CategoryDto?> GetCategoryById(long id)
    {
        var result = await _categoryRepository.GetCategoryById(id);
        return result == null ? null : new CategoryDto() { Id = result!.Id, Name = result.Name };
    }

    public async Task<List<ItemOutputDto>?> GetAllItemByCategories(long id)
    {
        var result = await _categoryRepository.GetAllItemByCategories(id);
        return result?.Select(ItemMapping.DoOutputDtoFromItem).ToList();
    }
}