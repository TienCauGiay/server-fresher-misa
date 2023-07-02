using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.ExcelEntities.Employees;
using MISA.WebFresher042023.Core.Interfaces.Excels;
using MISA.WebFresher042023.Core.Interfaces.ExcelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Excels
{
    public class EmployeeExcelService : IEmployeeExcelSercive
    {
        #region Constructor (Tiem DI)
        private readonly IEmployeeExcel _employeeExcel;
        private readonly IMapper _mapper;

        public EmployeeExcelService(IEmployeeExcel employeeExcel, IMapper mapper)
        {
            _employeeExcel = employeeExcel;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm xuất dữ liệu ra excel
        /// </summary>
        /// <param name="exportExcelDtos"></param>
        /// <returns>Số hàng xuất ra thành công</returns>
        /// Created By: BNTIEN (01/07/2023)
        public int ExportExcel(List<EmployeeDto> exportExcelDtos)
        {
            var exportExcels = _mapper.Map<List<EmployeeExportExcel>>(exportExcelDtos);
            return _employeeExcel.ExportExcel(exportExcels);
        }

        /// <summary>
        /// Hàm nhập dữ liệu từ excel
        /// </summary>
        /// <param name="importExcelDtos"></param>
        /// <returns>Số hàng insert thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: BNTIEN (01/07/2023)
        public int ImportExcel(List<EmployeeCreateDto> importExcelDtos)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
