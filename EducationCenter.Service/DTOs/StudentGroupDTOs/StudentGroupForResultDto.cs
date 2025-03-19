using EducationCenter.Service.DTOs.GroupDTOs;
using EducationCenter.Service.DTOs.StudentDTOs;

namespace EducationCenter.Service.DTOs.StudentGroupDTOs;

public class StudentGroupForResultDto
{
    public GroupForResultDto Group { get; set; }
    public ICollection<StudentForResultDto> StudentList { get; set; }
}
