using ItemManagementService.Data.Interface;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Data.Implementation;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategory(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryById(long id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<List<Item>?> GetAllItemByCategories(long id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new Exception("Can`t find a category by id!");
        }

        var listOfItems = category.Items;
        return listOfItems?.ToList();
    }
}