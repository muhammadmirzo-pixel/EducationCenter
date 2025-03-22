using EducationCenter.Service.DTOs.Groups;

namespace EducationCenter.Service.DTOs.StudentsGroup;

public class StudentGroupForUpdateDto
{
    public GroupForResultDto GroupName { get; set; }
    public int StudentCount { get; set; }
}
