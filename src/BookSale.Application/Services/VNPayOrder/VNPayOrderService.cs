using AutoMapper;
using BookSale.Application.Dtos;
using BookSale.Application.Repositories;
using BookSale.Application.Services.CurrentUser;
using BookSale.Application.Utils;
using BookSale.Domain.Entities;
using BookSale.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.VNPayOrder
{
    public class VNPayOrderService : IVNPayOrderService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderRepositoty _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _vnp_Url;
        private readonly string _vnp_HashSecret;
        private readonly string _vnp_TmnCode;
        public VNPayOrderService(IMapper mapper, ICurrentUserService currentUserService, 
                                 IOrderRepositoty orderRepositoty, IUnitOfWork unitOfWork,
                                 IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _orderRepository = orderRepositoty;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _vnp_Url = _configuration.GetSection("VNPaySettings:VnPayUrl").Value;
            _vnp_HashSecret = _configuration.GetSection("VNPaySettings:VnPayHashSecret").Value;
            _vnp_TmnCode = _configuration.GetSection("VNPaySettings:VnPayTmnCode").Value;
        }
        public async Task<PaymentResponse> CreateOrderWithPaymentUrl(PaymentRequest paymentRequest)
        {
            Order order = _mapper.Map<Order>(paymentRequest);
            order.CustomerName = paymentRequest.CustomerName;
            order.CustomerPhone = paymentRequest.CustomerPhone;
            order.PaymentMethodId = (int)PaymentMethodEnum.VNPay;
            order.UserId = _currentUserService.UserId;
            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var vnPayLibrary = new VnPayLibrary();
            vnPayLibrary.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnPayLibrary.AddRequestData("vnp_Command", "pay");
            vnPayLibrary.AddRequestData("vnp_TmnCode", _vnp_TmnCode);
            vnPayLibrary.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString());
            vnPayLibrary.AddRequestData("vnp_CurrCode", paymentRequest.Currency);
            vnPayLibrary.AddRequestData("vnp_TxnRef", order.Id.ToString());
            vnPayLibrary.AddRequestData("vnp_OrderInfo", "Order" + order.Id);
            vnPayLibrary.AddRequestData("vnp_ReturnUrl", paymentRequest.ReturnUrl);
            vnPayLibrary.AddRequestData("vnp_IpAddr", PayLibUtils.GetIpAddress(_httpContextAccessor.HttpContext));
            vnPayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPayLibrary.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            vnPayLibrary.AddRequestData("vnp_Locale", paymentRequest.Locale);
            vnPayLibrary.AddRequestData("vnp_OrderType", paymentRequest.OrderType);

            string paymentUrl = vnPayLibrary.CreateRequestUrl(_vnp_Url, _vnp_HashSecret);

            return new PaymentResponse
            {
                OrderId = order.Id,
                PaymentUrl = paymentUrl,
            }; ; 
        }
    }
}
