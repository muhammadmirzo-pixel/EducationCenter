using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using EducationCenter.Api;
using EducationCenter.Service.DTOs.StudentsGroup;

using EducationCenter.Service.DTOs.Students;
using EducationCenter.Service.DTOs.Groups;

namespace EducationCenter.Tests.Integrations;

public class StudentGroupControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    private async Task<long> CreateStudentAsync()
    {
        var dto = new StudentForCreationDto
        {
            FirstName = "Test",
            LastName = "Student",
            Email = $"test{Guid.NewGuid().ToString()}@example.com",
            Password = "P4$$w0rd"
        };

        var response = await _client.PostAsJsonAsync("api/student", dto);
        response.EnsureSuccessStatusCode();

        var createdStudent = await response.Content.ReadFromJsonAsync<StudentForResultDto>();
        return createdStudent!.Id;
    }

    private async Task<long> CreateGroupAsync()
    {
        var dto = new GroupForCreationDto
        {
            CourseId = 1,
            GroupName = $"Group-{Guid.NewGuid().ToString()}"
        };

        var response = await _client.PostAsJsonAsync("api/group", dto);
        response.EnsureSuccessStatusCode();

        var createdGroup = await response.Content.ReadFromJsonAsync<GroupForResultDto>();
        return createdGroup!.Id;
    }


    public StudentGroupControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]

    public async Task ShouldGetAllStudentGroupsAsync()
    {
        var response = await _client.GetAsync("/api/studentGroup");
        response.EnsureSuccessStatusCode();
    }

    [Fact]

    public async Task ShouldCreateStudentGroupAsync()
    {
        long studentId = await CreateStudentAsync();
        long groupId = await CreateGroupAsync();

        var dto = new StudentGroupForCreationDto
        {
            StudentId = studentId,
            GroupId = groupId
        };

        var response = await _client.PostAsJsonAsync("/api/studentGroup", dto);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<StudentGroupForResultDto>();

        Assert.NotNull(created);
        Assert.Equal(dto.StudentId, created!.StudentId);
        Assert.Equal(dto.GroupId, created.GroupId);
    }

    [Fact]
    public async Task ShouldGetStudentGroupByIdAsync()
    {
        long studentId = await CreateStudentAsync();
        long groupId = await CreateGroupAsync();

        var dto = new StudentGroupForCreationDto
        {
            StudentId = studentId,
            GroupId = groupId
        };

        var postResponse = await _client.PostAsJsonAsync("/api/studentGroup", dto);
        postResponse.EnsureSuccessStatusCode();

        var created = await postResponse.Content.ReadFromJsonAsync<StudentGroupForResultDto>();
        Assert.NotNull(created);

        var getResponse = await _client.GetAsync($"/api/studentGroup/{created!.Id}");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ShouldUpdateStudentGroupAsync()
    {
        long studentId = await CreateStudentAsync();
        long groupId = await CreateGroupAsync();

        var dto = new StudentGroupForCreationDto
        {
            StudentId = studentId,
            GroupId = groupId
        };

        var createResponse = await _client.PostAsJsonAsync("/api/studentGroup", dto);
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<StudentGroupForResultDto>();
        Assert.NotNull(created);

        var updateDto = new StudentGroupForUpdateDto
        {
            StudentId = studentId,
            GroupId = groupId
        };

        var updateResponse = await _client.PutAsJsonAsync($"/api/studentGroup/{created!.Id}", updateDto);
        updateResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ShouldDeleteStudentGroupAsync()
    {
        long studentId = await CreateStudentAsync();
        long groupId = await CreateGroupAsync();

        var dto = new StudentGroupForCreationDto
        {
            StudentId = studentId,
            GroupId = groupId
        };

        var createResponse = await _client.PostAsJsonAsync("/api/studentGroup", dto);
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<StudentGroupForResultDto>();
        Assert.NotNull(created);

        var deleteResponse = await _client.DeleteAsync($"/api/studentGroup/{created!.Id}");
        deleteResponse.EnsureSuccessStatusCode();
    }
}