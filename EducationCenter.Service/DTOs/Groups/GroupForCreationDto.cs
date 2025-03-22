using EducationCenter.Service.DTOs.Courses;

namespace EducationCenter.Service.DTOs.Groups;

public class GroupForCreationDto
{ 
    public CourseForResultDto Course { get; set; } 
    public string GroupName { get; set; }
}
