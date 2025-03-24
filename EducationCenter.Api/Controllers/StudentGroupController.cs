using EducationCenter.Service.DTOs.StudentsGroup;
using EducationCenter.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentGroupController : ControllerBase
{
    private readonly IStudentGroupService studentGroupService;

    public StudentGroupController(IStudentGroupService studentGroupService)
    {
        this.studentGroupService = studentGroupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await studentGroupService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await this.studentGroupService.GetByIdAsync(id);
        return Ok(result);
    }

    /*[HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        var result = await this.studentGroupService.GetByNameAsync(name);
        return Ok(result);
    }*/

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] StudentGroupForCreationDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var result = await studentGroupService.AddAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] StudentGroupForUpdateDto dto)
    {
        var result = await this.studentGroupService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var isDeleted = await this.studentGroupService.RemoveAsync(id);
        if (isDeleted)
            return Ok();

        return BadRequest("Failed to delete the student group.");
    }
}
