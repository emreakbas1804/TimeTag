using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Domain.Enums;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class EmployeeService : IEmployeeService
{
    private readonly EntityDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public EmployeeService(EntityDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    EntityResultModel entityResultModel = new();

    #region  Employee

    public async Task<EntityResultModel> AddEmployee(AddEmployeeDTO employeeModel)
    {
        try
        {
            Company_Department_Employee employee = new()
            {                
                rlt_Department_Id = employeeModel.DepartmentId,
                rlt_FileUpload_Id = employeeModel.rlt_FileUpload_Id,
                NameSurname = employeeModel.NameSurname,
                Title = employeeModel.Title,
                Phone = employeeModel.Phone,
                Address = employeeModel.Address,
                Email = employeeModel.Email,
                BirthDay = employeeModel.BirthDay,
                StartedJobTime = employeeModel.StartedJobTime,
                IsActive = true,
            };
            await _context.Department_Employees.AddAsync(employee);
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

    public async Task<EntityResultModel> UpdateEmployee(int departmentId, int employeeId, string nameSurname, string title, string phone, string address, string email, bool isActive, DateTime birthDay, DateTime startedJobTime, int rlt_FileUpload_Id)
    {
        try
        {
            var employeeEntity = await _context.Department_Employees.Where(q => q.Id == employeeId && q.IsActive).AsNoTracking().FirstOrDefaultAsync();
            if (employeeEntity == null)
            {
                entityResultModel.ResultMessage = "Kullanıcı bulunamadı.";
                return entityResultModel;
            }
            employeeEntity.rlt_Department_Id = departmentId;
            employeeEntity.NameSurname = nameSurname;
            employeeEntity.Title = title;
            employeeEntity.Phone = phone;
            employeeEntity.Address = address;
            employeeEntity.Email = email;
            employeeEntity.BirthDay = birthDay;
            employeeEntity.StartedJobTime = startedJobTime;
            employeeEntity.IsActive = isActive;
            employeeEntity.rlt_FileUpload_Id = rlt_FileUpload_Id == 0 ? null : rlt_FileUpload_Id;
            _context.Department_Employees.Update(employeeEntity);
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

    public async Task<EntityResultModel> GetEmployeesCompany(int companyId, int? departmentId)
    {
        try
        {
            var employeesQuery = _context.Department_Employees.Where(q => q.Department.Company.Id == companyId && q.IsActive);
            if (departmentId > 0 && departmentId != null)
            {
                employeesQuery = employeesQuery.Where(q => q.Department.Id == departmentId);
            }
            var employees = await employeesQuery.Select(c => new CompanyEmployeeDTO
            {
                Id = c.Id,
                NameSurname = c.NameSurname,
                Title = c.Title,
                Phone = c.Phone,
                Address = c.Phone,
                Email = c.Email,
                BirthDay = c.BirthDay,
                StartedJobTime = c.StartedJobTime,
                DepartmentName = c.Department.Name
            }).AsNoTracking().ToListAsync();
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = employees;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
            return entityResultModel;
        }

    }

    public async Task<EntityResultModel> GetEmployee(int employeeId)
    {
        try
        {
            var employee = await _context.Department_Employees.Where(q => q.Id == employeeId && q.IsActive).Select(c => new CompanyEmployeeDTO
            {
                Id = c.Id,
                NameSurname = c.NameSurname,
                Title = c.Title,
                Phone = c.Phone,
                Address = c.Address,
                Email = c.Email,
                BirthDay = c.BirthDay,
                StartedJobTime = c.StartedJobTime,
                DepartmentName = c.Department.Name,
                DepartmentId = c.Department.Id,
                ImageUrl = c.ProfileImage.FileUrl
            }).FirstOrDefaultAsync();
            if (employee != null)
            {
                entityResultModel.Result = EntityResult.Success;
                entityResultModel.ResultObject = employee;
            }
            entityResultModel.ResultMessage = "Personel bulunamadı";
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
            return entityResultModel;
        }
    }

    public async Task<int> GetEmployeesCompanyCount(int companyId, int? departmentId)
    {
        var query = _context.Department_Employees.Where(q => q.Department.Company.Id == companyId);
        if (departmentId > 0)
        {
            query = query.Where(q => q.Department.Id == departmentId);
        }
        var count = await query.CountAsync();
        return count;
    }
    #endregion


    #region  Employee-Bank

    public async Task<EntityResultModel> AddEmployeeBank(int employeeId, string bankName, string ownerName, string iban)
    {
        try
        {
            var isEmployeeExist = _context.Department_Employees.Any(q => q.Id == employeeId);
            if (!isEmployeeExist)
            {
                entityResultModel.ResultMessage = "Kullanıcı bulunamadı.";
                return entityResultModel;
            }

            Company_Department_Employee_Bank bank = new()
            {
                rlt_Employee_Id = employeeId,
                Name = bankName,
                OwnerName = ownerName,
                Iban = iban,
            };
            await _context.Employee_Banks.AddAsync(bank);
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

    public async Task<EntityResultModel> UpdateEmployeeBank(int bankId, string bankName, string ownerName, string iban)
    {
        try
        {
            var isBankExist = _context.Employee_Banks.Any(q => q.Id == bankId);
            if (!isBankExist)
            {
                entityResultModel.ResultMessage = "Banka bilgisi bulunamadı";
                return entityResultModel;
            }
            var bankEntity = await _context.Employee_Banks.Where(q => q.Id == bankId).FirstOrDefaultAsync();
            bankEntity.Name = bankName;
            bankEntity.OwnerName = ownerName;
            bankEntity.Iban = iban;
            _context.Employee_Banks.Update(bankEntity);
            await _context.SaveChangesAsync();
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu";
            return entityResultModel;
        }
    }

    public async Task<EntityResultModel> GetEmployeeBanks(int employeeId, int? bankId)
    {
        try
        {
            var bankQuery = _context.Employee_Banks.Where(q => q.Employee.Id == employeeId && q.Employee.IsActive);
            if (bankId > 0)
            {
                bankQuery = bankQuery.Where(q => q.Id == bankId);
            }

            var employeeBanks = await bankQuery.Select(c => new
            {
                Id = c.Id,
                EmployeeNameSurname = c.Employee.NameSurname,
                BankName = c.Name,
                OwnerName = c.OwnerName,
                Iban = c.Iban,
                RecordCreateTime = c.RecordCreateTime
            }).ToListAsync();
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = employeeBanks;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
            return entityResultModel;
        }
    }

    public async Task<EntityResultModel> DeleteEmployeeBank(int bankId)
    {
        try
        {
            var bankEntity = await _context.Employee_Banks.Where(q => q.Id == bankId).AsNoTracking().FirstOrDefaultAsync();
            if (bankEntity == null)
            {
                entityResultModel.ResultMessage = "Banak bilgisi bulunamadı.";
                return entityResultModel;
            }
            _context.Employee_Banks.Remove(bankEntity);
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

    #endregion


    #region  Employee-login-logout
    public async Task<EntityResultModel> AddLogEmployeeToJob(string token, LogType type)
    {
        try
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            var emploeeId = await _context.Employee_Tokens.Where(q => q.Token == token).Select(c => c.Employee.Id).FirstOrDefaultAsync();
            if (emploeeId == 0)
            {
                entityResultModel.ResultMessage = "Kullanıcı bulunamadı.";
                return entityResultModel;
            }
            Company_Department_Employee_Log loginJob = new()
            {
                ProcessTime = DateTime.Now,
                Type = type,
                IpAddress = ipAddress,
                Token = token,
                rlt_Employee_Id = emploeeId
            };
            await _context.Employee_Logs.AddAsync(loginJob);
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



    public async Task<EntityResultModel> GetLogsEmployee(int emploeeId, DateTime? startDate, DateTime? endDate, int page = 1, int count = 5)
    {
        try
        {
            var query = _context.Employee_Logs.Where(q=> q.rlt_Employee_Id == emploeeId);
            if(startDate != null ){
                query = query.Where(q=> q.RecordCreateTime > startDate);
            }
            if(endDate != null){
                endDate = endDate?.AddDays(1);
                query = query.Where(q=> q.RecordCreateTime < endDate);
            }
            var totalCount = query.Count();
            var logDetails = await query.Select(c=> new {
                Id = c.Id,
                processTime = c.ProcessTime,
                type = c.Type,
                nameSurname = c.Employee.NameSurname,
                isLatedToJob =  c.Employee.Department.StartJobTime != null && 
                                c.ProcessTime.TimeOfDay > TimeSpan.Parse(c.Employee.Department.StartJobTime)
            }).OrderBy(c=> c.Id).Skip((page - 1) * count).Take(count).ToListAsync();
            if (logDetails == null)
            {
                entityResultModel.ResultMessage = "Kullanıcının giriş bilgileri bulunamadı.";
                return entityResultModel;
            }
            dynamic logs = new {
                totalCount,
                logDetails
            };
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = logs;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
            return entityResultModel;
        }
    }





    #endregion


}