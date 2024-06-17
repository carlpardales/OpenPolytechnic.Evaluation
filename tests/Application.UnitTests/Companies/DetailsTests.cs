using Application.Companies;
using Application.Interfaces;
using Domain;
using Moq;

namespace Application.UnitTests.Companies
{
    [TestFixture]
    public class DetailsTests
    {
        [TestCase("1")]
        [Test]
        public async Task ExecuteAsync_WhenCompanyIsInRepository_ReturnsCompanyDetails(string id)
        {
            // Arrange
            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository
                .Setup(cr => cr.GetCompanyByIdAsync(id))
                .Returns(Task.FromResult<Company?>(
                    new Company
                    {
                        Id = "1",
                        Name = "the company",
                        Description = "some description"
                    }));

            var details = new Details(companyRepository.Object);

            // Act
            var result = await details.ExecuteAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [TestCase("1")]
        [Test]
        public async Task ExecuteAsync_WhenCompanyIsNotInRepository_ReturnsNull(string id)
        {
            // Arrange
            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository
                .Setup(cr => cr.GetCompanyByIdAsync(id))
                .Returns(Task.FromResult<Company?>(null));

            var details = new Details(companyRepository.Object);

            // Act
            var result = await details.ExecuteAsync(id);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
