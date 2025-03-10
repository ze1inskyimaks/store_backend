using ItemManagementService.Data.Interface;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Data.Implementation;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateItem(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteItem(Item item)
    {
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteItemById(long id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            throw new Exception("Can`t find item in Db!");
        }
        
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeItem(Item item)
    {
        var currentItem = await _context.Items.FindAsync(item.Id);
        if (currentItem != null)
        {
            _context.Items.Attach(currentItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Item?> GetItemById(long id)
    {
        return await _context.Items.FindAsync(id);
    }
}