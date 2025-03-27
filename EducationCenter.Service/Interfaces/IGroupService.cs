using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.Services.Paginations;

namespace EducationCenter.Service.Interfaces;

public interface IGroupService
{
    Task<bool> RemoveAsync(long id);
    Task<IEnumerable<GroupForResultDto>> GetAllAsync(Pagination pagination);
    Task<GroupForResultDto> GetByIdAsync(long id);
    Task<IEnumerable<GroupForResultDto>> GetByNameAsync(string name);
    Task<GroupForResultDto> AddAsync(GroupForCreationDto dto);
    Task<GroupForResultDto> UpdateAsync(long id, GroupForUpdateDto dto);
}
