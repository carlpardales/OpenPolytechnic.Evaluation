using Infrastructure.Repositories;
using Moq;
using Moq.Protected;
using System.Net;

namespace Infrastructure.UnitTests.Repositories
{
    [TestFixture]
    public class CompanyRepositoryTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandler = null!;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>();
        }

        [Test]
        public async Task GetCompanyByIdAsync_WhenFound_ReturnsCompany()
        {
            // Arrange
            SetupHttpMessageHandler(
                HttpStatusCode.OK,
                "<Company><id>1</id><name>Test Company</name><description>Test Description</description></Company>"
                );

            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://example.com/api")
            };

            var companyRepository = new CompanyRepository(httpClient);

            // Act
            var result = await companyRepository.GetCompanyByIdAsync("1");

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("1"));
            Assert.That(result.Name, Is.EqualTo("Test Company"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
        }

        [Test]
        public async Task GetCompanyByIdAsync_WhenHttpCallFails_ReturnsNull()
        {
            // Arrange
            SetupHttpMessageHandler(HttpStatusCode.NotFound, "<p>Some error</p>");

            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://example.com/api")
            };

            var companyRepository = new CompanyRepository(httpClient);

            // Act
            var result = await companyRepository.GetCompanyByIdAsync("1");

            //Assert
            Assert.That(result, Is.Null);
        }

        private void SetupHttpMessageHandler(HttpStatusCode statusCode, string content)
        {
            _httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content)
                });
        }
    }
}