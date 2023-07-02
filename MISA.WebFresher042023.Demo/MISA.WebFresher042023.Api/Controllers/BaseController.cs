using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.Interfaces.Services;

namespace MISA.WebFresher042023.Api.Controllers
{
    /// <summary>
    /// Controller triển khai các phương thức chung
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    [ApiController]
    public abstract class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        protected readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;

        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }

        #region API chung
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách entities</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _baseService.GetAllAsync();
            return Ok(res);
        }

        /// <summary>
        /// Lấy dữ liệu theo code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>entities</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var res = await _baseService.GetByCodeAsync(code);
            return Ok(res);
        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entities</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _baseService.GetByIdAsync(id);
            return Ok(res);
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi thêm</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpPost]
        public async Task<IActionResult> Post(TEntityCreateDto entityCreateDto)
        {
            var res = await _baseService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created, res);
        }

        /// <summary>
        /// Cập nhật thông tin 1 bản ghi
        /// </summary>
        /// <param name="entityUpdateDto"></param>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi sửa</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(TEntityUpdateDto entityUpdateDto, Guid id)
        {
            var res = await _baseService.UpdateAsync(entityUpdateDto, id);
            return Ok(res);
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// Created By: BNTIEN (17/06/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _baseService.DeleteAsync(id);
            return Ok(res);
        }

        [HttpDelete("ids")]
        public async Task<IActionResult> DeleteMultiple(List<Guid> ids)
        {
            var res = await _baseService.DeleteMultipleAsync(ids);
            return Ok(res);
        }
        #endregion
    }
}
