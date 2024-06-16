using Application.Interfaces;
using Domain;

namespace Application.Companies
{
    public class GetAllDetails
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllDetails(ICompanyRepository companyRepository) => _companyRepository = companyRepository;

        public async Task<List<Company>> ExecuteAsync()
        {
            return await _companyRepository.GetAllAsync();
        }
    }
}
