using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Domain.Enums;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class CompanyService : ICompanyService
{
    private readonly EntityDbContext _context;

    public CompanyService(
        EntityDbContext context
    )
    {
        _context = context;

    }
    EntityResultModel entityResultModel = new();
    public async Task<EntityResultModel> AddCompany(int userId, int licanceId, string title, string address, string description, string webSite)
    {
        try
        {
            var isUserExist = _context.Users.Any(q => q.Id == userId);
            if (!isUserExist)
            {
                entityResultModel.ResultMessage = "İlgili kullanıcı bulunamadı.";
                return entityResultModel;
            }
            var licanceEntity = await _context.Licances.Where(q => q.Id == licanceId && q.IsAdded == false).FirstOrDefaultAsync();
            if (licanceEntity != null)
            {
                entityResultModel.ResultMessage = "İlgili kullanıcı bulunamadı.";
                return entityResultModel;
            }
            Company company = new()
            {
                Title = title,
                Address = address,
                Description = description,
                WebSite = webSite,
                rlt_User_Id = userId,
                rlt_Licance_Id = licanceId
            };
            await _context.Companies.AddAsync(company);
            licanceEntity.IsAdded = true;
            _context.Licances.Update(licanceEntity);
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

    public async Task<EntityResultModel> UpdateCompany(int fileUploadId, int companyId, string title, string address, string description, string webSite)
    {
        try
        {
            Company company = await _context.Companies.Where(q => q.Id == companyId).FirstOrDefaultAsync();
            if (company == null)
            {
                entityResultModel.ResultMessage = "Firma bulunamadı.";
                return entityResultModel;
            }


            company.Address = address;
            company.Title = title;
            company.Description = description;
            company.WebSite = webSite;
            company.rlt_FileUpload_Id = fileUploadId > 0 ? fileUploadId : company.rlt_FileUpload_Id;
            company.LastUpdateTime = DateTime.Now;             
            _context.Companies.Update(company);
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

    public async Task<CompanyDTO> GetCompany(int companyId)
    {
        CompanyDTO company ;
        company = await _context.Companies.Where(q=> q.Id == companyId).Select(q=> new CompanyDTO {
            Id = q.Id,
            RecordCreateTime = q.RecordCreateTime,            
            Title = q.Title,    
            Address = q.Address,
            WebSite = q.WebSite,
            Description = q.Description,
            ImageUrl = q.Logo.FileUrl
        }).FirstOrDefaultAsync();
        return company;
    }

    public async Task<List<CompanyDTO>> GetCompanies(int userId)
    {
        var userCompanies = await _context.Companies.Where(q=> q.Owner.Id == userId).Select(c=> new CompanyDTO {
            Id = c.Id,
            RecordCreateTime = c.RecordCreateTime,
            ImageUrl = c.Logo.FileUrl,
            Title = c.Title,
            Address = c.Address,
            Description = c.Description,
            WebSite= c.WebSite 
        }).ToListAsync();
        return userCompanies;
    }

    public bool IsCompanyExist(int companyId)
    {
        return _context.Companies.Any(q=> q.Id == companyId);
    }
}