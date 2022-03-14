using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using JDC.IntegrationTests.Infrastructure;
using JDC.IntegrationTests.Infrastructure.Models;
using Xunit;

namespace JDC.IntegrationTests.ControllerTests
{
    public class GroupsControllerTests : IntegrationTestsBase
    {
        public GroupsControllerTests(WebApplicationFactory factory)
            : base(factory)
        {
        }

        [Theory]
        [InlineData("Home/Index")]
        [InlineData("Home/Documentation")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Act
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Assert
            response.Content.Headers.ContentType.ToString().Should().Be("text/html; charset=utf-8");
        }

        [Fact]
        public async Task Edit_GET_Action()
        {
            // Act
            var response = await Client.GetAsync("/Groups/Edit?groupId=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            responseString.Should().Contain("Редактирование группы");
        }

        [Fact]
        public async Task Edit_POST_Action_ValidModel()
        {
            // Arrange
            var (token, postRequest) = await GetRequestWithAntiForgeryToken(
                HttpMethod.Post, 
                "/Groups/Edit?groupId=1", 
                "/Groups/Edit");

            var formModel = new Dictionary<string, string>
            {
                { AntiForgeryTokenExtractor.AntiForgeryFieldName, token },
                { "Id", "1" },
                { "TecherId", "1" },
                { "InstitutionId", "1" },
                { "SpecialityId", "1" },
                { "Name", "New group name" },
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);

            // Act
            var response = await Client.SendAsync(postRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        }
    }
}
