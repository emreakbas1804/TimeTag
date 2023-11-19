using System.Threading.Tasks;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Domain.Enums;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class EmployeeService : IEmployeeService
{
    private readonly EntityDbContext _context;
    public EmployeeService(EntityDbContext context)
    {
        _context = context;
    }
    EntityResultModel entityResultModel = new();
    public async Task<EntityResultModel> AddEmployee(AddEmployeeDTO employeeModel)
    {
        try
        {
            Company_Employee employee = new()
            {
                rlt_Company_Id = employeeModel.CompanyId,
                rlt_Department_Id = employeeModel.DepartmentId,
                NameSurname = employeeModel.NameSurname,
                Title = employeeModel.Title,
                Phone = employeeModel.Phone,
                Address = employeeModel.Address,
                Email = employeeModel.Email,
                BirthDay = employeeModel.BirthDay,
                StartedJobTime = employeeModel.StartedJobTime,
                IsActive = true,
            };
            await _context.Company_Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
            return entityResultModel;            
        }
    }
}