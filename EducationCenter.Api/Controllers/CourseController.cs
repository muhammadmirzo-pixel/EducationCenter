using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.Interfaces;
using EducationCenter.Service.Services;
using EducationCenter.Service.Services.Paginations;
using Microsoft.AspNetCore.Mvc;

namespace EducationCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService courseService;

    public CourseController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        var result = await this.courseService.GetAllAsync(pagination);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> PostCourse([FromBody] CourseForCreationDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var result = await this.courseService.AddAsync(dto);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CourseForResultDto>> GetByIdAsync(long id)
    {
        var result = await this.courseService.GetByIdAsync(id);
        return Ok(result);
    }


    [HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        var result = await this.courseService.GetByNameAsync(name);
        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(long id, [FromBody] CourseForUpdateDto dto)
    {
        var result = await this.courseService.UpdateAsync(id, dto);
        return Ok(result);
    }


    [HttpDelete ("{id}")]
    public async Task<bool> Delete(long id)
    {
        var result = await this.courseService.RemoveAsync(id);
        return true;
    }
}
