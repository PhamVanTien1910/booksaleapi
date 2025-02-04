using BookSale.Application.Dtos;
using BookSale.Application.Services.VNPayOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Controllers.Payment
{
    [Route("v1/vnpay")]
    [ApiController]
    public class VNPayOrderController : ControllerBase
    {
        private IVNPayOrderService _orderService;
        private IHttpContextAccessor _httpContextAccessor;

        public VNPayOrderController(IVNPayOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create-payment-url")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            var response = await _orderService.CreateOrderWithPaymentUrl(request);
            return Ok(response);
        }
    }
}
