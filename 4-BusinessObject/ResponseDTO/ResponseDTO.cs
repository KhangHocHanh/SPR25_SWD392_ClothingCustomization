    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Enum;

namespace BusinessObject.ResponseDTO
{
    public class ResponseDTO
    {

        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public ResponseDTO(int status, string? message, object? data = null)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public class LoginResponse
        {
            public int UserId { get; set; }
            public string UserName { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string? Phone { get; set; }
            public string? FullName { get; set; }
            public bool IsDeleted { get; set; }
            public string RoleName { get; set; }
        }
        public class UserListDTO
        {
            public int UserId { get; set; }
            public string? UserName { get; set; }
            public string? Password { get; set; }


        }
        public class ProductListDTO
        {
            public int ProductId { get; set; }
            public int CategoryId { get; set; }
            public string? ProductName { get; set; }
            public decimal? Price { get; set; }
            public int StockInStorage { get; set; }
            public string? Image { get; set; }
            public string? Description { get; set; }
            public bool IsDeleted { get; set; }
        }
        public class FeedbackListDTO
        {
            public int FeedbackId { get; set; }
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public int Rating { get; set; }
            public string Review { get; set; } = null!;
            public DateTime? CreatedDate { get; set; }
        }

        public class DesignElementDTO
        {
            public int DesignElementId { get; set; }
            public string? Image { get; set; }
            public string? Text { get; set; }
            public string? Size { get; set; }
            public string? ColorArea { get; set; }

            // Thông tin từ bảng DesignArea
            public int DesignAreaId { get; set; }
            public string AreaName { get; set; } = string.Empty;

            // Thông tin từ bảng CustomizeProduct
            public int CustomizeProductId { get; set; }
            public string ShirtColor { get; set; } = string.Empty;
            public string? FullImage { get; set; }
        }

        public class CategoryListDTO
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; } = null!;
            public string? Description { get; set; }
            public bool IsDeleted { get; set; }

            public int ProductCount { get; set; }
        }


        public class CategoryDetailDTO : CategoryListDTO
        {
            public ICollection<ProductListDTO> Products { get; set; } = new List<ProductListDTO>();
        }

        public class OrderStageListDTO
        {
            public int OrderStageId { get; set; }
            public int OrderId { get; set; }
            public string OrderStageName { get; set; } = null!;
            public DateTime? UpdatedDate { get; set; }
        }

        public class OrderStageResponseDTO
        {
            public int OrderStageId { get; set; }
            public int OrderId { get; set; }
            public string OrderStageName { get; set; }
            public DateTime? UpdatedDate { get; set; }
        }

        public class OrderDTO
        {
            [Required(ErrorMessage = "Mã sản phẩm tùy chỉnh là bắt buộc.")]
            [Range(1, int.MaxValue, ErrorMessage = "Mã sản phẩm tùy chỉnh phải lớn hơn 0.")]
            public int CustomizeProductId { get; set; }

            [Required(ErrorMessage = "Ngày đặt hàng là bắt buộc.")]
            public DateTime? OrderDate { get; set; }

            public DateTime? DeliveryDate { get; set; }

            [Required(ErrorMessage = "Tên người nhận là bắt buộc.")]
            [StringLength(100, ErrorMessage = "Tên người nhận phải từ 3 đến 100 ký tự.", MinimumLength = 3)]
            public string RecipientName { get; set; }

            [Required(ErrorMessage = "Địa chỉ giao hàng là bắt buộc.")]
            public string DeliveryAddress { get; set; }

            public string? ShippingMethod { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Phí vận chuyển không được là số âm.")]
            public double? ShippingFee { get; set; }

            public string? Notes { get; set; }

            [Required(ErrorMessage = "Giá là bắt buộc.")]
            [Range(1, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
            public decimal? Price { get; set; }

            [Required(ErrorMessage = "Số lượng là bắt buộc.")]
            [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
            public int? Quantity { get; set; }

            [Required(ErrorMessage = "Tổng giá là bắt buộc.")]
            [Range(1, double.MaxValue, ErrorMessage = "Tổng giá phải lớn hơn 0.")]
            public decimal? TotalPrice { get; set; }
        }
        public class CustomizeProductResponseDTO
        {
            public int CustomizeProductId { get; set; }
            public int ProductId { get; set; }
            public int UserId { get; set; }
            public string? ShirtColor { get; set; }
            public string? FullImage { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
        }
        public class CustomizeProductWithOrderResponse
        {
            public int CustomizeProductId { get; set; }
            public int OrderStageId { get; set; }
            public int ProductId { get; set; }
            public string ShirtColor { get; set; }
            public int OrderId { get; set; }
            public decimal TotalPrice { get; set; }
            public string OrderStatus { get; set; }
            public DateTime OrderDate { get; set; }
            public string Message { get; set; }
        }

    }

}

