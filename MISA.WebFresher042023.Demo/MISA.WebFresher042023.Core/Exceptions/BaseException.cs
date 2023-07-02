using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Exceptions
{
    /// <summary>
    /// Khai báo cấu trúc tổng quan của 1 exception
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class BaseException
    {
        #region Propeties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Thông báo cho Dev
        /// </summary>
        public string? DevMessage { get; set; }
        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string? UserMessage { get; set; }
        /// <summary>
        /// Truy vết lỗi
        /// </summary>
        public string? TraceId { get; set; }
        /// <summary>
        /// Thông tin thêm về lỗi
        /// </summary>
        public string? MoreInfo { get; set; }
        /// <summary>
        /// Dữ liệu thông báo lỗi (nếu có)
        /// </summary>
        public Object? Data { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// chuyển đổi đối tượng thành json
        /// </summary>
        /// <returns>đối tượng dưới dangj json</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion
    }
}
