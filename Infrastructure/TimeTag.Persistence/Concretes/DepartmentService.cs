using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Domain.Enums;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class DepartmentService : IDepartmentService
{
    private readonly EntityDbContext _context;
    public DepartmentService(EntityDbContext context)
    {
        _context = context;
    }
    EntityResultModel entityResultModel = new();
    public async Task<EntityResultModel> AddDepartment(int companyId, string name, string address, string description, string startJobTime, string finishJobTime)
    {
        try
        {
            var isCompanyExist = _context.Companies.Any(q => q.Id == companyId);
            if (!isCompanyExist)
            {
                entityResultModel.ResultMessage = "Firma bulunamadı.";
                return entityResultModel;
            }
            Company_Department department = new()
            {
                Name = name,
                Address = address,
                Description = description,
                StartJobTime = startJobTime,
                FinishJobTime = finishJobTime,
                rlt_Company_Id = companyId
            };
            await _context.Company_Departments.AddAsync(department);
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

    public async Task<EntityResultModel> UpdateDepartment(int departmentId, string name, string address, string description, string startJobTime, string finishJobTime)
    {
        try
        {
            Company_Department department = await _context.Company_Departments.Where(q => q.Id == departmentId).FirstOrDefaultAsync();
            if (department == null)
            {
                entityResultModel.ResultMessage = "Firma departmanı bulunamadı.";
                return entityResultModel;
            }
            department.Name = name;
            department.Address = address;
            department.Description = description;
            department.StartJobTime = startJobTime;
            department.FinishJobTime = finishJobTime;
            department.LastUpdateTime = DateTime.Now;
            _context.Company_Departments.Update(department);
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

    public async Task<DepartmentDTO> GetDepartment(int departmentId)
    {
        var departmant = await _context.Company_Departments.Where(q => q.Id == departmentId).Select(c => new DepartmentDTO
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Address = c.Address,
            StartJobTime = c.StartJobTime,
            FinishJobTime = c.FinishJobTime
        }).FirstOrDefaultAsync();
        return departmant;
    }

    public async Task<List<DepartmentDTO>> GetDepartments(int companyId)
    {
        var departmants = await _context.Company_Departments.Where(q => q.Company.Id == companyId).Select(c => new DepartmentDTO
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Address = c.Address,
            StartJobTime = c.StartJobTime,
            FinishJobTime = c.FinishJobTime
        }).ToListAsync();
        return departmants;
    }

    public async Task<EntityResultModel> DeleteDepartment(int departmantId)
    {
        try
        {
            Company_Department department = await _context.Company_Departments.Where(q=> q.Id == departmantId).FirstOrDefaultAsync();
            if(department == null)
            {
                entityResultModel.ResultMessage = "Departman bulunamadı.";
                return entityResultModel;
            }
            _context.Company_Departments.Remove(department);
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

    public bool IsDepartmentExist(int departmantId)
    {
        return _context.Company_Departments.Any(q=> q.Id == departmantId);
    }

    public async Task<int> GetDepartmentsCount(int companyId)
    {
        return await _context.Company_Departments.Where(q=> q.Company.Id == companyId).CountAsync();
    }
}