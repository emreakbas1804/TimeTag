using System.Threading.Tasks;
using System.Transactions;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface ILicanceService
{
    Task<string> AddLicance();
    Task<int> GetLicanceId(string serialNumber);
}