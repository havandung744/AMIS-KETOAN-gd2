using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<Entity>
    {
        Task<IEnumerable<Entity>> GetAll();
        Task<IEnumerable<Entity>> GetAll(int? pageSize, int? pageIndex, string? employeeFilter, string? bankName, int? gender, Guid? departmentId, bool IsOrganizations);
        Task<Entity> GetById(Guid EntityId);
        Task<int> DeleteById(Guid EntityId);
        Task<int> Insert(Entity entity);
        Task<int> Update(Guid entityId, Entity entity);

        // thực hiện kiểm tra trùng mã đối tượng
        Task<bool> CheckDuplicateCode(string entitycode);
        
        // thực hiện lấy mã của đối tượng
        Task<string> GetEntityCode(Guid entytyId);
        // thực hiện tự động lấy mã đối tượng mới
        Task<object> GetNewEntityCode();
        Task<int> DeleteMultiEntityById(string entityIdList);
    }
}
