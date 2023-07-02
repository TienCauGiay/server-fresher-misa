using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Core.DTO.Employees.CustomValidate;
using MISA.WebFresher042023.Core.Resources;

namespace MISA.WebFresher042023.Core.DTO.Employees
{
    /// <summary>
    /// Lớp chuyển đổi đối tượng employee thêm mới, bỏ đi 1 số trường không cần thiết
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class EmployeeCreateDto
    {
        #region Property
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_NotNull_Employee_Code))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_Code))]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_NotNull_Employee_FullName))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_FullName))]
        public string FullName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [ValidateDatetime("Validate_Invalid_DOB")]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính (number)
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Mã phòng ban: Khóa ngoại liên kết với bảng Department
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_NotNull_Employee_Department))]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_NotNull_Employee_Department))]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        [MaxLength(25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_IdentityNumber))]
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp chứng minh nhân dân
        /// </summary>
        [ValidateDatetime("Validate_Invalid_IdentityDate")]
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp chứng minh nhân dân
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_IdentityPlace))]
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Vị trí làm việc
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_PositionName))]
        public string? PositionName { get; set; }
        /// <summary>
        /// Địa chỉ nhân viên
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_Address))]
        public string? Address { get; set; }
        /// <summary>
        /// Điện thoại di động
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_PhoneNumber))]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        [MaxLength( 50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_PhoneLandline))]
        public string? PhoneLandline { get; set; }
        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_Email))]
        [ValidateEmail(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_Email_ErrorFormat))]
        public string? Email { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_BankAccount))]
        public string? BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_BankName))]
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_MaxLength_Employee_BankBranch))]
        public string? BankBranch { get; set; }
        /// <summary>
        /// Là khách hàng 
        /// </summary>
        public int? IsCustomer { get; set; }
        /// <summary>
        /// Là nhà cung cấp
        /// </summary>
        public int? IsProvider { get; set; }
        #endregion
    }
}
