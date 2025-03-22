using EducationCenter.Service.DTOs.Courses;

namespace EducationCenter.Service.DTOs.Groups;

public class GroupForResultDto
{
    public long GroupId { get; set; }
    public int GroupCount { get; set; }
    public string GroupName { get; set; }
    public CourseForResultDto CourseName { get; set; }
}