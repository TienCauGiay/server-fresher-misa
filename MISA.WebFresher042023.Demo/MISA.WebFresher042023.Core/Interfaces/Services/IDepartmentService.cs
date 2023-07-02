using MISA.WebFresher042023.Core.DTO.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    /// <summary>
    /// Interface department service
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        #region Method riêng (Department)
        /// <summary>
        /// Tìm kiếm phòng ban theo tên
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<IEnumerable<DepartmentDto>?> GetByName(string? textSearch);
        #endregion
    }
}
