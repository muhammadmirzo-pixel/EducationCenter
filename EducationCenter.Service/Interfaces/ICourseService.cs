using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.Services.Paginations;

namespace EducationCenter.Service.Interfaces;

public interface ICourseService
{
    Task<bool> RemoveAsync(long id);
    Task<IEnumerable<CourseForResultDto>> GetAllAsync(Pagination pagination);
    Task<CourseForResultDto> GetByIdAsync(long id);
    Task<IEnumerable<CourseForResultDto>> GetByNameAsync(string name);
    Task<CourseForResultDto> AddAsync(CourseForCreationDto dto);
    Task<CourseForResultDto> UpdateAsync(long id, CourseForUpdateDto dto);
}
