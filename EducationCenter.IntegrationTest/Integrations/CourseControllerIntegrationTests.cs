using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using EducationCenter.Api;
using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.DTOs.Groups;

public class CourseControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CourseControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestGetAll()
    {
        var response = await _client.GetAsync("/api/course");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestPostCourse()
    {
        var dto = new CourseForCreationDto { CourseName = "TestCourse" };
        var response = await _client.PostAsJsonAsync("/api/course", dto);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetById()
    {
        var create = new CourseForCreationDto { CourseName = "TestCourse" };
        var postResp = await _client.PostAsJsonAsync("/api/course", create);
        postResp.EnsureSuccessStatusCode();
        var created = await postResp.Content.ReadFromJsonAsync<CourseForResultDto>();
        var response = await _client.GetAsync($"/api/course/{created.Id}");
        response.EnsureSuccessStatusCode();
        var course = await response.Content.ReadFromJsonAsync<CourseForResultDto>();
        Assert.Equal("TestCourse", course.CourseName);
    }

    [Fact]
    public async Task TestGetByName()
    {
        var create = new CourseForCreationDto { CourseName = "TestCourse" };
        await _client.PostAsJsonAsync("/api/course", create);
        var response = await _client.GetAsync("/api/course/search?name=TestCourse");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestUpdateCourse()
    {
        var create = new CourseForCreationDto { CourseName = "TestCourse" };
        var postResp = await _client.PostAsJsonAsync("/api/course", create);
        postResp.EnsureSuccessStatusCode();
        var created = await postResp.Content.ReadFromJsonAsync<CourseForResultDto>();

        var update = new CourseForUpdateDto { CourseName = "UpdatedCourse" };
        var putResp = await _client.PutAsJsonAsync($"/api/course/{created.Id}", update);
        putResp.EnsureSuccessStatusCode();
        var updated = await putResp.Content.ReadFromJsonAsync<CourseForResultDto>();
        Assert.Equal("UpdatedCourse", updated.CourseName);
    }

    [Fact]
    public async Task TestDeleteCourse()
    {
        var delete = new CourseForResultDto { Id = 1 };
        var deleteResp = await _client.PostAsJsonAsync("/api/course", delete);
        var deleted = await deleteResp.Content.ReadFromJsonAsync<CourseForResultDto>();

        var delResp = await _client.DeleteAsync($"/api/course/{deleted.Id}");
        delResp.EnsureSuccessStatusCode();
    }
}