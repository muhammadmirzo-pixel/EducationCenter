using EducationCenter.Domain.Commons;

namespace EducationCenter.Domain.Entites;

public class Group : Auditable
{
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public string Name { get; set; }
    public ICollection<StudentGroup> StudentGroups { get; set; }
}
