using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using EducationCenter.Api;
using EducationCenter.Service.DTOs.Courses;
using Microsoft.AspNetCore.Mvc.Testing;

public class CourseControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CourseControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnSuccess()
    {
        // Act
        var response = await _client.GetAsync("/api/course");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateCourse_ShouldReturnCreatedCourse()
    {
        // Arrange
        var dto = new CourseForCreationDto
        {
            CourseName = $"TestCourse_{Guid.NewGuid()}"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/course", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseForResultDto>();
        result.Should().NotBeNull();
        result!.CourseName.Should().Be(dto.CourseName);
    }

    [Fact]
    public async Task GetCourseById_ShouldReturnCorrectCourse()
    {
        // Arrange
        var createDto = new CourseForCreationDto
        {
            CourseName = $"TestCourse_{Guid.NewGuid()}"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/course", createDto);
        createResponse.EnsureSuccessStatusCode();
        var createdCourse = await createResponse.Content.ReadFromJsonAsync<CourseForResultDto>();

        // Act
        var getResponse = await _client.GetAsync($"/api/course/{createdCourse!.Id}");

        // Assert
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var fetchedCourse = await getResponse.Content.ReadFromJsonAsync<CourseForResultDto>();
        fetchedCourse.Should().NotBeNull();
        fetchedCourse!.Id.Should().Be(createdCourse.Id);
    }

    [Fact]
    public async Task SearchCourseByName_ShouldReturnSuccess()
    {
        // Arrange
        var courseName = $"TestCourse_{Guid.NewGuid()}";
        var createDto = new CourseForCreationDto { CourseName = courseName };
        await _client.PostAsJsonAsync("/api/course", createDto);

        // Act
        var response = await _client.GetAsync($"/api/course/search?name={courseName}");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateCourse_ShouldModifyCourseSuccessfully()
    {
        // Arrange
        var createDto = new CourseForCreationDto { CourseName = "5-sinf" };
        var createResponse = await _client.PostAsJsonAsync("/api/course", createDto);
        createResponse.EnsureSuccessStatusCode();
        var existingCourse = await createResponse.Content.ReadFromJsonAsync<CourseForResultDto>();

        var updateDto = new CourseForUpdateDto { CourseName = "6-sinf" };

        // Act
        var updateResponse = await _client.PutAsJsonAsync($"/api/course/{existingCourse!.Id}", updateDto);

        // Assert
        updateResponse.EnsureSuccessStatusCode();
        var updatedCourse = await updateResponse.Content.ReadFromJsonAsync<CourseForResultDto>();
        updatedCourse.Should().NotBeNull();
        updatedCourse!.CourseName.Should().Be(updateDto.CourseName);
    }

    [Fact]
    public async Task DeleteCourse_ShouldRemoveCourseSuccessfully()
    {
        // Arrange
        var createDto = new CourseForCreationDto { CourseName = "Math" };
        var createResponse = await _client.PostAsJsonAsync("/api/course", createDto);
        createResponse.EnsureSuccessStatusCode();
        var courseToDelete = await createResponse.Content.ReadFromJsonAsync<CourseForResultDto>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/course/{courseToDelete!.Id}");

        // Assert
        deleteResponse.EnsureSuccessStatusCode();
    }
}
