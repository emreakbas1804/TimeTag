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

        public DbSet<User> Users {get;set;}
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_LoginLog> User_LoginLogs { get; set; }
        public DbSet<User_Token> User_Tokens {get;set;}
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<Licance> Licances { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Company_Department> Company_Departments { get; set; }
        public DbSet<Company_Employee> Company_Employees { get; set; }
        public DbSet<Company_EmployeeBank> Company_EmployeeBanks { get; set; }
        public DbSet<Company_EmployeeLoginJob> Company_EmployeeLoginJobs { get; set; }
        public DbSet<Company_EmployeeLogOutJob> Company_EmployeeLogOutJobs { get; set; }
        public DbSet<Company_EmployeeToken> Company_EmployeeTokens {get;set;}
        

    }
}