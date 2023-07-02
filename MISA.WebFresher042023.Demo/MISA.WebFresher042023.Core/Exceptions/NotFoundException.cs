using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Exceptions
{
    /// <summary>
    /// Exception đại diện cho lỗi không tìm thấy tài nguyên
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class NotFoundException : Exception
    {
        #region Property
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public string? ErrorMessage { get; set; }
        #endregion

        #region Constructor
        public NotFoundException() { }
        public NotFoundException(int errorCode, string? errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
