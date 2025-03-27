using EducationCenter.Service.DTOs.StudentsGroup;
using EducationCenter.Service.Services.Paginations;

namespace EducationCenter.Service.Interfaces;

public interface IStudentGroupService
{
    Task<bool> RemoveAsync(long id);
    Task<IEnumerable<StudentGroupForResultDto>> GetAllAsync(Pagination pagination);
    Task<StudentGroupForResultDto> GetByIdAsync(long id);
    //Task<IEnumerable<StudentGroupForResultDto>> GetByNameAsync(string name);
    Task<StudentGroupForResultDto> AddAsync(StudentGroupForCreationDto dto);
    Task<StudentGroupForResultDto> UpdateAsync(long id, StudentGroupForUpdateDto dto);
}
