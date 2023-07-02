using AutoMapper;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Exceptions;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Services
{
    /// <summary>
    /// class triển khai các phương thức chung
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
        : IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;

        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        #region Method chung
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>danh sách entities</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<IEnumerable<TEntityDto>?> GetAllAsync()
        {
            var entities = await _baseRepository.GetAllAsync();
            if (entities != null)
            {
                var result = _mapper.Map<List<TEntityDto>>(entities);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Lấy thông tin entities theo code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>entities theo code</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<TEntityDto?> GetByCodeAsync(string code)
        {
            var entities = await _baseRepository.GetByCodeAsync(code);
            if (entities != null)
            {
                var entitiesDto = _mapper.Map<TEntityDto>(entities);
                return entitiesDto;
            }
            return default(TEntityDto);
        }

        /// <summary>
        /// Lấy thông tin entities theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entities theo id</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<TEntityDto?> GetByIdAsync(Guid id)
        {
            var entities = await _baseRepository.GetByIdAsync(id);
            if (entities != null)
            {
                var entitiesDto = _mapper.Map<TEntityDto>(entities);
                return entitiesDto;
            }
            return default(TEntityDto);
        }

        /// <summary>
        /// Thêm mới 1 entities
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi thêm</returns>
        /// Created By: BNTIEN (17/06/2023)
        public virtual async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var entities = _mapper.Map<TEntity>(entityCreateDto);

            var res = await _baseRepository.InsertAsync(entities);
            return res;
        }

        /// <summary>
        /// Cập nhật thông tin entities
        /// </summary>
        /// <param name="entityUpdateDto"></param>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi sửa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public virtual async Task<int> UpdateAsync(TEntityUpdateDto entityUpdateDto, Guid id)
        {
            var entities = _mapper.Map<TEntity>(entityUpdateDto);

            var res = await _baseRepository.UpdateAsync(entities, id);
            return res;
        }

        /// <summary>
        /// Xóa thực thể theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi xóa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            var entitiesDelete = await _baseRepository.GetByIdAsync(id);
            if (entitiesDelete != null)
            {
                var res = await _baseRepository.DeleteAsync(id);
                return res;
            }
            return 0;
        }

        /// <summary>
        /// Xóa nhiều thực thể theo các id tương ứng
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi xóa</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var res = await _baseRepository.DeleteMultipleAsync(ids);
            return res;
        }
        #endregion
    }
}
