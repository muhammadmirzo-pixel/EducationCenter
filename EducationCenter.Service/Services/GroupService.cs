using AutoMapper;
using EducationCenter.Data.IRepositories;
using EducationCenter.Domain.Entites;
using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.Exceptions;
using EducationCenter.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationCenter.Service.Services;

public class GroupService(IRepository<Group> groupRepository, IRepository<Course> courseRepository, IMapper mapper) : IGroupService
{
    private readonly IRepository<Group> groupRepository = groupRepository;
    private readonly IRepository<Course> courseRepository = courseRepository;
    private readonly IMapper mapper = mapper;

    /*public GroupService
    {
        this.groupRepository = groupRepository;
        this.courseRepository = courseRepository;
        this.mapper = mapper;
    }*/

    public async Task<GroupForResultDto> AddAsync(GroupForCreationDto dto)
    {
        var checkCourse = await this.courseRepository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == dto.Course.CourseName);
        
        if (checkCourse == null) 
            throw new CustomException(404, "Course not found");
        
        var group = this.mapper.Map<Group>(dto);
        group.CreatedAt = DateTime.UtcNow;

        var insertedGroup = await this.groupRepository.InsertAsync(group);
        await this.groupRepository.SaveChangeAsync();

        return this.mapper.Map<GroupForResultDto>(insertedGroup);
    }

    public async Task<IQueryable<GroupForResultDto>> GetAllAsync()
    {
        var groups = await this.groupRepository.GetAll()
            .AsNoTracking()
            .OrderBy(g => g.Id)
            .ToListAsync();

        return this.mapper.Map<IQueryable<GroupForResultDto>>(groups);
    }

    public async Task<GroupForResultDto> GetByIdAsync(long id)
    {
        var group = await this.groupRepository.SelectByIdAsync(id);
        if (group == null)
            throw new CustomException(404, "Group not found");

        return this.mapper.Map<GroupForResultDto>(group);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var group = await this.groupRepository.SelectByIdAsync(id);
        if (group == null)
            throw new CustomException(404, "Group not found");

        await this.groupRepository.DeleteAsync(id);
        return await this.groupRepository.SaveChangeAsync();
    }

    public async Task<GroupForResultDto> UpdateAsync(long id, GroupForUpdateDto dto)
    {
        var group = await this.groupRepository.SelectByIdAsync(id);
        if (group == null)
            throw new CustomException(404, "Group not found");

        var mappedGroup = this.mapper.Map(dto, group);
        this.groupRepository.UpdateAsync(mappedGroup);
        await this.groupRepository.SaveChangeAsync();

        return this.mapper.Map<GroupForResultDto>(mappedGroup);
    }
}
