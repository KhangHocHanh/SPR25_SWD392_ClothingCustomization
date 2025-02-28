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



        #endregion
    }
}
