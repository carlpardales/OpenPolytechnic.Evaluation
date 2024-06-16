using Domain;

namespace Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetCompanyByIdAsync(string companyId);
    }
}
