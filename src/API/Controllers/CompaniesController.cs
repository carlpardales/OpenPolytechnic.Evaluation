using Microsoft.AspNetCore.Mvc;
using Application.Companies;
using Domain;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly Details _details;

        public CompaniesController(Details details)
        {
            _details = details;
        }

        [HttpGet("{id}")] //api/v1/companies/1
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
