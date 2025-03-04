using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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
}
