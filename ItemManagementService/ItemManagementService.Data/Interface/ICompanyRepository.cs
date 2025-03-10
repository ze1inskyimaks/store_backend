using ItemManagementService.Data.Model;

namespace ItemManagementService.Data.Interface;

public interface ICompanyRepository
{
    public Task CreateCompany(Company company);
    public Task DeleteCompany(Company company);
    public Task<Company?> GetCompanyById(long id);
    public Task<List<Item>?> GetAllItemByCompanies(long id);
}