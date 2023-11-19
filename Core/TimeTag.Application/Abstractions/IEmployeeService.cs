using System.Threading.Tasks;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface IEmployeeService
{
    Task<EntityResultModel> AddEmployee(AddEmployeeDTO employeeModel);
}
