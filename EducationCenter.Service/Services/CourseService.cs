using AutoMapper;
using EducationCenter.Data.IRepositories;
using EducationCenter.Domain.Entites;
using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.DTOs.StudentsGroup;
using EducationCenter.Service.Exceptions;
using EducationCenter.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace EducationCenter.Service.Services;

public class CourseService : ICourseService
{
    private readonly IRepository<Course> courseRepository;
    private readonly IMapper mapper;

    public CourseService(IRepository<Course> courseRepository, IMapper mapper)
    {
        this.courseRepository = courseRepository;
        this.mapper = mapper;
    }

    public async Task<CourseForResultDto> AddAsync(CourseForCreationDto dto)
    {
        var isCourseExist = await this.courseRepository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CourseName == dto.CourseName);
        if (isCourseExist != null) 
            throw new CustomException(409, "Course already exist");

        var course = this.mapper.Map<Course>(dto);
        course.CreatedAt = DateTime.UtcNow;
        
        var insertedCourse = await this.courseRepository.InsertAsync(course);
        await this.courseRepository.SaveChangeAsync();

        return this.mapper.Map<CourseForResultDto>(insertedCourse);
    }

    public async Task<IEnumerable<CourseForResultDto>> GetAllAsync()
    {
        var courses = await this.courseRepository.GetAll()
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<CourseForResultDto>>(courses);
    }

    public async Task<CourseForResultDto> GetByIdAsync(long id)
    {
        var course = await this.courseRepository.SelectByIdAsync(id);
        if (course == null)
            throw new CustomException(404, "Course not found");

        return this.mapper.Map<CourseForResultDto>(course);
    }

    public async Task<IEnumerable<CourseForResultDto>> GetByNameAsync(string name)
    {
        var courses = await this.courseRepository.GetAll()
            .AsNoTracking()
            .Where(c => c.CourseName.Contains(name))
            .ToListAsync();
        if (courses == null)
            throw new CustomException(404, "Course not found");

        return this.mapper.Map<IEnumerable<CourseForResultDto>>(courses);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var course = await this.courseRepository.SelectByIdAsync(id);
        if (course == null)
            throw new CustomException(404, "Course not found");

        await this.courseRepository.DeleteAsync(id);
        return await this.courseRepository.SaveChangeAsync();
    }

    public async Task<CourseForResultDto> UpdateAsync(long id, CourseForUpdateDto dto)
    {
        var course = await this.courseRepository.SelectByIdAsync(id);
        if (course is null)
            throw new CustomException(404, "Course not found");

        course.UpdatedAt = DateTime.UtcNow;
        var mapped = this.mapper.Map(dto, course);
        await this.courseRepository.SaveChangeAsync();

        return this.mapper.Map<CourseForResultDto>(mapped);
    }
}
