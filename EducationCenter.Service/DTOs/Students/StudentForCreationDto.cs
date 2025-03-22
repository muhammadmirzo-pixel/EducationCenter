namespace EducationCenter.Service.DTOs.Students;

public class StudentForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long GroupId { get; set; }
}
