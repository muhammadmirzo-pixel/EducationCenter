using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using EducationCenter.Api;
using EducationCenter.Service.DTOs.Students;
using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Domain.Entites;

namespace EducationCenter.Tests.Integrations;

public class GroupControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GroupControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CheckToGetAll()
    {
        var response = await _client.GetAsync("/api/group");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CheckToPostStudent()
    {
        var dto = new GroupForCreationDto { CourseId = 1, GroupName = "Math",  };
        var response = await _client.PostAsJsonAsync("/api/group", dto);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CheckToGetById()
    {
        var create = new GroupForResultDto { Id = 1, GroupName = "Math", CourseId = 1 };
        var postResp = await _client.PostAsJsonAsync("/api/group", create);
        var created = await postResp.Content.ReadFromJsonAsync<StudentForResultDto>();
        var response = await _client.GetAsync($"/api/group/{created.Id}");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CheckToGetByName()
    {
        var create = new GroupForResultDto { GroupName = "Math" };
        await _client.PostAsJsonAsync("/api/group", create);
        var response = await _client.GetAsync("/api/group/search?name = Math");
        response.EnsureSuccessStatusCode();
    }

   /* [Fact]
    public async Task CheckToUpdateStudent()
    {
        var update = new GroupForUpdateDto { GroupName = "Math" };
        var putResp = await _client.PutAsJsonAsync("/api/group", update);
        var updated = await putResp.Content.ReadFromJsonAsync<GroupForResultDto>();

        var updateResp = await _client.PutAsync("/api/group/");
        updateResp.EnsureSuccessStatusCode();
    }*/

    [Fact]
    public async Task CheckToDeleteStudent()
    {
        var delete = new GroupForResultDto { Id = 1, CourseId = 1 };
        var deleteResp = await _client.PostAsJsonAsync("/api/group", delete);
        var deleted = await deleteResp.Content.ReadFromJsonAsync<GroupForResultDto>();

        var delResp = await _client.DeleteAsync($"/api/group/{deleted.Id}");
        delResp.EnsureSuccessStatusCode();
    }
}