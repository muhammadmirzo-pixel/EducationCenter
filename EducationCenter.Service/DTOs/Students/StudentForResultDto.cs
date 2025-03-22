using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.DTOs.StudentsGroup;
using System.Text.RegularExpressions;

namespace EducationCenter.Service.DTOs.Students;

public class StudentForResultDto
{
    public long StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GroupForResultDto StudentGroups { get; set; }
}