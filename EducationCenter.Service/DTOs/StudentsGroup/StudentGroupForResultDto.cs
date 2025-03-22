using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.DTOs.Students;

namespace EducationCenter.Service.DTOs.StudentsGroup;

public class StudentGroupForResultDto
{
    public GroupForResultDto Group { get; set; }
    public ICollection<StudentForResultDto> StudentList { get; set; }
}
