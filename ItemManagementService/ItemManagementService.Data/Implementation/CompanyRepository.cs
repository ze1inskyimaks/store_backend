using ItemManagementService.Data.Interface;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Data.Implementation;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateCompany(Company company)
    {
        await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCompany(Company company)
    {
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
    }

    public async Task<Company?> GetCompanyById(long id)
    {
        return await _context.Companies.FindAsync(id);
    }

    public async Task<List<Item>?> GetAllItemByCompanies(long id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            throw new Exception("Can`t find a company by id!");
        }

        var listOfItems = company.Items;
        return listOfItems?.ToList();
    }
}