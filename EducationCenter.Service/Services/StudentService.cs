using AutoMapper;
using EducationCenter.Data.IRepositories;
using EducationCenter.Domain.Entites;
using EducationCenter.Service.DTOs.Students;
using EducationCenter.Service.Exceptions;
using EducationCenter.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationCenter.Service.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<Student> stRepository;
    private readonly IMapper mapper;

    public StudentService(IRepository<Student> stRepository, IMapper mapper)
    {
        this.stRepository = stRepository;
        this.mapper = mapper;
    }
    public async Task<StudentForResultDto> AddAsync(StudentForCreationDto dto)
    {
        var group = await this.stRepository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Email == dto.Email);

        if (group is not null)
            throw new CustomException(404, "group is already exist");

        var student = this.mapper.Map<Student>(dto);
        student.CreatedAt = DateTime.UtcNow;

        var insertedStudent = await this.stRepository.InsertAsync(student);
        await this.stRepository.SaveChangeAsync();

        return this.mapper.Map<StudentForResultDto>(insertedStudent);
    }

    public async Task<IEnumerable<StudentForResultDto>> GetAllAsync()
    {
        var studentsList = await this.stRepository.GetAll()
            .AsNoTracking()
            .Include(s => s.StudentGroups)
            .OrderBy(s => s.Id)
            .ToListAsync();

        return mapper.Map<IEnumerable<StudentForResultDto>>(studentsList);
    }

    public async Task<StudentForResultDto> GetByIdAsync(long id)
    {
        var student = await this.stRepository.SelectByIdAsync(id);
        if (student is null)
            throw new CustomException(404, "student not found");

        return this.mapper.Map<StudentForResultDto>(student);
    }

    public async Task<StudentForResultDto> GetByNameAsync(string name)
    {
        var student = await this.stRepository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.FirstName == name || s.LastName == name);

        if (student is null)
            throw new CustomException(404, "student not found");

        return this.mapper.Map<StudentForResultDto>(student);
    }


    public async Task<bool> RemoveAsync(long id)
    {
        var student = await this.stRepository.SelectByIdAsync(id);
        if (student is null)
            throw new CustomException(404, "student not found");

        await this.stRepository.DeleteAsync(id);
        return await this.stRepository.SaveChangeAsync();
    }

    public async Task<StudentForResultDto> UpdateAsync(long id, StudentForUpdateDto dto)
    {
        var student = await this.stRepository.SelectByIdAsync(id);
        if (student is null)
            throw new CustomException(404, "student not found");

        var mappedStudent = this.mapper.Map(dto, student);

        this.stRepository.UpdateAsync(mappedStudent);
        await this.stRepository.SaveChangeAsync();

        return this.mapper.Map<StudentForResultDto>(mappedStudent);
    }
}
