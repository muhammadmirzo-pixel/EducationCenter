using EducationCenter.Service.DTOs.StudentsGroup;

namespace EducationCenter.Service.Interfaces;

public interface IStudentGroupService
{
    Task<bool> RemoveAsync(long id);
    Task<IQueryable<StudentGroupForResultDto>> GetAllAsync();
    Task<StudentGroupForResultDto> GetByIdAsync(long id);
    //Task<StudentGroupForResultDto> GetByNameAsync(string name);
    Task<StudentGroupForResultDto> AddAsync(StudentGroupForCreationDto dto);
    Task<StudentGroupForResultDto> UpdateAsync(long id, StudentGroupForUpdateDto dto);
}
