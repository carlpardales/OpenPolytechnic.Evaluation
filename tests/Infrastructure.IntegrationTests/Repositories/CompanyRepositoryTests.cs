using Infrastructure.Repositories;

namespace Infrastructure.IntegrationTests.Repositories
{
    [TestFixture]
    public class CompanyRepositoryTests
    {
        private HttpClient _httpClient = null!;


        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://raw.githubusercontent.com")
            };
        }

        [TearDown]
        public void Teardown()
        {
            _httpClient.Dispose();
        }

        [TestCase("1")]
        [TestCase("2")]
        [Test]
        public async Task GetCompanyByIdAsync_WhenRealApiFindsTheId_ReturnsCompany(string id)
        {
            // Arrange
            var companyRepository = new CompanyRepository(_httpClient);

            // Act
            var result = await companyRepository.GetCompanyByIdAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetCompanyByIdAsync_WhenRealApiFindsNoId_ReturnsNull()
        {
            // Arrange
            var companyRepository = new CompanyRepository(_httpClient);

            // Act
            var result = await companyRepository.GetCompanyByIdAsync("3");

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}