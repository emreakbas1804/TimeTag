using System.Linq;
using System.Threading.Tasks;
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
    public CompanyService(EntityDbContext context)
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
            var isLicanceExist = _context.Licances.Any(q => q.Id == licanceId);
            if (!isLicanceExist)
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
            await _context.SaveChangesAsync();
            entityResultModel.Result =EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
            return entityResultModel;
        }

    }
}