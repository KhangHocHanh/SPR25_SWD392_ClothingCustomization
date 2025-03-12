using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        #region Khang
        public class ProductCreateDTO
        {
            [Required(ErrorMessage = "Category ID is required")]
            public int CategoryId { get; set; }

            [Required(ErrorMessage = "Product name is required")]
            [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
            public string ProductName { get; set; } = null!;

            [Required(ErrorMessage = "Price is required")]
            [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
            public decimal Price { get; set; }

            [Required(ErrorMessage = "Stock is required")]
            [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than 0")]
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
        public class CategoryCreateDTO
        {
            [Required]
            public string CategoryName { get; set; } = null!;
            public string? Description { get; set; }
        }

        public class CategoryUpdateDTO
        {
            public int CategoryId { get; set; }
            [Required]
            public string CategoryName { get; set; } = null!;
            public string? Description { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class CategoryListDTO
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; } = null!;
            public string? Description { get; set; }
            public bool IsDeleted { get; set; }
            public int ProductCount { get; set; }
        }

        // DesignElement
        public class DesignElementCreateDTO
        {
            [Required]
            public int DesignAreaId { get; set; }  // Chọn từ danh sách DesignArea (Dropdown)

            [Required]
            public int CustomizeProductId { get; set; }  // Chọn từ danh sách CustomizeProduct (Dropdown)

            public IFormFile? Image { get; set; }  // Upload hình ảnh

            [MaxLength(250)]
            public string? Text { get; set; }  // Văn bản thiết kế

            [MaxLength(10)]
            public string? Size { get; set; }

            [MaxLength(20)]
            public string? ColorArea { get; set; }
        }
        #endregion
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

            public string RoleName { get; set; } = "Member"; // Default role
        }

        public class UserUpdateDTO
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool Gender { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Avatar { get; set; }
        }

        public class ChangePasswordDTO
        {
            [Required]
            public string Username { get; set; } = string.Empty;
            [Required, MinLength(6)]
            public string Password { get; set; } = string.Empty;
            [Required, MinLength(6)]
            public string ConfirmPassword { get; set; } = string.Empty;
        }
        public class GoogleLoginRequest
        {
            public string IdToken { get; set; }
        }

        public class NotificationDTO
        {

            public int UserId { get; set; }

            public string? Subject { get; set; }

            public string? Message { get; set; }

            public DateTime CreatedDate { get; set; }

            public bool IsRead { get; set; }
        }

        public class PostNotificationDTO
        {

            public int UserId { get; set; }

            public string? Subject { get; set; }

            public string? Message { get; set; }

        }

        public class PutNotificationDTO
        {
            public string? Subject { get; set; }

            public string? Message { get; set; }

        }

        public class NotificationRoleDTO
        {
            public string RoleName { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }

        }

        #endregion
    }
}
