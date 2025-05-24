using EducationCenter.Service.DTOs.Groups;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace EducationCenter.Tests.Integrations;

public class GroupControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GroupControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllGroups_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/api/group");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateGroup_WithValidData_ShouldReturnCreatedGroup()
    {
        var newGroup = new GroupForCreationDto
        {
            CourseId = 1,
            GroupName = "Math" + Guid.NewGuid()
        };

        var response = await _client.PostAsJsonAsync("/api/group", newGroup);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdGroup = await response.Content.ReadFromJsonAsync<GroupForResultDto>();
        createdGroup.Should().NotBeNull();
        createdGroup.GroupName.Should().Be(newGroup.GroupName);
    }

    [Fact]
    public async Task GetGroupById_WhenGroupExists_ShouldReturnGroup()
    {
        var groupToCreate = new GroupForCreationDto
        {
            CourseId = 1,
            GroupName = "Physics" + Guid.NewGuid()
        };

        var postResponse = await _client.PostAsJsonAsync("/api/group", groupToCreate);
        postResponse.EnsureSuccessStatusCode();

        var createdGroup = await postResponse.Content.ReadFromJsonAsync<GroupForResultDto>();
        createdGroup.Should().NotBeNull();

        var getResponse = await _client.GetAsync($"/api/group/{createdGroup.Id}");
        getResponse.EnsureSuccessStatusCode();

        var fetchedGroup = await getResponse.Content.ReadFromJsonAsync<GroupForResultDto>();
        fetchedGroup.Should().NotBeNull();
        fetchedGroup.Id.Should().Be(createdGroup.Id);
        fetchedGroup.GroupName.Should().Be(createdGroup.GroupName);
    }

    [Fact]
    public async Task SearchGroupsByName_ShouldReturnSuccess()
    {
        var groupToCreate = new GroupForCreationDto
        {
            CourseId = 1,
            GroupName = "Chemistry" + Guid.NewGuid()
        };

        var postResponse = await _client.PostAsJsonAsync("/api/group", groupToCreate);
        postResponse.EnsureSuccessStatusCode();

        // Trim spaces around '=' in query string for correct request
        var searchName = groupToCreate.GroupName;
        var getResponse = await _client.GetAsync($"/api/group/search?name={searchName}");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateGroup_WhenGroupExists_ShouldReturnUpdatedGroup()
    {
        var groupToCreate = new GroupForCreationDto
        {
            CourseId = 1,
            GroupName = "Biology" + Guid.NewGuid()
        };

        var postResponse = await _client.PostAsJsonAsync("/api/group", groupToCreate);
        postResponse.EnsureSuccessStatusCode();

        var createdGroup = await postResponse.Content.ReadFromJsonAsync<GroupForResultDto>();
        createdGroup.Should().NotBeNull();

        var updateDto = new GroupForUpdateDto
        {
            GroupName = "UpdatedGroupName"
        };

        var putResponse = await _client.PutAsJsonAsync($"/api/group/{createdGroup.Id}", updateDto);
        putResponse.EnsureSuccessStatusCode();

        var updatedGroup = await putResponse.Content.ReadFromJsonAsync<GroupForResultDto>();
        updatedGroup.Should().NotBeNull();
        updatedGroup.GroupName.Should().Be(updateDto.GroupName);
    }

    [Fact]
    public async Task DeleteGroup_WhenGroupExists_ShouldReturnSuccess()
    {
        var groupToCreate = new GroupForCreationDto
        {
            CourseId = 1,
            GroupName = "History" + Guid.NewGuid()
        };

        var postResponse = await _client.PostAsJsonAsync("/api/group", groupToCreate);
        postResponse.EnsureSuccessStatusCode();

        var createdGroup = await postResponse.Content.ReadFromJsonAsync<GroupForResultDto>();
        createdGroup.Should().NotBeNull();

        var deleteResponse = await _client.DeleteAsync($"/api/group/{createdGroup.Id}");
        deleteResponse.EnsureSuccessStatusCode();
    }
}
