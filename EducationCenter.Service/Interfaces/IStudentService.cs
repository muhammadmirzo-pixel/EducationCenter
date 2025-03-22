using EducationCenter.Service.DTOs.Students;

namespace EducationCenter.Service.Interfaces;

public interface IStudentService
{
    Task<bool> RemoveAsync(long id);
    Task<IQueryable<StudentForResultDto>> GetAllAsync();
    Task<StudentForResultDto> GetByIdAsync(long id);
    Task<StudentForResultDto> GetByNameAsync(string name);
    Task<StudentForResultDto> AddAsync(StudentForCreationDto dto);
    Task<StudentForResultDto> UpdateAsync(long id, StudentForUpdateDto dto);
}
