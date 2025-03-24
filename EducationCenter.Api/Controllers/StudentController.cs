﻿using EducationCenter.Service.DTOs.Students;
using EducationCenter.Service.Interfaces;
using EducationCenter.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await this.studentService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] StudentForCreationDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var result = await this.studentService.AddAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentForResultDto>> GetByIdAsync(long id)
    {
        var result = await this.studentService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(long id, [FromBody] StudentForUpdateDto dto)
    {
        var result = await this.studentService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(long id)
    {
        var result = await this.studentService.RemoveAsync(id);
        return true;
    }
}
