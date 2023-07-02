using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Exceptions;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Services
{
    /// <summary>
    /// Class triển khai các phương thức của entities employee
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class EmployeeService
        : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IMapper mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        #region Method riêng (Employee)
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong hệ thống
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            var codeMax = await _employeeRepository.GetByCodeMaxAsync();
            if (codeMax != null)
            {
                var maxLength = codeMax.Length - 3;
                var maxEmployeeCode = int.Parse(codeMax.Substring(3)) + 1;
                var newEmployeeCode = $"NV-{maxEmployeeCode.ToString().PadLeft(maxLength, '0')}";
                return newEmployeeCode;
            }
            return "";
        }

        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách nhân viên theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<FilterEmployeeDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            var filterEmployee = await _employeeRepository.GetFilterAsync(pageSize, pageNumber, textSearch);
            if (filterEmployee?.Data != null)
            {
                var filterEmployeeDto = _mapper.Map<FilterEmployeeDto>(filterEmployee);
                return filterEmployeeDto;
            }
            return null;
        }

        /// <summary>
        /// Hàm validate dữ liệu, khi thêm mới, sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listValidate"></param>
        /// <returns>true nếu dữ 1 trường dữ liệu nào đó k hợp lệ</returns>
        /// Created By: BNTIEN (17/06/2023)
        private async Task<bool> ValidateEmployee(Employee entity, Dictionary<string, string> listValidate)
        {
            if (await _departmentRepository.GetByIdAsync(entity.DepartmentId) == null)
            {
                listValidate.Add(Resources.ResourceVN.Text_DepartmentId, Resources.ResourceVN.Validate_NotExist_Department_Id);
            }

            if (listValidate.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Thêm 1 nhân viên mới
        /// </summary>
        /// <param name="employeeCreateDto"></param>
        /// <returns>Số hàng ảnh hưởng sau khi thêm</returns>
        /// <exception cref="ValidateException">Bắn ra lỗi nếu dữ liệu đầu vào không hợp lệ</exception>
        /// Created By: BNTIEN (17/06/2023)
        public override async Task<int> InsertAsync(EmployeeCreateDto employeeCreateDto)
        {
            var listValidate = new Dictionary<string, string>();

            var employee = _mapper.Map<Employee>(employeeCreateDto);

            bool checkThrow = await ValidateEmployee(employee, listValidate);

            var checkDuplicateCode = await _employeeRepository.GetByCodeAsync(employee.EmployeeCode);

            if (checkDuplicateCode != null)
            {
                listValidate.Add(Resources.ResourceVN.Text_EmployeeCode, Resources.ResourceVN.Validate_Exist_Employee_Code);
                checkThrow = true;
            }

            // Throw exception nếu dữ liệu không hợp lệ
            if (checkThrow)
            {
                throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, listValidate);
            }

            var res = await base.InsertAsync(employeeCreateDto);
            return res;
        }

        /// <summary>
        /// Cập nhật thông tin 1 nhân viên
        /// </summary>
        /// <param name="employeeUpdateDto"></param>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi sửa</returns>
        /// <exception cref="ValidateException">Bắn ra lỗi nếu dữ liệu đầu vào không hợp lệ</exception>
        /// Created By: BNTIEN (17/06/2023)
        public override async Task<int> UpdateAsync(EmployeeUpdateDto employeeUpdateDto, Guid id)
        {
            if (employeeUpdateDto.EmployeeId != id)
            {
                throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_NotMatch_Employee_Id, null);
            }
            var listValidate = new Dictionary<string, string>();

            var employee = _mapper.Map<Employee>(employeeUpdateDto);

            bool checkThrow = await ValidateEmployee(employee, listValidate);

            var checkDuplicateCode = await _employeeRepository.GetByCodeAsync(employee.EmployeeCode);

            if (checkDuplicateCode != null)
            {
                if (checkDuplicateCode.EmployeeCode != (await _employeeRepository.GetByIdAsync(id))?.EmployeeCode)
                {
                    listValidate.Add(Resources.ResourceVN.Text_EmployeeCode, Resources.ResourceVN.Validate_Exist_Employee_Code);
                    checkThrow = true;
                }
            }

            // Throw exception nếu dữ liệu không hợp lệ
            if (checkThrow)
            {
                throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, listValidate);
            }

            var res = await base.UpdateAsync(employeeUpdateDto, id);
            return res;
        }
        #endregion
    }
}
