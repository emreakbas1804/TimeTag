namespace TimeTag.Application.Abstractions;
public interface IPermissionService
{
    bool HasUserThisPermissionInThisCompanyByTagName(int userId, int departmentId, int companyId, string permissionTagName);
}