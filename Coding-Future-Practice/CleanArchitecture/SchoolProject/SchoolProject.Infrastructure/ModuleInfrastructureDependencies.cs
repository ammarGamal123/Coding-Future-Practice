using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies
            (this IServiceCollection services)
        {
             
            services.AddTransient<IStudentRepository, StudentRepository>();
            return services;
        }

    }
}
