using EducationCenter.Domain.Entites;
using EducationCenter.Service.DTOs.Courses;

namespace EducationCenter.Service.DTOs.Groups;

public class GroupForResultDto
{
    public long Id { get; set; }
    public string GroupName { get; set; }
    public CourseForResultDto CourseName { get; set; }
}