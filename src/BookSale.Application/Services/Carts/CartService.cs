using AutoMapper;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Dtos.Response;
using BookSale.Application.Exceptions;
using BookSale.Application.Repositories;
using BookSale.Application.Services.CurrentUser;
using BookSale.Domain.Caching;
using BookSale.Domain.Entities;
using BookSale.Domain.Specifications.Books;
using BookSale.Domain.Specifications.Carts;
using BookSale.Domain.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;
        public CartService(ICartRepository cartRepository, IUserRepository userRepository, 
            IBookRepository bookRepository,
            IMapper mapper, IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            ICacheService cacheService
        )
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _cacheService = cacheService;
        }
        public async Task<CartItemResponseDto> CreateAsync(CartsRequest cartsRequest)
        {
            var userId = _currentUserService.UserId;
            var user = await _userRepository.FristOrDefaultAsync(new UserIdSpecification(userId)) ?? throw new UserIdNotFoundException();
            var bookId = new BookIdSpecification(cartsRequest.BookId);
            var book = await _bookRepository.FristOrDefaultAsync(bookId) ?? throw new BookIdNotFoundException();
            var cartEntity = _mapper.Map<Cart>(cartsRequest);
            cartEntity.UserId = userId;
            await _cartRepository.AddAsync(cartEntity);
            await _unitOfWork.SaveChangesAsync();
            var getBook = await _bookRepository.GetByIdAsync(cartsRequest.BookId)
                  ?? throw new BookIdNotFoundException();
            var cartDto = _mapper.Map<CartItemResponseDto>(cartEntity);
            cartDto.BookName = getBook.Title;
            cartDto.Price = getBook.Price;
            return cartDto;
        }

        public async Task<CartResponseDto> GetAsync()
        {
            var userId = _currentUserService.UserId;
            var cartItem = await _cartRepository.ToListAsync(new CartUserIdSpecification(userId));
            if (!cartItem.Any())
            {
                throw new CartNotFoundException();
            }
            var cartDtos = new CartResponseDto
            {
                UserId = userId,
            };
            var cachKey = "CartItem";
            var cachCart = await _cacheService.GetAsync<CartResponseDto>(cachKey);
            if (cachCart != null) {
                return cachCart;
            }
            foreach (var item in cartItem)
            {
                var book = await _bookRepository.GetByIdAsync(item.BookId)
                    ?? throw new BookIdNotFoundException();
                cartDtos.Items.Add(new CartItemResponseDto
                {
                    Id = item.Id,
                    BookName = book.Title,
                    Quantity = item.Quantity,
                    Price = book.Price
                });
            }
            await _cacheService.SetAsync(cachKey, cartDtos, TimeSpan.FromHours(10));
            return cartDtos;
        }
    }
}
