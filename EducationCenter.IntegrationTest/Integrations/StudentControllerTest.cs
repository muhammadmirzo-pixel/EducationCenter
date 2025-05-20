using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using EducationCenter.Api;
using EducationCenter.Service.DTOs.Students;
using Microsoft.AspNetCore.Http.HttpResults;
using EducationCenter.Service.DTOs.Groups;

namespace EducationCenter.Tests.Integrations;
public class StudentControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StudentControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestGetAll()
    {
        var response = await _client.GetAsync("/api/student");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestPostStudent()
    {
        var dto = new StudentForCreationDto { FirstName = "Ali", LastName = "Valiyev", Email = "asdasd@gmail.com", Password = "asdas_123" };
        var response = await _client.PostAsJsonAsync("/api/student", dto);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetById()
    {
        var get = new StudentForResultDto { Id = 1, FirstName = "Ali", LastName = "Valiyev", Email = "asdasd@gmail.com" };
        var getResp = await _client.PostAsJsonAsync("/api/student", get);
        var created = await getResp.Content.ReadFromJsonAsync<StudentForResultDto>();
        var response = await _client.GetAsync($"/api/student/{created.Id}");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetByName()
    {
        var get = new StudentForResultDto { Id = 1, FirstName = "Ali", LastName = "Valiyev", Email = "asdasd@gmail.com"};
        await _client.PostAsJsonAsync("/api/student", get);
        var response = await _client.GetAsync("/api/student/search?name=Ali");
        response.EnsureSuccessStatusCode();
    }

    /*[Fact]
    public async Task TestUpdateStudent()
    {
        var update = new StudentForUpdateDto { FirstName = "Ali", LastName = "Valiyev" };
        var putResp = await _client.PostAsJsonAsync("/api/student", update);
        var updated = await potResp.Content.ReadFromJsonAsync<StudentForResultDto>();

        var update = new StudentForUpdateDto { FirstName = "Vali", LastName = "Aliev" };
        var putResp = await _client.PutAsJsonAsync($"/api/student/{created.Id}", update);
        putResp.EnsureSuccessStatusCode();
    }*/

    [Fact]
    public async Task TestDeleteStudent()
    {
        var delete = new StudentForResultDto { Id = 1};
        var postResp = await _client.PostAsJsonAsync("/api/student", delete);
        var deleted  = await postResp.Content.ReadFromJsonAsync<StudentForResultDto>();

        var delResp = await _client.DeleteAsync($"/api/student/{deleted.Id}");
        delResp.EnsureSuccessStatusCode();
    }
}