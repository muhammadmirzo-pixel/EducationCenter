using AutoMapper;
using EducationCenter.Data.IRepositories;
using EducationCenter.Domain.Entites;
using EducationCenter.Service.DTOs.StudentsGroup;
using EducationCenter.Service.Exceptions;
using EducationCenter.Service.Interfaces;
using EducationCenter.Service.Services.Paginations;
using Microsoft.EntityFrameworkCore;

namespace EducationCenter.Service.Services;

public class StudentGroupService : IStudentGroupService
{
    private readonly IRepository<StudentGroup> studentGroupRepository;
    private readonly IMapper mapper;

    public StudentGroupService(IRepository<StudentGroup> studentGroupRepository, IMapper mapper)
    {
        this.studentGroupRepository = studentGroupRepository;
        this.mapper = mapper;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var studentGroup = await this.studentGroupRepository.SelectByIdAsync(id);
        if (studentGroup == null)
            throw new CustomException(404, "Student group not found");

        await this.studentGroupRepository.DeleteAsync(id);
        return await this.studentGroupRepository.SaveChangeAsync();
    }

    public async Task<IEnumerable<StudentGroupForResultDto>> GetAllAsync(Pagination pagination)
    {
        pagination ??= new Pagination
        {
            PageNumber = 1,
            PageSize = 10
        };

        var studentGroups = await this.studentGroupRepository.GetAll()
            .AsNoTracking()
            .OrderBy(sg => sg.Id)
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<StudentGroupForResultDto>>(studentGroups);
    }

    public async Task<StudentGroupForResultDto> GetByIdAsync(long id)
    {
        var studentGroup = await this.studentGroupRepository.SelectByIdAsync(id);
        if (studentGroup == null)
            throw new CustomException(404, "Student group not found");

        return this.mapper.Map<StudentGroupForResultDto>(studentGroup);
    }

    /*public async Task<IEnumerable<StudentGroupForResultDto>> GetByNameAsync(string name)
    {
        var studentGroup = await this.studentGroupRepository.GetAll()
            .AsNoTracking()
            .Where(g => g.)
            .OrderBy(Group => Group.Id)
            .ToListAsync();
        if (studentGroup == null)
            throw new CustomException(404, "Student group not found");

        return this.mapper.Map<IEnumerable<StudentGroupForResultDto>>(studentGroup);
    }*/

    public async Task<StudentGroupForResultDto> AddAsync(StudentGroupForCreationDto dto)
    {
        var isInfoExist = await this.studentGroupRepository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(sg => sg.GroupId == dto.GroupId && sg.StudentId == dto.StudentId);
        if (isInfoExist != null) throw new CustomException(409, "Student group already exist");

        var studentGroup = this.mapper.Map<StudentGroup>(dto);
        studentGroup.CreatedAt = DateTime.UtcNow;
        var insertedStudentGroup = await this.studentGroupRepository.InsertAsync(studentGroup);
        await this.studentGroupRepository.SaveChangeAsync();

        return this.mapper.Map<StudentGroupForResultDto>(insertedStudentGroup);
    }

    public async Task<StudentGroupForResultDto> UpdateAsync(long id, StudentGroupForUpdateDto dto)
    {
        var studentGroup = await this.studentGroupRepository.SelectByIdAsync(id);
        if (studentGroup == null)
            throw new CustomException(404, "Student group not found");
        
        studentGroup.UpdatedAt = DateTime.UtcNow;
        var mappedStudentGroup = this.mapper.Map(dto, studentGroup);
        await this.studentGroupRepository.SaveChangeAsync();

        return this.mapper.Map<StudentGroupForResultDto>(mappedStudentGroup);
    }
}
