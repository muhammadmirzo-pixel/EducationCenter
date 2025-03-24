using EducationCenter.Domain.Entites;
using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.DTOs.StudentsGroup;

namespace EducationCenter.Service.DTOs.Groups;

public class GroupForResultDto
{
    public long Id { get; set; }
    public string GroupName { get; set; }
    public long CourseId { get; set; }
}