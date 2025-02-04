using BookSale.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.VNPayOrder
{
    public interface IVNPayOrderService
    {
        Task<PaymentResponse> CreateOrderWithPaymentUrl(PaymentRequest paymentRequest);
    }
}
