using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.ExcelEntities.Employees
{
    /// <summary>
    /// Thực thể (employee) xuất dữ liệu ra excel
    /// </summary>
    public class EmployeeExportExcel
    {
        #region Property
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string? GenderName { get; set; }
        /// <summary>
        /// Ngay sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Chức danh
        /// </summary>
        public string? PositionName { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string? BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranch { get; set; } 
        #endregion
    }
}
