using EducationCenter.Domain.Commons;

namespace EducationCenter.Domain.Entites;


/// <summary>
/// this entity is course-name
/// </summary>
public class Course : Auditable
{
    public string Name { get; set; }
    public string Teacher { get; set; }
    public ICollection<Group> Groups { get; set; }
}
