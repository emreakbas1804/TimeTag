using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface ILicanceService
{
    Task<string> AddLicance(List<string> tokens);
    Task<int> GetLicanceId(string serialNumber);
}