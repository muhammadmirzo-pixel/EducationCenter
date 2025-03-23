using EducationCenter.Service.DTOs.Groups;

namespace EducationCenter.Service.Interfaces;

public interface IGroupService
{
    Task<bool> RemoveAsync(long id);
    Task<IEnumerable<GroupForResultDto>> GetAllAsync();
    Task<GroupForResultDto> GetByIdAsync(long id);
    Task<IEnumerable<GroupForResultDto>> GetByNameAsync(string name);
    Task<GroupForResultDto> AddAsync(GroupForCreationDto dto);
    Task<GroupForResultDto> UpdateAsync(long id, GroupForUpdateDto dto);
}
