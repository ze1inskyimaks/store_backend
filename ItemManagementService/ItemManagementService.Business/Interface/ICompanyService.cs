using ItemManagementService.Business.ModelDto.Item;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Business.Interface;

public interface ICompanyService 
{
    public Task CreateCompany(long id);
    public Task DeleteCompany(long id);
    public Task<Company?> GetCompanyById(long id);
    public Task<List<ItemOutputDto>?> GetAllItemByCompanies(long id);
}