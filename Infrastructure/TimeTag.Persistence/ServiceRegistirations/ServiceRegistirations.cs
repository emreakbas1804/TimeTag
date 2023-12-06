using System;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTag.Application.Abstractions;
using TimeTag.Persistence.Concretes;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.ServiceRegistirations
{
    public static class ServiceRegistirations
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ILicanceService, LicanceService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
        }
    }
}