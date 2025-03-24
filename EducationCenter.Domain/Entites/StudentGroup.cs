using EducationCenter.Domain.Commons;

namespace EducationCenter.Domain.Entites;

public class StudentGroup : Auditable
{
    public long StudentId { get; set; }
    //public ICollection<Student> StudentInformation { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
}