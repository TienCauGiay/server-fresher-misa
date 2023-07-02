using MISA.WebFresher042023.Core.ExcelEntities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Excels
{
    public interface IEmployeeExcel : IBaseExcel<EmployeeImportExcel, EmployeeExportExcel>
    {

    }
}
