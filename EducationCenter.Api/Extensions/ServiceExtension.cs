using EducationCenter.Data.IRepositories;
using EducationCenter.Data.Repositories;
using EducationCenter.Service.Interfaces;
using EducationCenter.Service.Services;

namespace EducationCenter.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentGroupService, StudentGroupService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ICourseService, CourseService>();
    }
}
