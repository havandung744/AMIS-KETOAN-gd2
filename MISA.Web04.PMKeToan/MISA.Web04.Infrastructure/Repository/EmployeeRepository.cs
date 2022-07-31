﻿using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MISA.Web04.Infrastructure.Repository
{
    public class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }


        public async Task<int> DeleteMultiEmployeeById(string employeeIdList)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Thực hiện xóa danh sách nhân viên được chọn dữ liệu
                var sqlCommandText = $"Proc_DeleteMultiEmployeeById";
                Parameters.Add("@m_StringEmployeeId", employeeIdList);
                var res = await SqlConnection.ExecuteAsync(sql: sqlCommandText, param:Parameters, commandType: System.Data.CommandType.StoredProcedure);
                // trả về thông tin cho client
                return res;
            }
        }

        /// <summary>
        /// thực hiện phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi/trang</param>
        /// <param name="pageIndex">vị trí trang hiện tại</param>
        /// <param name="employeeFilter">nội dung tìm kiếm</param>
        /// <returns>các bản ghi</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// CreatedBy: HVDUNG(20/06/2022)

        public async Task<object> GetPaging(int? pageSize, int? pageIndex, string? employeeFilter, string? bankName, int? gender, Guid? departmentId, bool IsOrganizations)
        {
            Parameters.Add("@m_PageIndex", pageIndex);
            Parameters.Add("@m_PageSize", pageSize);
            Parameters.Add("@m_EmployeeFilter", employeeFilter);
            Parameters.Add("@m_BankName", bankName);
            Parameters.Add("@m_Gender", gender);
            Parameters.Add("@m_DepartmentId", departmentId);
            Parameters.Add("@m_IsOrganizations", IsOrganizations);
            Parameters.Add("@m_TotalRecords", direction: ParameterDirection.Output, dbType: DbType.Int32);
            Parameters.Add("@m_TotalPages", direction: ParameterDirection.Output, dbType: DbType.Int32);

            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var employeesPaging = await SqlConnection.QueryAsync<Employee>(
                     "Proc_pagingEmployee",
                     param: Parameters,
                     commandType: CommandType.StoredProcedure
                 );

                int totalRecords = Parameters.Get<int>("@m_TotalRecords");
                int totalPages = Parameters.Get<int>("@m_TotalPages");

                return new
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Data = employeesPaging,
                };
            }
        }

        /// <summary>
        /// Thực hiện cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns>
        /// 1 -> cập nhật thành công
        /// 0 -> cập nhật thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (18/06/2022)
        public override async Task<int> Update(Guid employeeId, Employee employee)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                employee.EmployeeId = employeeId;
                // 3. Thực hiện cập nhật dữ liệu
                var sqlCommandText = $"Proc_UpdateEmployee";
                var res = await SqlConnection.ExecuteAsync(sql: sqlCommandText, param: employee, commandType: System.Data.CommandType.StoredProcedure);
                // 4. trả về thông tin cho client
                return res;
            }
        }




    }
}
