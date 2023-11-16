using System.Threading.Tasks;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface ICompanyService
{
    Task<EntityResultModel> AddCompany(int userId, int licanceId,string title, string address, string description, string webSite);
}