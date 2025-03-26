using System.Net;
using _2_Service.Service;
using _2_Service.Vnpay;
using _4_BusinessObject.VnPay;
using BusinessObject.Model;
using BusinessObject.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;
        private readonly IOrderStageService _orderStageService;

        public PaymentController(IVnpay vnPayservice, IConfiguration configuration, IOrderStageService orderStageService, IOrderService orderService)
        {
            _vnpay = vnPayservice;
            _configuration = configuration;
            _orderService = orderService;
            _orderStageService = orderStageService;

            _vnpay.Initialize(_configuration["Vnpay:TmnCode"], _configuration["Vnpay:HashSecret"], _configuration["Vnpay:BaseUrl"], _configuration["Vnpay:CallbackUrl"]);
        }

        /// <summary>
        /// Tạo url thanh toán
        /// </summary>
        /// <param name="money">Số tiền phải thanh toán</param>
        /// <param name="description">Mô tả giao dịch</param>
        /// <returns></returns>
        //[HttpGet("CreatePaymentUrl")]
        //public ActionResult<string> CreatePaymentUrl(double money, string description)
        //{
        //    try
        //    {
        //        var ipAddress = NetworkHelper.GetIpAddress(HttpContext); // Lấy địa chỉ IP của thiết bị thực hiện giao dịch

        //        var request = new VNPAY.NET.Models.PaymentRequest
        //        {
        //            PaymentId = DateTime.Now.Ticks,
        //            Money = money,
        //            Description = description,
        //            IpAddress = ipAddress,
        //            BankCode = BankCode.ANY, // Tùy chọn. Mặc định là tất cả phương thức giao dịch
        //            CreatedDate = DateTime.Now, // Tùy chọn. Mặc định là thời điểm hiện tại
        //            Currency = Currency.VND, // Tùy chọn. Mặc định là VND (Việt Nam đồng)
        //            Language = DisplayLanguage.Vietnamese // Tùy chọn. Mặc định là tiếng Việt
        //        };

        //        var paymentUrl = _vnpay.GetPaymentUrl(request);

        //        return Created(paymentUrl, paymentUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        /// <summary>
        /// Thực hiện hành động sau khi thanh toán. URL này cần được khai báo với VNPAY để API này hoạt đồng (ví dụ: http://localhost:1234/api/Vnpay/IpnAction)
        /// </summary>
        /// <returns></returns>
        [HttpGet("IpnAction")]
        public IActionResult IpnAction()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpay.GetPaymentResult(Request.Query);
                    if (paymentResult.IsSuccess)
                    {
                        // Thực hiện hành động nếu thanh toán thành công tại đây. Ví dụ: Cập nhật trạng thái đơn hàng trong cơ sở dữ liệu.
                        return Ok();
                    }

                    // Thực hiện hành động nếu thanh toán thất bại tại đây. Ví dụ: Hủy đơn hàng.
                    return BadRequest("Thanh toán thất bại");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NotFound("Không tìm thấy thông tin thanh toán.");
        }

        ///// <summary>
        ///// Trả kết quả thanh toán về cho người dùng
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("Callback")]
        //public ActionResult<PaymentResult> Callback()
        //{
        //    if (Request.QueryString.HasValue)
        //    {
        //        try
        //        {
        //            var paymentResult = _vnpay.GetPaymentResult(Request.Query);

        //            if (paymentResult.IsSuccess)
        //            {
        //                return Ok(paymentResult);
        //            }

        //            return BadRequest(paymentResult);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }

        //    return NotFound("Không tìm thấy thông tin thanh toán.");
        //}
        // Phương thức Callback trả về kết quả thanh toán dưới dạng JSON
        //[HttpGet("Callback")]
        //public ActionResult<PaymentResult> Callback()
        //{
        //    if (Request.QueryString.HasValue)
        //    {
        //        try
        //        {
        //            var paymentResult = _vnpay.GetPaymentResult(Request.Query);

        //            if (paymentResult.IsSuccess)
        //            {
        //                // Trả về URL của trang thành công cho FE
        //                return Ok(new { Status = "Success", RedirectUrl = "https://phamdangtuc-001-site1.ntempurl.com/swagger/index.html" });
        //            }

        //            // Trả về URL của trang thất bại cho FE
        //            return Ok(new { Status = "Failed", RedirectUrl = "https://phamdangtuc-001-site1.ntempurl.com/swagger/index.html" });
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(new { Status = "Error", Message = ex.Message });
        //        }
        //    }

        //    return NotFound("Không tìm thấy thông tin thanh toán.");
        //}

        [HttpGet("CreatePaymentUrl")]
        public async Task<ActionResult<string>> CreatePaymentUrl(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return BadRequest("Order not found");
                }

                var ipAddress = NetworkHelper.GetIpAddress(HttpContext);

                var request = new VNPAY.NET.Models.PaymentRequest
                {
                    PaymentId = DateTime.Now.Ticks,
                    Money = (double)order.TotalPrice,
                    Description = $"Thanh toán đơn hàng #{orderId}",
                    IpAddress = ipAddress,
                    BankCode = BankCode.ANY,
                    CreatedDate = DateTime.Now,
                    Currency = Currency.VND,
                    Language = DisplayLanguage.Vietnamese
                };

                var paymentUrl = _vnpay.GetPaymentUrl(request);
                return Created(paymentUrl, paymentUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Callback")]
        public async Task<ActionResult<ResponseDTO>> Callback()
        {
            if (!Request.QueryString.HasValue)
            {
                return NotFound(new ResponseDTO(404, "Không tìm thấy thông tin thanh toán."));
            }

            try
            {
                var paymentResult = _vnpay.GetPaymentResult(Request.Query);
                if (!paymentResult.IsSuccess)
                {
                    return BadRequest(new ResponseDTO(400, "Payment failed", new { RedirectUrl = "https://yourfrontend.com/payment-failed" }));
                }

                var orderId = (int)paymentResult.PaymentId;
                var existingOrder = await _orderService.GetOrderByIdAsync(orderId);
                if (existingOrder == null)
                {
                    return BadRequest(new ResponseDTO(400, $"OrderId {orderId} does not exist."));
                }

                var orderStageDto = new OrderStageCreateDTO
                {
                    OrderId = orderId,
                    OrderStageName = "Purchased",
                    UpdatedDate = DateTime.UtcNow
                };

                var response = await _orderStageService.CreateOrderStageAsync(orderStageDto);
                if (response.Status != 201)
                {
                    return BadRequest(response);
                }

                return Ok(new ResponseDTO(200, "Payment success", new { RedirectUrl = "https://yourfrontend.com/payment-success" }));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(500, "Internal server error", ex.Message));
            }
        }
    }
}