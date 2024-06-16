using Microsoft.AspNetCore.Mvc;
using Application.Companies;
using Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly GetAllDetails _getAllDetails;
        private readonly Details _details;

        public CompaniesController(GetAllDetails getAllDetails, Details details)
        {
            _getAllDetails = getAllDetails;
            _details = details;
        }

        [HttpGet] //api/companies
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            var companies = await _getAllDetails.ExecuteAsync();         

            return Ok(companies);
        }

        [HttpGet("{id}")] //api/companies/1
        public async Task<ActionResult> GetCompany(string id)
        {
            var company = await _details.ExecuteAsync(id);

            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
    }
}
