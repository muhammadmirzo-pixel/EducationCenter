﻿using EducationCenter.Domain.Commons;
using EducationCenter.Domain.Enums;

namespace EducationCenter.Domain.Entites;

public class Student : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<StudentGroup> StudentGroups { get; set; }
    public Model Role { get; set; }
}
