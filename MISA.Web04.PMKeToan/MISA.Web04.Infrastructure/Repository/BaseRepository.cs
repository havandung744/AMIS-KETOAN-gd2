using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MySqlConnector;

namespace MISA.Web04.Infrastructure.Repository
{
    public class BaseRepository<Entity>: IBaseRepository<Entity>
    {
        protected string ConnectionString;
        protected MySqlConnection SqlConnection;
        protected DynamicParameters Parameters = new DynamicParameters();

        public BaseRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("HVDUNG");
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <typeparam name="Entity">type của object</typeparam>
        /// <returns>Danh sách object</returns>
        /// CreatedBy: HVDUNG(15/06/2022)
        public async Task<IEnumerable<Entity>> GetAll()
        {
            var className = typeof(Entity).Name;
            //await
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var entities = await SqlConnection.QueryAsync<Entity>(sql: $"Proc_Get{className}s", commandType: System.Data.CommandType.StoredProcedure);
                return (IEnumerable<Entity>)entities;
            }
        }

        /// <summary>
        /// lấy dữ liệu theo bộ lọc
        /// </summary>
        /// <param name="pageSize"> số bản ghi/trang</param>
        /// <param name="pageIndex">trang hiện tại</param>
        /// <param name="employeeFilter">nội dung tìm kiếm</param>
        /// <param name="bankName">tên ngân hàng</param>
        /// <param name="gender">giới tính</param>
        /// <param name="departmentId">phòng ban</param>
        /// <param name="IsOrganizations">có phải là nhà cung cấp hay không</param>
        /// <returns>danh sách object</returns>
        /// CreatedBy: HVDUNG(01/08/2022)
        public async Task<IEnumerable<Entity>> GetAll(int? pageSize, int? pageIndex, string? employeeFilter, string? bankName, int? gender, Guid? departmentId, bool IsOrganizations)
        {
            var className = typeof(Entity).Name;
            Parameters.Add("@m_PageIndex", null);
            Parameters.Add("@m_PageSize", null);
            //employeeFilter = employeeFilter.Trim();
            Parameters.Add("@m_EmployeeFilter", employeeFilter);
            Parameters.Add("@m_BankName", bankName);
            Parameters.Add("@m_Gender", gender);
            Parameters.Add("@m_DepartmentId", departmentId);
            Parameters.Add("@m_IsOrganizations", IsOrganizations);
            Parameters.Add("@m_TotalRecords", direction: ParameterDirection.Output, dbType: DbType.Int32);
            Parameters.Add("@m_TotalPages", direction: ParameterDirection.Output, dbType: DbType.Int32);

            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var employeesPaging = await SqlConnection.QueryAsync<Entity>(
                     $"Proc_paging{className}",
                     param: Parameters,
                     commandType: CommandType.StoredProcedure
                 );

                return employeesPaging;
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="EntityId">id của đối tượng</param>
        /// <returns>thông tin đối tượng trả về</returns>
        /// CreateBy: HVDUNG(18/06/2022)
        public async Task<Entity> GetById(Guid EntityId)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @{className}Id";
                var sqlCommand = $"Proc_Get{className}ById";
                Parameters.Add($"@d_{className}Id", EntityId);
                var entity = await SqlConnection.QueryFirstOrDefaultAsync<Entity>(sql: sqlCommand, param: Parameters, commandType: System.Data.CommandType.StoredProcedure);
                // 4.trả kết quả cho cliend
                return entity;
            }
        }

        /// <summary>
        /// lấy Id tự động
        /// </summary>
        /// <returns>Id lớn nhất + 1 </returns>
        /// CreateBy: HVDUNG(18/06/2022)
        public async Task<object> GetNewEntityCode()
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"Proc_GetNew{className}Code";
                var newEntityCodes = await SqlConnection.QueryFirstOrDefaultAsync(sql: sqlCommand, commandType: System.Data.CommandType.StoredProcedure);
                // trả kết quả cho cliend
                return newEntityCodes.NewEmployeeCode;
            }
           
        }

        /// <summary>
        /// Xóa đối tượng theo Id
        /// </summary>
        /// <param name="EntityId">id của đối tượng</param>
        /// <returns>
        /// 0 -> xóa thất bại
        /// 1 -> xóa hành công
        /// </returns>
        /// CreatedBy: HVDUNG (18/06/2022)
        public async Task<int> DeleteById(Guid EntityId)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"Proc_Delete{className}ById";
                Parameters.Add($"@d_{className}Id", EntityId);
                var res = await SqlConnection.ExecuteAsync(sql: sqlCommand, param: Parameters, commandType: System.Data.CommandType.StoredProcedure);
                // 4.trả kết quả cho cliend
                return res;
            }
        }


        /// <summary>
        /// thêm mới đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>
        /// 0 -> thêm thất bại
        /// 1 -> thêm hành công
        /// </returns>
        /// CreatedBy: HVDUNG (18/06/2022)
        public async Task<int> Insert(Entity entity)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // 3. Thực hiện thêm mới dữ liệu vào database
                var sqlCommandText = $"Proc_Add{className}";
                var res = await SqlConnection.ExecuteAsync(sql: sqlCommandText, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                // 4. trả về thông tin cho client
                return res;
            }
        }

        /// <summary>
        /// Cập nhật thông tin đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <param name="entity">đối tượng</param>
        /// <returns>
        /// 0 -> cập nhật thất bại
        /// 1 -> cập nhật thành công
        /// </returns>
        /// CreatedBy: HVDUNG(18/06/2022)
        public async Task<int> Update(Guid entityId, Entity entity)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // 3. Thực hiện cập nhật dữ liệu
                var sqlCommandText = $"Proc_Update{className}";
                var res = await SqlConnection.ExecuteAsync(sql: sqlCommandText, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                // 4. trả về thông tin cho client
                return res;
            }
        }

        /// <summary>
        /// kiểm tra mã đối tượng có bị trùng lặp hay không
        /// </summary>
        /// <param name="entityCode">mã của đối tượng</param>
        /// <returns>
        /// true -> trùng lặp
        /// false -> không trùng lặp
        /// </returns>
        /// CreatedBy: HVDUNG(18/06/2022)
        public async Task<bool> CheckDuplicateCode(string entityCode)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCheck = $"select {className}Code from {className} where {className}Code=@{className}Code";
                Parameters.Add($"@{className}Code", entityCode);
                var res = await SqlConnection.QueryFirstOrDefaultAsync<string>(sql: sqlCheck, param: Parameters);
                if (res != null)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// lấy ra mã của đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>mã của đối tượng</returns>
        /// CreatedBy: HVDUNG(18/06/2022)
        public async Task<string> GetEntityCode(Guid entityId)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"select {className}Code from {className} where {className}Id = @{className}Id";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{className}Id",entityId);
                var res = await SqlConnection.QueryFirstOrDefaultAsync<string>(sql: sqlCommand, param: dynamicParameters);
                return res;
            }
        }

        /// <summary>
        /// Thực hiện xóa nhiều đối tượng
        /// </summary>
        /// <param name="entityIdList">danh sách id của nhân viên</param>
        /// <returns>
        /// 0: xóa thất bại
        /// số>0: số bản ghi đã xóa
        /// </returns>
        /// CreatedBy: HVDUNG(01/08/2022) 
        public async Task<int> DeleteMultiEntityById(string entityIdList)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Thực hiện xóa danh sách nhân viên được chọn dữ liệu
                var sqlCommandText = $"Proc_DeleteMulti{className}ById";
                Parameters.Add($"@m_String{className}Id", entityIdList);
                var res = await SqlConnection.ExecuteAsync(sql: sqlCommandText, param: Parameters, commandType: System.Data.CommandType.StoredProcedure);
                // trả về thông tin cho client
                return res;
            }
        }

    }
}
