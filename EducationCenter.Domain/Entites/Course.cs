using EducationCenter.Domain.Commons;

namespace EducationCenter.Domain.Entites;

public class Course : Auditable
{
    public string Name { get; set; }
    public ICollection<Group> Groups { get; set; }
}
