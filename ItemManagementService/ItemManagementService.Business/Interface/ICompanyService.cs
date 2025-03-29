using ItemManagementService.Business.ModelDto.Item;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Business.Interface;

public interface ICompanyService 
{
    public Task CreateCompany(string id);
    public Task DeleteCompany(string id);
    public Task<Company?> GetCompanyById(string id);
    public Task<List<ItemOutputDto>?> GetAllItemByCompanies(string id);
}