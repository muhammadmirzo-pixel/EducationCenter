using EducationCenter.Service.DTOs.CourseDTOs;

namespace EducationCenter.Service.DTOs.GroupDTOs;

public class GroupForCreationDto
{ 
    public CourseForResultDto Course { get; set; } 
    public string GroupName { get; set; }
}
