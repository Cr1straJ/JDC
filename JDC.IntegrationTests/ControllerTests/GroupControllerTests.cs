using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using JDC.IntegrationTests.Infrastructure;
using JDC.IntegrationTests.Infrastructure.Helpers;
using NUnit.Framework;

namespace JDC.IntegrationTests.ControllerTests
{
    [TestFixture]
    internal class GroupControllerTests : IntegrationTestsBase
    {
        [Test]
        public async Task EditGroup_WhenGroupsExist_ShouldUpdateGroup()
        {
            // Arrange
            Builder.SpecialitiesBuilder.AddDefaultSpeciality().Build();
            var specialityId = Builder.SpecialitiesBuilder.Specialities.Last().Id;

            Builder.TeachersBuilder.AddDefaultTeacher().Build();
            var teacherId = Builder.TeachersBuilder.Teachers.Last().Id;

            Builder.InstitutionsBuilder.AddDefaultInstitution().Build();
            var institutionId = Builder.InstitutionsBuilder.Institutions.Last().Id;

            Builder.GroupsBuilder.AddDefaultGroup(institutionId, specialityId, teacherId).Build();
            var groupId = Builder.GroupsBuilder.Groups.Last().Id;

            using var request = new HttpRequestMessage(HttpMethod.Post, $"Groups/Edit");
            var group = Builder.GroupsBuilder.Groups.First(group => group.Id == groupId);
            group.Name = "new name";
            request.AddContent(group);

            // Act
            var actualResult = await Client.SendAsync(request);
            group = Builder.GroupsBuilder.Groups.Find(x => x.Id == groupId);
            Builder.GroupsBuilder.Reload(group);

            // Assert
            actualResult.StatusCode.Should().Be(HttpStatusCode.Redirect);
            group.Name.Should().Be("new name");
        }
    }
}
