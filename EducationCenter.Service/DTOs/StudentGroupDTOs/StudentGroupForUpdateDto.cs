using EducationCenter.Service.DTOs.GroupDTOs;

namespace EducationCenter.Service.DTOs.StudentGroupDTOs;

public class StudentGroupForUpdateDto
{
    public GroupForResultDto GroupName { get; set; }
    public int StudentCount { get; set; }
}
