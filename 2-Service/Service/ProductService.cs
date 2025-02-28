using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ResponseDTO;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace Service.Service
{
    public interface IProductService
    {
        Task<ResponseDTO> GetListProductsAsync();
        Task<ResponseDTO> GetProductByIdAsync(int id);
        Task<ResponseDTO> CreateProductAsync(ProductCreateDTO productDto);
        Task<ResponseDTO> UpdateProductAsync(ProductUpdateDTO productDto);
        Task<ResponseDTO> DeleteProductAsync(int id);
    }
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateProductAsync(ProductCreateDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(Const.SUCCESS_CREATE_CODE, "Product created successfully");
        }

        public async Task<ResponseDTO> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return new ResponseDTO(Const.WARNING_NO_DATA_CODE, "Product not found");

            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(Const.SUCCESS_DELETE_CODE, "Product deleted successfully");
        }

        public async Task<ResponseDTO> GetListProductsAsync()
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync();

                if (products == null || !products.Any())
                {
                    return new ResponseDTO(Const.SUCCESS_CREATE_CODE, "Empty List");
                }

                var result = _mapper.Map<List<ProductListDTO>>(products);

                return new ResponseDTO(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception e)
            {
                return new ResponseDTO(Const.ERROR_EXCEPTION, e.Message);
            }
        }

        public async Task<ResponseDTO> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return new ResponseDTO(Const.WARNING_NO_DATA_CODE, "Product not found");

            var result = _mapper.Map<ProductListDTO>(product);
            return new ResponseDTO(Const.SUCCESS_READ_CODE, "Success", result);
        }

        public async Task<ResponseDTO> UpdateProductAsync(ProductUpdateDTO productDto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productDto.ProductId);
            if (product == null) return new ResponseDTO(Const.WARNING_NO_DATA_CODE, "Product not found");

            _mapper.Map(productDto, product);
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Product updated successfully");
        }
    }
}
