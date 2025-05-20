using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using EducationCenter.Api;
using EducationCenter.Service.DTOs.StudentsGroup;
using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.DTOs.Students;

namespace EducationCenter.Tests.Integrations;

public class StudentGroupControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StudentGroupControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestGetAll()
    {
        var response = await _client.GetAsync("/api/studentGroup");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestPostAsync()
    {
        var group = new StudentGroupForCreationDto { StudentId = 1, GroupId = 1};
        var groupResponse = await _client.PostAsJsonAsync("/api/studentGroup", group);
        groupResponse.EnsureSuccessStatusCode();
        var createdGroup = await groupResponse.Content.ReadFromJsonAsync<StudentGroupForResultDto>();

        Assert.Equal(group.StudentId, createdGroup.StudentId);
        Assert.Equal(group.GroupId, createdGroup.GroupId);
    }

    [Fact]
    public async Task TestGetById()
    {
        var create = new StudentGroupForResultDto { Id = 1, StudentId = 1, GroupId = 1 };
        var postResp = await _client.PostAsJsonAsync("/api/studentgroup", create);
        var created = await postResp.Content.ReadFromJsonAsync<StudentGroupForResultDto>();
        var response = await _client.GetAsync($"/api/studentgroup/{created.Id}");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestUpdateStudentGroup()
    {
        var create = new StudentGroupForResultDto { Id = 1, StudentId = 1, GroupId = 1 };
        var postResp = await _client.PostAsJsonAsync("/api/studentgroup", create);
        var created = await postResp.Content.ReadFromJsonAsync<StudentGroupForResultDto>();

        var update = new StudentGroupForUpdateDto { /* required fields for update */ };
        var putResp = await _client.PutAsJsonAsync($"/api/studentgroup/{created.Id}", update);
        putResp.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestDeleteStudentGroup()
    {
        var delete = new bool {  };
        var postResp = await _client.PostAsJsonAsync("/api/studentgroup", delete);
        var created = await postResp.Content.ReadFromJsonAsync<StudentGroupForResultDto>();

        var delResp = await _client.DeleteAsync($"/api/studentgroup/{delete}");
        delResp.EnsureSuccessStatusCode();
    }
}