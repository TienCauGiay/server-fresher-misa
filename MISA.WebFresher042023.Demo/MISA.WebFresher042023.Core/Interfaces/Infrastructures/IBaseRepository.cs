using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Infrastructures
{
    /// <summary>
    /// Interface khai báo các phương thức có thể tái sử dụng được
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    public interface IBaseRepository<TEntity>
    {
        #region Method chung
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>danh sách entities</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<IEnumerable<TEntity>?> GetAllAsync();

        /// <summary>
        /// Lấy thông tin entities theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entities theo id</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<TEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy thông tin entities theo code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>entities theo code</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<TEntity?> GetByCodeAsync(string code);

        /// <summary>
        /// Thêm mới 1 entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi thêm</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Cập nhật thông tin entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi sửa</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<int> UpdateAsync(TEntity entity, Guid id);

        /// <summary>
        /// Xóa thực thể theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi xóa</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều thực thể theo các id tương ứng
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>số hàng bị ảnh hưởng sau khi xóa</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<int> DeleteMultipleAsync(List<Guid> ids);
        #endregion
    }
}
