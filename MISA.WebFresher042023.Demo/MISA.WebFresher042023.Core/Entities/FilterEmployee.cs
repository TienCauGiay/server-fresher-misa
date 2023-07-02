using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Entities
{
    /// <summary>
    /// Thực thể nhân viên tìm kiếm phân trang
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)

    public class FilterEmployee
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
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }
        /// <summary>
        /// Hàm tạo
        /// </summary>
        #endregion

        #region Constructor
        public FilterEmployee()
        {
            Data = new List<Employee>();
        }
        #endregion
    }
}
