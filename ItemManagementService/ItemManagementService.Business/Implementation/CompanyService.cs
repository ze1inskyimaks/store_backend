using ItemManagementService.Business.Interface;
using ItemManagementService.Business.ModelDto.Item;
using ItemManagementService.Data.Interface;
using ItemManagementService.Data.Model;

namespace ItemManagementService.Business.Implementation;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task CreateCompany(long id)
    {
        var company = new Company()
        {
            Id = id
        };
        await _companyRepository.CreateCompany(company);
    }

    public async Task DeleteCompany(long id)
    {
        var company = await _companyRepository.GetCompanyById(id);
        if (company == null)
        {
            throw new Exception("Error with company id");
        }
        await _companyRepository.DeleteCompany(company);
    }

    public async Task<Company?> GetCompanyById(long id)
    {
        return await _companyRepository.GetCompanyById(id);
    }

    public async Task<List<ItemOutputDto>?> GetAllItemByCompanies(long id)
    {
        var result = await _companyRepository.GetAllItemByCompanies(id);
        return result?.Select(ItemMapping.DoOutputDtoFromItem).ToList();
    }
}