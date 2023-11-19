using System.Linq;
using TimeTag.Application.Abstractions;
using TimeTag.Application.Attr;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class PermissionService : IPermissionService
{
    private readonly EntityDbContext _context;
    public PermissionService(EntityDbContext context)
    {
        _context = context;
    }
    public bool HasUserThisPermissionInThisCompanyByTagName(int userId, int departmentId, int companyId, string permissionTagName)
    {
        if (userId <= 0) { return false; }        
        if(permissionTagName == PermissonTags.Company) return HasUserEditCompany(userId, companyId); 
        if(permissionTagName == PermissonTags.Department) return HasUserEditDepartment(userId, departmentId); 
        return false;
    }
    private bool HasUserEditCompany(int userId, int CompanyId)
    {        
        return _context.Users.Any(q=> q.Companies.Any(c=> c.Id == CompanyId) && q.Id == userId);
    }
    private bool HasUserEditDepartment(int userId, int departmentId)
    {
        return _context.Users.Any(q=> q.Companies.Any(c=> c.Departments.Any(c=> c.Id == departmentId)));
    }



}