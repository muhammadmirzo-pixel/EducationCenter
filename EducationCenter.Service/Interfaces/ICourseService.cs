using EducationCenter.Service.DTOs.Courses;

namespace EducationCenter.Service.Interfaces;

public interface ICourseService
{
    Task<bool> RemoveAsync(long id);
    Task<IEnumerable<CourseForResultDto>> GetAllAsync();
    Task<CourseForResultDto> GetByIdAsync(long id);
    Task<IEnumerable<CourseForResultDto>> GetByNameAsync(string name);
    Task<CourseForResultDto> AddAsync(CourseForCreationDto dto);
    Task<CourseForResultDto> UpdateAsync(long id, CourseForUpdateDto dto);
}
