
using System.Net.Http.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using EducationCenter.Api;
using EducationCenter.Service.DTOs.Students;

namespace EducationCenter.Tests.Integrations;

public class StudentControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StudentControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ShouldReturnAllStudents_WhenRequested()
    {
        var response = await _client.GetAsync("/api/student");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ShouldCreateNewStudent_WhenDataIsValid()
    {
        var studentDto = new StudentForCreationDto
        {
            FirstName = "Ali",
            LastName = "Valiyev",
            Email = $"ali{Guid.NewGuid()}@mail.com",
        };

        var response = await _client.PostAsJsonAsync("/api/student", studentDto);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdStudent = await response.Content.ReadFromJsonAsync<StudentForResultDto>();
        createdStudent.Should().NotBeNull();
        createdStudent!.FirstName.Should().Be(studentDto.FirstName);
    }

    [Fact]
    public async Task ShouldReturnStudentById_WhenStudentExists()
    {
        // Create student
        var studentDto = new StudentForCreationDto
        {
            FirstName = "Ali",
            LastName = "Valiyev",
            Email = $"ali{Guid.NewGuid()}@mail.com",
        };

        var postResponse = await _client.PostAsJsonAsync("/api/student", studentDto);
        postResponse.EnsureSuccessStatusCode();
        var createdStudent = await postResponse.Content.ReadFromJsonAsync<StudentForResultDto>();

        // Get by ID
        var getResponse = await _client.GetAsync($"/api/student/{createdStudent!.Id}");
        getResponse.EnsureSuccessStatusCode();

        var fetchedStudent = await getResponse.Content.ReadFromJsonAsync<StudentForResultDto>();
        fetchedStudent.Should().NotBeNull();
        fetchedStudent!.Id.Should().Be(createdStudent.Id);
    }

    [Fact]
    public async Task ShouldReturnStudentByName_WhenNameIsProvided()
    {
        var studentDto = new StudentForCreationDto { FirstName = "Ali" };

        await _client.PostAsJsonAsync("/api/student", studentDto);
        var response = await _client.GetAsync("/api/student/search?name=Ali");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ShouldUpdateStudent_WhenDataIsValid()
    {
        var createDto = new StudentForCreationDto
        {
            FirstName = "Ali" + Guid.NewGuid().ToString(),
            LastName = "Valiyev" + Guid.NewGuid().ToString(),
            Email = $"ali{Guid.NewGuid().ToString()}@gmail.com"
        };

        var postResponse = await _client.PostAsJsonAsync("/api/student", createDto);
        postResponse.EnsureSuccessStatusCode();
        var createdStudent = await postResponse.Content.ReadFromJsonAsync<StudentForResultDto>();
        createdStudent.Should().NotBeNull();

        var updateDto = new StudentForUpdateDto
        {
            FirstName = "Vali",
            LastName = "Aliyev"
        };

        var putResponse = await _client.PutAsJsonAsync($"/api/student/{createdStudent!.Id}", updateDto);
        putResponse.EnsureSuccessStatusCode();

        var updatedStudent = await putResponse.Content.ReadFromJsonAsync<StudentForResultDto>();
        updatedStudent.Should().NotBeNull();
        updatedStudent!.FirstName.Should().Be(updateDto.FirstName);
    }

    [Fact]
    public async Task ShouldDeleteStudent_WhenStudentExists()
    {
        var createDto = new StudentForCreationDto
        {
            FirstName = "ToDelete",
            LastName = "Student",
            Email = $"ali{Guid.NewGuid().ToString()}@gmail.com"
        };

        var postResponse = await _client.PostAsJsonAsync("/api/student", createDto);
        postResponse.EnsureSuccessStatusCode();

        var createdStudent = await postResponse.Content.ReadFromJsonAsync<StudentForResultDto>();
        createdStudent.Should().NotBeNull();

        var deleteResponse = await _client.DeleteAsync($"/api/student/{createdStudent!.Id}");
        deleteResponse.EnsureSuccessStatusCode();
    }
}
