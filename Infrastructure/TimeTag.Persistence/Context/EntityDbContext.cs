using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using TimeTag.Domain.Entities;

namespace TimeTag.Persistence.Context
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_LoginLog> User_LoginLogs { get; set; }
        public DbSet<User_Token> User_Tokens { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<Licance> Licances { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Company_Department> Company_Departments { get; set; }
        public DbSet<Company_Department_Employee> Department_Employees { get; set; }
        public DbSet<Company_Department_Employee_Bank> Employee_Banks { get; set; }
        public DbSet<Company_Department_Employee_Log> Employee_Logs { get; set; }
        public DbSet<Company_Department_Employee_Token> Employee_Tokens { get; set; }
        public DbSet<AppDomain> AppDomains { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SecurityCode> SecurityCodes { get; set; }

    }
}