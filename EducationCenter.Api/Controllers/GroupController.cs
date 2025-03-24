using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.Interfaces;
using EducationCenter.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService groupService;

    public GroupController(IGroupService groupService)
    {
        this.groupService = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await this.groupService.GetAllAsync();
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> PostCourse([FromBody] GroupForCreationDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var result = await this.groupService.AddAsync(dto);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<GroupForResultDto>> GetByIdAsync(long id)
    {
        var result = await this.groupService.GetByIdAsync(id);
        return Ok(result);
    }


    [HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        var result = await this.groupService.GetByNameAsync(name);
        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(long id, [FromBody] GroupForUpdateDto dto)
    {
        var result = await this.groupService.UpdateAsync(id, dto);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<bool> Delete(long id)
    {
        var result = await this.groupService.RemoveAsync(id);
        return true;
    }
}
