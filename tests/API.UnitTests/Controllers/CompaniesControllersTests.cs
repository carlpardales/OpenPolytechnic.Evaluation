using API.Controllers;
using Application.Companies;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.UnitTests.Controllers
{
    [TestFixture]
    public class CompaniesControllersTests
    {
        [Test]
        public async Task GetCompany_WhenDetailsFound_ReturnsOkResult_WithCompany()
        {
            // Arrange
            var companyId = "1";
            var expectedCompany = new Company
            {
                Id = companyId,
                Name = "Test Company",
                Description = "Test Description"
            };

            var details = new Mock<IDetails>();
            details
                .Setup(d => d.ExecuteAsync(companyId))
                .Returns(Task.FromResult<Company?>(expectedCompany));

            var companiesController = new CompaniesController(details.Object);

            // Act
            var result = await companiesController.GetCompany(companyId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            Assert.That(okResult.Value, Is.InstanceOf<Company>());
            var returnValue = okResult.Value as Company;
            Assert.That(returnValue, Is.Not.Null);
            Assert.That(returnValue.Id, Is.EqualTo(companyId));
        }

        [Test]
        public async Task GetCompany_WhenDetailsDoNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var companyId = "1";

            var details = new Mock<IDetails>();
            details
                .Setup(d => d.ExecuteAsync("3"))
                .Returns(Task.FromResult<Company?>(null));

            var companiesController = new CompaniesController(details.Object);

            // Act
            var result = await companiesController.GetCompany(companyId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = result as NotFoundResult;
            Assert.That(notFoundResult, Is.Not.Null);
        }
    }
}