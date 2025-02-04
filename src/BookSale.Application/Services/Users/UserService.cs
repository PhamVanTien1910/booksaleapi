using AutoMapper;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Application.Exceptions;
using BookSale.Application.Repositories;
using BookSale.Application.Services.CurrentUser;
using BookSale.Domain.Payloads;
using BookSale.Domain.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<UserDto?> GetMe()
        {
           return await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(_currentUserService.UserId));
        }

        public async Task<PaginatedResult<UserDto>> getPaginationUserAsync(int page, int pageSize)
        {
                var userDtos = await _userRepository.ToListAsync<UserDto>(
                    includes: new List<Expression<Func<User, object>>> { u => u.Role },
                    orderBy: query => query.OrderBy(user => user.Id),
                    page: page,
                    size: pageSize);

                var userCount = await _userRepository.CountAsync();

                return new PaginatedResult<UserDto>(userCount, userDtos);
        }

        public async Task<UserDto> UpdateUserByIdAsysc(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId) ?? throw new UserIdNotFoundException();
            var otherUser = await _userRepository.FristOrDefaultAsync(new UserEmailSpecification(updateUserDto.Email!));
            if(otherUser != null && otherUser.Id != user.Id)
            {
                throw new EmailExistedExceptions();
            }
            _mapper.Map(updateUserDto, user);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            var userDto = await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(user.Id));
            return userDto!;
        }
    }
}
