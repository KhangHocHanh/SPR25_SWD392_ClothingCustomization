using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using static BusinessObject.RequestDTO.RequestDTO;

namespace SPR25_SWD392_ClothingCustomization.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("productList")]
        public async Task<IActionResult> GetListProduct()
        {
            var result = await _productService.GetListProductsAsync();

            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getProductBy{id}")]
        public async Task<IActionResult> GetProductById(int id) => Ok(await _productService.GetProductByIdAsync(id));
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productDto) =>
      Ok(await _productService.CreateProductAsync(productDto));

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO productDto) =>
            Ok(await _productService.UpdateProductAsync(productDto));

        [HttpDelete("deleteProductBy{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await _productService.DeleteProductAsync(id));
    }
}

