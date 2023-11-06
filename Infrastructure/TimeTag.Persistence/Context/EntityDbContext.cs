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
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<Licance> Licances { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Company_Department> Company_Departments { get; set; }
        public DbSet<Company_Employee> Company_Employees { get; set; }
        public DbSet<Company_EmployeeBank> Company_EmployeeBanks { get; set; }
        public DbSet<Company_EmployeeLoginJob> Company_EmployeeLoginJobs { get; set; }
        public DbSet<Company_EmployeeLogOutJob> Company_EmployeeLogOuts { get; set; }
        

    }
}