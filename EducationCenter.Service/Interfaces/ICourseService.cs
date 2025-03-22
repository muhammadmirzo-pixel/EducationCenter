using EducationCenter.Service.DTOs.Courses;

namespace EducationCenter.Service.Interfaces;

public interface ICourseService
{
    Task<bool> RemoveAsync(long id);
    Task<IQueryable<CourseForResultDto>> GetAllAsync();
    Task<CourseForResultDto> GetByIdAsync(long id);
    Task<CourseForResultDto> GetByNameAsync(string name);
    Task<CourseForResultDto> AddAsync(CourseForCreationDto dto);
    Task<CourseForResultDto> UpdateAsync(long id, CourseForUpdateDto dto);
}
