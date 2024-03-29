﻿using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MISA.Web04.Core.Resources;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;

namespace MISA.Web04.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// Thực hiện validate dữ liệu cho Employee
        /// </summary>
        /// <param name="employee">đối tượng employee</param>
        /// <returns>
        /// true - validate thành công
        /// false - validate thất bại
        /// </returns>
        /// CreateBy: HVDUNG (20/06/2022)
        protected override async Task<bool> Validate(Employee employee)
        {
            // 1.1 thông tin mã nhân viên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {string text = "EmployeeCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            // 1.2 thông tin họ tên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {string text = "EmployeeName";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            // số chứng minh thư nhân dân phải đúng định dạng
            if (!string.IsNullOrEmpty(employee.IdentityNumber) && !CheckIndentityNumber(employee.IdentityNumber))
            {
                ErrorData.Add("IdentityNumber", Resources.ResourceVN.IdentityNumber);
            }

            //// 1.3 thông tin email phải đúng định dạng
            if (!string.IsNullOrEmpty(employee.Email) && !IsValidEmail(employee.Email))
            {
                ErrorData.Add("Email", Resources.ResourceVN.checkValidateEmail);
            }

            //// 1.4 ngày sinh không được lớn hơn ngày hiện tại
            if (employee.DateOfBirth != null && !CheckDateOfBirth((DateTime)employee.DateOfBirth))
            {
                ErrorData.Add("DateOfBirth", Resources.ResourceVN.checkValidateDateOfBirth);
            }
            // 1.5 mã nhân viên không được phép trùng lặp
            if (employee.EmployeeCode != null)
            {
                var isDuplicate =  await _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
                if (isDuplicate == true)
                    ErrorData.Add("EmployeeCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "EmployeeCode"));
            }

            // 1.6 nhân viên phải thuộc một phòng ban xác định (có DepartmentId)
            if (employee.DepartmentId == Guid.Empty)
            {
                string text = "DepartmentId";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            //if (employee.PositionId == Guid.Empty)
            //{
            //    string text = "PositionId";
            //    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            //}

            //1.7 chức danh không được phép để trống
            if (string.IsNullOrEmpty(employee.EmployeePosition))
            {
                string text = "EmployeePosition";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }

            //1.8 các thông tin khi là nhà cung cấp không được phép để trống
            if (employee.IsOrganizations)
            {
                //1.8.1 tên đơn vị không được phép để trống
                if (string.IsNullOrEmpty(employee.OrganizationName))
                {
                    string text = "OrganizationName";
                    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                }
                //1.8.2 mã số thuế không được phép để trống và phải đúng định dạng
                if (string.IsNullOrEmpty(employee.TaxCode) || !checkTaxCode(employee.TaxCode))
                {
                    string text = "TaxCode";
                    if(string.IsNullOrEmpty(employee.TaxCode))
                    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                    else
                    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.TaxCodeNumber, text));
                }
                //1.8.3 địa chỉ đơn vị không được phép để trống
                if (string.IsNullOrEmpty(employee.OrganizationAddress))
                {
                    string text = "OrganizationAddress";
                    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                }
            }

            if (ErrorData.Count > 0) return false;
            else return true;
        }

        /// <summary>
        /// Thực hiện validate dữ liệu cho Employee khi cập nhật
        /// </summary>
        /// <param name="employee">đối tượng employee</param>
        /// <returns>
        /// true - validate thành công
        /// false - validate thất bại
        /// </returns>
        /// CreateBy: HVDUNG (20/06/2022)
        protected override async Task<bool> ValidateForUpdate(Guid employeeId, Employee employee)
        {        
            // 1.1 thông tin mã nhân viên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                string text = "EmployeeCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                return false;
            }
            // kiểm tra mã nhân viên có bị trùng lặp hay không (không tính chính nó)
            var getEmployeeCode = await _employeeRepository.GetEntityCode(employeeId);
            var isDuplicate = await _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
            if (getEmployeeCode != employee.EmployeeCode && isDuplicate == true)
                ErrorData.Add("EmployeeCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "EmployeeCode"));


            // 1.2 thông tin họ tên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                string text = "EmployeeName";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }

            // số chứng minh thư nhân dân phải đúng định dạng
            if (!string.IsNullOrEmpty(employee.IdentityNumber) && !CheckIndentityNumber(employee.IdentityNumber))
            {
                ErrorData.Add("IdentityNumber", Resources.ResourceVN.IdentityNumber);
            }
            //// 1.3 thông tin email phải đúng định dạng
            if (!string.IsNullOrEmpty(employee.Email) && !IsValidEmail(employee.Email))
            {
                ErrorData.Add("Email", Resources.ResourceVN.checkValidateEmail);
            }

            //// 1.4 ngày sinh không được lớn hơn ngày hiện tại
            if (employee.DateOfBirth != null && !CheckDateOfBirth((DateTime)employee.DateOfBirth))
            {
                ErrorData.Add("DateOfBirth", Resources.ResourceVN.checkValidateDateOfBirth);
            }

            // 1.6 nhân viên phải thuộc một phòng ban xác định (có DepartmentId)
            if (employee.DepartmentId == Guid.Empty)
            {
                string text = "DepartmentId";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            //if (employee.PositionId == Guid.Empty)
            //{   string text = "PositionId";
            //    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            //}

            //1.7 chức danh không được phép để trống
            if (string.IsNullOrEmpty(employee.EmployeePosition))
            {
                string text = "EmployeePosition";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }

            //1.8 các thông tin khi là nhà cung cấp không được phép để trống
            if (employee.IsOrganizations)
            {
                //1.8.1 tên đơn vị không được phép để trống
                if (string.IsNullOrEmpty(employee.OrganizationName))
                {
                    string text = "OrganizationName";
                    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                }
                //1.8.2 mã số thuế không được phép để trống và phải đúng định dạng
                if (string.IsNullOrEmpty(employee.TaxCode) || !checkTaxCode(employee.TaxCode))
                {
                    string text = "TaxCode";
                    if (string.IsNullOrEmpty(employee.TaxCode))
                        ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                    else
                        ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.TaxCodeNumber, text));
                }
                //1.8.3 địa chỉ đơn vị không được phép để trống
                if (string.IsNullOrEmpty(employee.OrganizationAddress))
                {
                    string text = "OrganizationAddress";
                    ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                }
            }




            if (ErrorData.Count > 0) return false;
            else return true;
        }


        /// <summary>
        /// Thực hiện kiểm tra Email có đúng định dạng không
        /// </summary>
        /// <param name="email"> email </param>
        /// <returns>
        /// true - email đúng định dạng
        /// false - email không đúng định dạng
        /// </returns>
        /// CreateBy: HVDUNG (09/06/2022)
        /// </summary>
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Thực hiện kiểm tra ngày sinh có lớn hơn ngày hiện tại không
        /// </summary>
        /// <param name="DateOfBirth"> ngày sinh </param>
        /// <returns>
        /// true - ngày sinh không lớn hơn ngày hiện tại
        /// false - ngày sinh lớn hơn ngày hiện tại
        /// </returns>
        /// CreateBy: HVDUNG (09/06/2022)
        bool CheckDateOfBirth(DateTime? DateOfBirth)
        {
            if (DateOfBirth > DateTime.Now)
                return false;
            return true;
        }

        /// <summary>
        /// Thực hiện kiểm tra số chứng minh thư nhân dân đã đúng định dạng hay chưa
        /// </summary>
        /// <param name="IdentityNumber">số chứng minh thư nhân dân</param>
        /// <returns>
        /// true: đúng định dạng
        /// false: sai định dạng
        /// </returns>
        /// Author: HVDUNG(02/08/2022)
        bool CheckIndentityNumber(string? IdentityNumber)
        {
            long n;
            bool isNumeric = long.TryParse(IdentityNumber, out n);
            if (IdentityNumber.Length==12 && isNumeric)
                return true;
            return false;
        }

        /// <summary>
        /// Thực hiện kiểm tra xem một chuỗi string có phải là dạng số không
        /// </summary>
        /// <param name="number">Chuỗi số</param>
        /// <returns>
        /// true: là chuỗi số
        /// false: không là chuỗi số
        /// </returns>
        /// Author: HVDUNG(03/08/2022)
        bool CheckNumber(string? number)
        {
            long n;
            bool isNumeric = long.TryParse(number, out n);
            if (isNumeric)
                return true;
            return false;
        }

        /// <summary>
        /// Thực hiện kiểm tra taxCode có đúng định dạng không
        /// </summary>
        /// <param name="taxCode">mã số thuế</param>
        /// <returns>
        /// true: đúng định dạng
        /// false: không đúng định dạng
        /// </returns>
        /// Author: HVDUNG(03/08/2022)
        bool checkTaxCode(string taxCode)
        {
            if (taxCode.Length != 14)
                return false;
            else
            {
                string temp_start = taxCode.Substring(0,10);
                string temp_end = taxCode.Substring(11);
                char temp_space = taxCode[10];
                if (!this.CheckNumber(temp_start) || !this.CheckNumber(temp_end) || temp_space != '-')
                    return false;
                return true;
            }

        }

    }
}
