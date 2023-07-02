using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.ExcelServices
{
    /// <summary>
    /// Interface khai báo các phương thức chung
    /// </summary>
    /// <typeparam name="TImportExcelDto"></typeparam>
    /// <typeparam name="TExportExcelDto"></typeparam>
    /// Created By: BNTIEN (01/07/2023)
    public interface IBaseExcelService <TImportExcelDto, TExportExcelDto>
    {
        #region Methods chung
        /// <summary>
        /// Hàm nhập dữ liệu từ excel
        /// </summary>
        /// <param name="importExcelDtos"></param>
        /// <returns>Số bản ghi isert thành công</returns>
        /// Created By: BNTIEN (01/07/2023)
        int ImportExcel(List<TImportExcelDto> importExcelDtos);
        /// <summary>
        /// Hàm xuất dữ liệu ra excel
        /// </summary>
        /// <param name="exportExcelDtos"></param>
        /// <returns>Số bản ghi xuất thành công</returns>
        /// Created By: BNTIEN (01/07/2023)
        int ExportExcel(List<TExportExcelDto> exportExcelDtos); 
        #endregion
    }
}
