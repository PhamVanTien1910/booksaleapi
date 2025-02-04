using AutoMapper;
using BookSale.Application.Dtos;
using BookSale.Application.Exceptions;
using BookSale.Application.Repositories;
using BookSale.Application.Services.CurrentUser;
using BookSale.Domain.Enums;
using BookSale.Domain.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public AdminService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var currentUserId = _currentUserService.UserId;
            var currenIdUserSpecification = new UserIdSpecification(currentUserId);
            var roleAdminSpecification = new UserRoleIdSpecification(RoleEnums.Admin); 
            if(await _userRepository.ExistsAsync(currenIdUserSpecification.And(roleAdminSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to delete information");
            }
            var targetUserIdSpecification = new UserIdSpecification(id);
            var user = await _userRepository.FristOrDefaultAsync(targetUserIdSpecification) ?? throw new UserIdNotFoundException();
            if(currentUserId != id && user.RoleId == (int)RoleEnums.Admin)
            {
                throw new AuthorizationException("The admin is not authorized to delete information of other admins");
            }
            _userRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto updateUserDto)
        {
            var currentUserId = _currentUserService.UserId;
            var currenIdUserSpecification = new UserIdSpecification(currentUserId);
            var roleAdminSpecification = new UserRoleIdSpecification(RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currenIdUserSpecification.And(roleAdminSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to edit information");
            }
            var targetUserIdSpecification = new UserIdSpecification(id);
            var user = await _userRepository.FristOrDefaultAsync(targetUserIdSpecification) ?? throw new UserIdNotFoundException();
            if (currentUserId != id && user.RoleId == (int)RoleEnums.Admin)
            {
                throw new AuthorizationException("The admin is not authorized to edit information of other admins");
            }
            if (!Enum.IsDefined(typeof(RoleEnums), updateUserDto.RoleId)) 
            {
                throw new RoleIdNotFoundException();
            }
            var otherUser = await _userRepository.FristOrDefaultAsync(new UserEmailSpecification(updateUserDto.Email!));
            if(otherUser != null && otherUser.Id != user.Id)
            {
                throw new EmailExistedExceptions();
            }
            _mapper.Map(updateUserDto, user);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            var userDto = await _userRepository.FirstOrDefaultAsync<UserDto>(new UserIdSpecification(id));
            return userDto!;
        }
    }
}
