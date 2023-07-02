using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.Interfaces.ExcelServices;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeExcelsController : ControllerBase
    {
        IEmployeeExcelSercive _employeeExcelSercive;

        public EmployeeExcelsController(IEmployeeExcelSercive employeeExcelSercive)
        {
            _employeeExcelSercive = employeeExcelSercive;
        }

        [HttpPost("export")]
        public IActionResult Export(List<EmployeeDto> employees)
        {
            return Ok(_employeeExcelSercive.ExportExcel(employees));
        }
    }
}
