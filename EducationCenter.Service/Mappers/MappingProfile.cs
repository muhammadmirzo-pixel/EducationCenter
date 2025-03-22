using AutoMapper;
using EducationCenter.Domain.Entites;
using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.DTOs.Students;
using EducationCenter.Service.DTOs.StudentsGroup;

namespace EducationCenter.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentForCreationDto>().ReverseMap();
        CreateMap<Student, StudentForUpdateDto>().ReverseMap();
        CreateMap<Student, StudentForResultDto>().ReverseMap();

        CreateMap<Course, CourseForCreationDto>().ReverseMap();
        CreateMap<Course, CourseForUpdateDto>().ReverseMap();
        CreateMap<Course, CourseForResultDto>().ReverseMap();

        CreateMap<Group, GroupForCreationDto>().ReverseMap();
        CreateMap<Group, GroupForUpdateDto>().ReverseMap();
        CreateMap<Group, GroupForResultDto>().ReverseMap();

        CreateMap<StudentGroup, StudentGroupForCreationDto>().ReverseMap();
        CreateMap<StudentGroup, StudentGroupForUpdateDto>().ReverseMap();
        CreateMap<StudentGroup, StudentGroupForResultDto>().ReverseMap();
    }
}
