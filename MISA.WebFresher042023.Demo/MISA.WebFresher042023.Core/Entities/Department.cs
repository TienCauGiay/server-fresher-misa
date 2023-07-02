using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    /// <summary>
    /// Thực thể Department
    /// </summary>
    /// Created By : BNTIEN (17/06/2023)
    public class Department : BaseEntity
    {
        /// <summary>
        /// Khai báo các Property của thực thể Department
        /// </summary>
        #region Property riêng (Department)
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion
    }
}
