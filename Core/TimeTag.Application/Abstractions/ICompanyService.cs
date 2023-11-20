using System.Threading.Tasks;
using TimeTag.Application.DTO;
using Microsoft.AspNetCore.Http;
using System.IO;
using TimeTag.Domain.Entities;
using System.Collections.Generic;

namespace TimeTag.Application.Abstractions;
public interface ICompanyService
{
    Task<EntityResultModel> AddCompany(int userId, int licanceId,string title, string address, string description, string webSite);
    Task<EntityResultModel> UpdateCompany(int fileUploadId, int companyId,string title, string address, string description, string webSite);
    Task<CompanyDTO> GetCompany(int companyId);
    Task<List<CompanyDTO>> GetCompanies(int userId);
    bool IsCompanyExist(int companyId);
}