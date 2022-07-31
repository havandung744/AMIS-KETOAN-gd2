using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<Entity> : ControllerBase
    {
      IBaseRepository<Entity> _baseRepository;
        IBaseService<Entity> _baseService;


        /// <summary>
        /// Thực hiện khởi tạo đối tượng
        /// </summary>
        /// <param name="baseRepository">khởi tạo đối tượng baseRepository</param>
        /// <param name="baseService">khởi tạo đối tượng baseService</param>
        /// CreatedBy: HVDUNG (20/06/2022)
        public MISABaseController(IBaseRepository<Entity> baseRepository, IBaseService<Entity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        /// <summary>
        /// thực hiện lấy ra danh sách đối tượng
        /// </summary>
        /// <returns>danh sách các đối tượng</returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entitys = await _baseRepository.GetAll();
            return Ok(entitys);
        }

        /// <summary>
        /// thực hiện lấy ra đối tượng dựa vào Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng đó</param>
        /// <returns>Thông tin đối tượng</returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpGet("{entityId}")]
        public async Task<IActionResult> Get(Guid entityId)
        {
            var entity = await _baseRepository.GetById(entityId);
            return Ok(entity);
        }

        /// <summary>
        /// thực hiện xóa đối tượng dựa vào Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng đó</param>
        /// <returns>
        /// 1 - xóa thành công
        /// 0 - xóa thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpDelete("{entityId}")]
        public async Task<IActionResult> Delete(Guid entityId)
        {
            var res = await _baseRepository.DeleteById(entityId);
            return Ok(res);
        }

        /// <summary>
        /// thực hiện thêm mới đối tượng
        /// </summary>
        /// <param name="entity">thông tin đối tượng thêm vào</param>
        /// <returns>
        /// 1 - thêm thành công
        /// 0 - thêm thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpPost]
        public async Task<IActionResult> Post(Entity entity)
        {
            // thêm mới vào database
            var res = await _baseService.InsertService(entity);
            return StatusCode(201, res);
        }

        /// <summary>
        /// thực hiện cập nhật đối tượng dựa vào Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng đó</param>
        /// <returns>
        /// 1 - cập nhật thành công
        /// 0 - cập nhật thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpPut("{entityId}")]
        public async Task<IActionResult> Put(Guid entityId, Entity entity)
        {
            var res = await _baseService.UpdateService(entityId, entity);
            return Ok(res);
        }
    }
}
