using EducationCenter.Service.Interfaces;
using EducationCenter.Service.Services;

namespace EducationCenter.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this ServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentGroupService, StudentGroupService>();
        services.AddScoped<>();
    }
}
