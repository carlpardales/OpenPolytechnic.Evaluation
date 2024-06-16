using Application.Interfaces;
using Domain;
using System.Xml.Linq;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly HttpClient _httpClient;

        public CompanyRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Company?> GetCompanyByIdAsync(string companyId)
        {
            var company = (await GetAll()).Find(c => c.Id == companyId);

            return (await GetAll()).Find(c => c.Id == companyId);
        }

        private async Task<List<Company>> GetAll()
        {
            List<Company> companies = [];

            try
            {
                var company1Task = GetCompany("/openpolytechnic/dotnet-developer-evaluation/main/xml-api/1.xml");
                var company2Task = GetCompany("/openpolytechnic/dotnet-developer-evaluation/main/xml-api/2.xml");

                await Task.WhenAll(company1Task, company2Task);

                companies.Add(company1Task.Result);
                companies.Add(company2Task.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 
            }

            return companies;
        }
        

        private async Task<Company> GetCompany(string path)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                // TODO: Throwing exception atm. Find better way to handle 3rd party API errors.
                throw new HttpRequestException($"Error fetching XML data from {path}: {response.ReasonPhrase}");
            }

            // Convert XML to model
            string xmlContentAsString = await response.Content.ReadAsStringAsync();
            XDocument xmlDoc = XDocument.Parse(xmlContentAsString);

            Company company = new()
            {
                Id = xmlDoc.Root?.Element("id")?.Value ?? string.Empty,
                Name = xmlDoc.Root?.Element("name")?.Value ?? string.Empty,
                Description = xmlDoc.Root?.Element("description")?.Value ?? string.Empty
            };

            return company;
        }
    }
}
