using System;
using System.Threading.Tasks;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface IEmployeeService
{

    Task<EntityResultModel> AddEmployee(AddEmployeeDTO employeeModel);
    Task<EntityResultModel> UpdateEmployee(int departmentId,int employeeId,string nameSurname, string title, string phone, string address, string email,bool isActive, DateTime birthDay, DateTime startedJobTime);
    Task<EntityResultModel> GetEmployeesCompany(int companyId, int? departmentId);
    Task<EntityResultModel> GetEmployee(int employeeId);
    

    Task<EntityResultModel> AddEmployeeBank(int employeeId,string bankName,string ownerName, string iban);
    Task<EntityResultModel> UpdateEmployeeBank(int bankId, string bankName, string ownerName, string iban);
    Task<EntityResultModel> GetEmployeeBanks(int employeeId, int? bankId);
    Task<EntityResultModel> DeleteEmployeeBank(int bankId);


    Task<EntityResultModel> AddLoginEmployeeToJob(string token);
    Task<EntityResultModel> AddLogoutEmployeeToJob(string token);
    
    Task<EntityResultModel> GetLoginLogEmployee(int emploeeId);
    Task<EntityResultModel> GetLogoutLogEmployee(int emploeeId);

}
