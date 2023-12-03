using System;
using System.Threading.Tasks;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;

namespace TimeTag.Application.Abstractions;
public interface IEmployeeService
{

    Task<EntityResultModel> AddEmployee(AddEmployeeDTO employeeModel);
    Task<EntityResultModel> UpdateEmployee(int departmentId,int employeeId,string nameSurname, string title, string phone, string address, string email,bool isActive, DateTime birthDay, DateTime startedJobTime, int rlt_FileUpload_Id);
    Task<EntityResultModel> GetEmployeesCompany(int companyId, int? departmentId);
    Task<int> GetEmployeesCompanyCount(int companyId, int? departmentId);
    Task<EntityResultModel> GetEmployee(int employeeId);
    

    Task<EntityResultModel> AddEmployeeBank(int employeeId,string bankName,string ownerName, string iban);
    Task<EntityResultModel> UpdateEmployeeBank(int bankId, string bankName, string ownerName, string iban);
    Task<EntityResultModel> GetEmployeeBanks(int employeeId, int? bankId);
    Task<EntityResultModel> DeleteEmployeeBank(int bankId);


    Task<EntityResultModel> AddLogEmployeeToJob(string token,LogType type);    
    
    Task<EntityResultModel> GetLogsEmployee(int emploeeId);    

}
