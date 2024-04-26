using FunctionalTests.Controllers.Common;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Newtonsoft.Json;
using System.Text;

namespace FunctionalTests.Controllers.Entity
{
    public class BlogControllerTest : BaseControllerTests
    {

        public BlogControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {

        }
        [Fact]
        public async Task Valid_GetData_ReturnsList()
        {
            var client = this.GetNewClientByAdminAuthorization();
            var response = await client.GetAsync($"/api/Blog/GetList");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<BlogDto>>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);

            Assert.Equal(4, result.Count);
        }


        #region Private Methode
        
        #endregion
    }
}
