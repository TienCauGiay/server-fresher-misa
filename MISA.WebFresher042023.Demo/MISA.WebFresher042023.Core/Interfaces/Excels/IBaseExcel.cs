using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Excels
{
    /// <summary>
    /// Interface khai báo các phương thức chung
    /// </summary>
    /// <typeparam name="TImportExcel"></typeparam>
    /// <typeparam name="TExportExcel"></typeparam>
    /// Created By: BNTIEN (01/07/2023)
    public interface IBaseExcel <TImportExcel, TExportExcel>
    {
        #region Methods chung
        /// <summary>
        /// Hàm nhập dữ liệu từ excel
        /// </summary>
        /// <param name="importExcels"></param>
        /// <returns>Số bản ghi isert thành công</returns>
        /// Created By: BNTIEN (01/07/2023)
        int ImportExcel(List<TImportExcel> importExcels);
        /// <summary>
        /// Hàm xuất dữ liệu ra excel
        /// </summary>
        /// <param name="exportExcels"></param>
        /// <returns>Số bản ghi xuất thành công</returns>
        /// Created By: BNTIEN (01/07/2023)
        int ExportExcel(List<TExportExcel> exportExcels); 
        #endregion
    }
}
