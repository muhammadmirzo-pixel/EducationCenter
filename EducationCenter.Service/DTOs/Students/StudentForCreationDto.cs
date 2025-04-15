using EducationCenter.Service.DTOs.StudentsGroup;
using System.ComponentModel.DataAnnotations;

namespace EducationCenter.Service.DTOs.Students;

public class StudentForCreationDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}
