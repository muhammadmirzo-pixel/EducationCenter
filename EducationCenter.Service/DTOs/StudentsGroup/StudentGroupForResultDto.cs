using EducationCenter.Service.DTOs.Groups;
using EducationCenter.Service.DTOs.Students;

namespace EducationCenter.Service.DTOs.StudentsGroup;

public class StudentGroupForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public long GroupId { get; set; }
}
