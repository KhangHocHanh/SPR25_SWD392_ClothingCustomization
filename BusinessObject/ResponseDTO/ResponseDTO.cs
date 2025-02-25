using System;
using System.Collections.Generic;
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
            public UserStatus IsDeleted { get; set; }
            public UserRole RoleId { get; set; }
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


    }
}
