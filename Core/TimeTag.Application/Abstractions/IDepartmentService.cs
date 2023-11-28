using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;

namespace TimeTag.Application.Abstractions;
public interface IDepartmentService
{
    Task<EntityResultModel> AddDepartment(int companyId, string name, string address, string description, string startJobTime, string finishJobTime);
    Task<EntityResultModel> UpdateDepartment(int departmentId, string name, string address, string description, string startJobTime, string finishJobTime);
    Task<DepartmentDTO> GetDepartment(int departmentId);
    Task<List<DepartmentDTO>> GetDepartments(int companyId);
    Task<int> GetDepartmentsCount(int companyId);
    Task<EntityResultModel> DeleteDepartment(int departmantId);
    bool IsDepartmentExist(int departmantId);
}