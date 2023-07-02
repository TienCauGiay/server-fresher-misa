using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Employees
{
    /// <summary>
    /// Thực thể nhân viên tìm kiếm phân trang DTO
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class FilterEmployeeDto
    {
        #region Property
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Số bản ghi trên trang hiện tại
        /// </summary>
        public int CurrentPageRecords { get; set; }
        /// <summary>
        /// Danh sách nhân viên DTO
        /// </summary>
        public List<EmployeeDto> Data { get; set; }
        #endregion

        #region Constructor
        public FilterEmployeeDto()
        {
            Data = new List<EmployeeDto>();
        }
        #endregion
    }
}
