using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<object> GetPaging(int? pageSize, int? pageIndex, string? textSearch, string? bankName, int? gender, Guid? departmentId, bool IsOrganizations);
        Task<int> DeleteMultiEmployeeById(string employeeIdList);
    }
}
