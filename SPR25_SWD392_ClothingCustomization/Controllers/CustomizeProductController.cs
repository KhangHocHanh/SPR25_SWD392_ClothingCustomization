using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Service;

namespace SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/customizeproducts")]
    [ApiController]
    public class CustomizeProductController : Controller
    {
        private readonly ICustomizeProductService _customizeProductService;
        public CustomizeProductController(ICustomizeProductService customizeProductService)
        {
            _customizeProductService = customizeProductService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomizeProduct>>> GetAll()
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

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CustomizeProduct customizeProduct)
        {
            await _customizeProductService.AddCustomizeProduct(customizeProduct);
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