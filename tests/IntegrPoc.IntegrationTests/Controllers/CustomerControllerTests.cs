using IntegrPoc.Api;
using IntegrPoc.Api.Models;
using IntegrPoc.IntegrationTests.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrPoc.IntegrationTests.Controllers
{
    [Collection(nameof(IntegrationTestsApiFixtureCollection))]
    public class CustomerControllerTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public CustomerControllerTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Get_ShouldFindOneCustomerById")]
        public async Task Get_ShouldFindOneCustomerById()
        {
            // Arrange
            var id = new Guid("3fff9268-b022-4e4d-b1b5-f462c8240b25");

            // Act
            var response = await _testsFixture.Client.GetAsync($"api/customer/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerViewModel>(responseString);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(customer);
            Assert.Equal("First Customer", customer.Name);
        }

        [Fact(DisplayName = "Get_ShouldReturn422StatusCode_WhenCustomerIdIsNotValid")]
        public async Task Get_ShouldReturn422StatusCode_WhenCustomerIdIsNotValid()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            var response = await _testsFixture.Client.GetAsync($"api/customer/{id}");

            // Assert
            Assert.Equal(422, (int)response.StatusCode);
        }

        [Fact(DisplayName = "Register_ShouldReturn201StatusCode_WhenCustomerIsValid")]
        public async Task Register_ShouldReturn201StatusCode_WhenCustomerIsValid()
        {
            // Arrange
            var content = new StringContent(JsonConvert.SerializeObject(new CustomerViewModel
            {
                Id = new Guid("b0b99307-92fc-481d-94d4-c445a5f106cf"),
                Name = "One Customer"
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _testsFixture.Client.PostAsync("api/customer/", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact(DisplayName = "Update_ShouldReturn204StatusCode_WhenCustomerIsValid")]
        public async Task Update_ShouldReturn204StatusCode_WhenCustomerIsValid()
        {
            // Arrange
            var content = new StringContent(JsonConvert.SerializeObject(new CustomerViewModel
            {
                Id = new Guid("b0b99307-92fc-481d-94d4-c445a5f106cf"),
                Name = "One Customer"
            }), Encoding.UTF8, "application/json");

            // Act
            var response = await _testsFixture.Client.PutAsync("api/customer/", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact(DisplayName = "Delete_ShouldReturn204StatusCode_WhenCustomerIdIsValid")]
        public async Task Delete_ShouldReturn204StatusCode_WhenCustomerIdIsValid()
        {
            // Arrange
            var id = new Guid("3fff9268-b022-4e4d-b1b5-f462c8240b25");

            // Act
            var response = await _testsFixture.Client.DeleteAsync($"api/customer/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
