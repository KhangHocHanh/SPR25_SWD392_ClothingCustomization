using BusinessObject.Enum;
using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.RequestDTO
{
    public class RequestDTO
    {
        public class LoginRequestDTO
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
        }
        public class ProductCreateDTO
        {
            public int CategoryId { get; set; }
            public string ProductName { get; set; } = null!;
            public decimal Price { get; set; }
            public int StockInStorage { get; set; }
            public string? Image { get; set; }
            public string? Description { get; set; }
        }
        public class ProductUpdateDTO
        {
            public int ProductId { get; set; }
            public int CategoryId { get; set; }
            public string ProductName { get; set; } = null!;
            public decimal Price { get; set; }
            public int StockInStorage { get; set; }
            public string? Image { get; set; }
            public string? Description { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class FeedbackDTO
        {
            [Required(ErrorMessage = "OrderId is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "OrderId must be greater than 0.")]
            public int OrderId { get; set; }

            [Required(ErrorMessage = "UserId is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0.")]
            public int UserId { get; set; }

            [Required(ErrorMessage = "Rating is required.")]
            [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
            public int Rating { get; set; }

            [Required(ErrorMessage = "Review cannot be empty.")]
            [StringLength(500, ErrorMessage = "Review must be between 10 and 500 characters.", MinimumLength = 10)]
            public string Review { get; set; } = string.Empty;

            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        }
        public class RoleDTO
        {
            public string RoleName { get; set; } = string.Empty;
        }

        public class UserDTO
        {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string FullName { get; set; } = string.Empty;

            public bool Gender { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }

            public string? Address { get; set; }

            [Phone]
            public string? Phone { get; set; }

            public string? Avatar { get; set; }

            public bool IsDeleted { get; set; }

            public string RoleName { get; set; } = string.Empty;
        }

        public class UserRegisterDTO
        {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required, MinLength(6)]
            public string Password { get; set; } = string.Empty;

            [Required]
            public string FullName { get; set; } = string.Empty;

            public bool Gender { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }

            public string? Address { get; set; }

            [Phone]
            public string? Phone { get; set; }

            public string? Avatar { get; set; }

            public string RoleName { get; set; } = "Customer"; // Default role
        }

        public class OrderDTO
        {
            public int CustomizeProductId { get; set; }
            public DateTime? OrderDate { get; set; }
            public DateTime? DeliveryDate { get; set; }
            public string? RecipientName { get; set; }
            public string? DeliveryAddress { get; set; }
            public string? ShippingMethod { get; set; }
            public double? ShippingFee { get; set; }
            public string? Notes { get; set; }
            public decimal? Price { get; set; }
            public int? Quantity { get; set; }
            public decimal? TotalPrice { get; set; }
         
        }
        public class OrderCreateDTO
        {
            [Required]
            public int CustomizeProductId { get; set; }

            public DateTime? OrderDate { get; set; } = DateTime.UtcNow;
            public DateTime? DeliveryDate { get; set; }

            [Required]
            public string RecipientName { get; set; } = string.Empty;

            [Required]
            public string DeliveryAddress { get; set; } = string.Empty;

            public string? ShippingMethod { get; set; }
            public double? ShippingFee { get; set; }
            public string? Notes { get; set; }

            [Required]
            public decimal Price { get; set; }

            [Required]
            public int Quantity { get; set; }

            public decimal? TotalPrice { get; set; }
        }

        public class OrderUpdateDTO
        {
            [Required]
            public int OrderId { get; set; }

            public int CustomizeProductId { get; set; }
            public DateTime? OrderDate { get; set; }
            public DateTime? DeliveryDate { get; set; }
            public string? RecipientName { get; set; }
            public string? DeliveryAddress { get; set; }
            public string? ShippingMethod { get; set; }
            public double? ShippingFee { get; set; }
            public string? Notes { get; set; }
            public decimal? Price { get; set; }
            public int? Quantity { get; set; }
            public decimal? TotalPrice { get; set; }
        }

        public class OrderListDTO
        {
            public int OrderId { get; set; }
            public int CustomizeProductId { get; set; }
            public DateTime? OrderDate { get; set; }
            public DateTime? DeliveryDate { get; set; }
            public string? RecipientName { get; set; }
            public string? DeliveryAddress { get; set; }
            public string? ShippingMethod { get; set; }
            public double? ShippingFee { get; set; }
            public string? Notes { get; set; }
            public decimal? Price { get; set; }
            public int? Quantity { get; set; }
            public decimal? TotalPrice { get; set; }
        }


        public class OrderStageCreateDTO
        {
            [Required(ErrorMessage = "OrderId is required.")]
            [Range(1, int.MaxValue, ErrorMessage = "OrderId must be greater than 0.")]
            public int OrderId { get; set; }

            [Required(ErrorMessage = "OrderStageName is required.")]
            [EnumDataType(typeof(OrderStageEnum), ErrorMessage = "Invalid value for OrderStageName.")]
            public OrderStageEnum OrderStageName { get; set; }

            public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        }

    }

    #region Hai
    public class FeedbackDTO
        {
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public int Rating { get; set; }
            public string Review { get; set; } = string.Empty;
            public DateTime CreatedDate { get; set; }
        }
        public class RoleDTO
        {
            public string RoleName { get; set; } = string.Empty;
        }

        public class UserDTO
        {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string FullName { get; set; } = string.Empty;

            public bool Gender { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }

            public string? Address { get; set; }

            [Phone]
            public string? Phone { get; set; }

            public string? Avatar { get; set; }

            public bool IsDeleted { get; set; }

            public string RoleName { get; set; } = string.Empty;
        }

        public class UserRegisterDTO
        {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required, MinLength(6)]
            public string Password { get; set; } = string.Empty;

            [Required]
            public string FullName { get; set; } = string.Empty;

            public bool Gender { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }

            public string? Address { get; set; }

            [Phone]
            public string? Phone { get; set; }

            public string? Avatar { get; set; }

            public string RoleName { get; set; } = "Customer"; // Default role
        }


        #endregion
    
}
