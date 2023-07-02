using MISA.WebFresher042023.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    /// <summary>
    /// Thực thể nhân viên
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Khai báo các Property của thực thể nhân viên
        /// </summary>
        #region Property riêng (Employee)
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính (number)
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Giới tính (text)
        /// </summary>
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case (int?)E_Gender.Male: return Resources.ResourceVN.Gender_Male;
                    case (int?)E_Gender.Female: return Resources.ResourceVN.Gender_Female;
                    default: return Resources.ResourceVN.Gender_Other;
                }
            }
        }
        /// <summary>
        /// Mã phòng ban: Khóa ngoại liên kết với bảng Department
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp chứng minh nhân dân
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp chứng minh nhân dân
        /// </summary>
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Vị trí làm việc
        /// </summary>
        public string? PositionName { get; set; }
        /// <summary>
        /// Địa chỉ nhân viên
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? PhoneLandline { get; set; }
        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? BankBranch { get; set; }
        /// <summary>
        /// Là khách hàng 
        /// </summary>
        public int? IsCustomer  { get; set; }
        /// <summary>
        /// Là nhà cung cấp
        /// </summary>
        public int? IsProvider { get; set; }
        #endregion
    }
}
