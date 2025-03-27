using EducationCenter.Service.DTOs.Students;
using EducationCenter.Service.Services.Paginations;

namespace EducationCenter.Service.Interfaces;

public interface IStudentService
{
    Task<bool> RemoveAsync(long id);
    Task<IEnumerable<StudentForResultDto>> GetAllAsync(Pagination pagination);
    Task<StudentForResultDto> GetByIdAsync(long id);
    Task<IEnumerable<StudentForResultDto>> GetByNameAsync(string name);
    Task<StudentForResultDto> AddAsync(StudentForCreationDto dto);
    Task<StudentForResultDto> UpdateAsync(long id, StudentForUpdateDto dto);
}
