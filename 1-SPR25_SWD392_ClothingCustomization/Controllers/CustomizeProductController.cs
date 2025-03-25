using _2_Service.Service;
using AutoMapper;
using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/customizeproducts")]
    [ApiController]
    public class CustomizeProductController : Controller
    {
        private readonly ICustomizeProductService _customizeProductService;
        private readonly IMapper _mapper;
        public CustomizeProductController(ICustomizeProductService customizeProductService, IMapper mapper)
        {
            _customizeProductService = customizeProductService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomizeProductResponseDTO>>> GetAll()
        {
            return Ok(await _customizeProductService.GetAllCustomizeProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomizeProduct>> GetById(int id)
        {
            var customizeProduct = await _customizeProductService.GetCustomizeProductById(id);
            if (customizeProduct == null)
                return NotFound();
            return Ok(customizeProduct);
        }

        //[HttpPost]
        //public async Task<ActionResult> Create([FromBody] CustomizeProduct customizeProduct)
        //{
        //    await _customizeProductService.AddCustomizeProduct(customizeProduct);
        //    return CreatedAtAction(nameof(GetById), new { id = customizeProduct.CustomizeProductId }, customizeProduct);
        //}
        // POST api/customizeproduct
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCustomizeProductDTO createCustomizeProductDTO)
        {
            // Kiểm tra dữ liệu hợp lệ
            if (createCustomizeProductDTO == null)
            {
                return BadRequest("Invalid data.");
            }

            // Ánh xạ DTO thành entity CustomizeProduct
            var customizeProduct = _mapper.Map<CustomizeProduct>(createCustomizeProductDTO);

            // Gọi service để lưu dữ liệu vào cơ sở dữ liệu
            await _customizeProductService.AddCustomizeProduct(customizeProduct);

            // Trả về kết quả khi tạo thành công
            return CreatedAtAction(nameof(GetById), new { id = customizeProduct.CustomizeProductId }, customizeProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CustomizeProduct customizeProduct)
        {
            if (id != customizeProduct.CustomizeProductId)
                return BadRequest();
            await _customizeProductService.UpdateCustomizeProduct(customizeProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _customizeProductService.DeleteCustomizeProduct(id);
            return NoContent();
        }
    }

}
