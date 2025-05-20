using EducationCenter.Service.DTOs.Courses;
using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.DTOs.StudentsGroup;
using System.Text.RegularExpressions;

namespace EducationCenter.Service.DTOs.Students;

public class StudentForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public ICollection<StudentGroupForResultDto> StudentGroups { get; set; }
}