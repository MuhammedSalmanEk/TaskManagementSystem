using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
namespace Tests.IntegrationApiTest
{
	public class TasksApiTests:IClassFixture<WebApplicationFactory<Program>>	
	{
		private readonly HttpClient _client;

		public TasksApiTests(WebApplicationFactory<Program> factory)
		{
			_client = factory.CreateClient();
		}



		[Fact]

        public async Task GetTasks_ShouldReturnUnauthorized_IfHeaderMissing()
        {
            var response = await _client.GetAsync("/api/tasks");

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetTasks_ShouldReturnOk_WithUserHeader()
        {
            _client.DefaultRequestHeaders.Add("user", "admin");

            var response = await _client.GetAsync("/api/tasks");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}

