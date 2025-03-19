using EducationCenter.Service.DTOs.CourseDTOs;
using EducationCenter.Service.DTOs.GroupDTOs;
using EducationCenter.Service.DTOs.StudentGroupDTOs;
using System.Text.RegularExpressions;

namespace EducationCenter.Service.DTOs.StudentDTOs;

public class StudentForResultDto
{
    public long StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GroupForResultDto StudentGroups { get; set; }
}