using EducationCenter.Service.DTOs.CourseDTOs;
using EducationCenter.Service.DTOs.StudentDTOs;

namespace EducationCenter.Service.DTOs.GroupDTOs;

public class GroupForResultDto
{
    public long GroupId { get; set; }
    public int GroupCount { get; set; }
    public string GroupName { get; set; }
    public CourseForResultDto CourseName { get; set; }
}