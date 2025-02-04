using AutoMapper;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Exceptions;
using BookSale.Application.Repositories;
using BookSale.Application.Services.CurrentUser;
using BookSale.Domain.Entities;
using BookSale.Domain.Enums;
using BookSale.Domain.Specifications.Authors;
using BookSale.Domain.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin.Authors
{
    public class AuthorsService : IAuthorsService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public AuthorsService(ICurrentUserService currentUserService,IUserRepository userRepository, IAuthorRepository authorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<AuthorsResponseDto> CreateAuthorAsync(AuthorsRequestDto authors)
        {
            var currentUserId = _currentUserService.UserId;
            var currenIdUserSpecification = new UserIdSpecification(currentUserId);
            var roleIdUserSpecification = new UserRoleIdSpecification(RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currenIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to create information");
            }
            var author = _mapper.Map<BookSale.Domain.Entities.Authors>(authors);
            await _authorRepository.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AuthorsResponseDto>(author);
        }

        public async Task DeleteAuthorByIdAsync(int id)
        {
            var currentUserId = _currentUserService.UserId;
            var currenIdUserSpecification = new UserIdSpecification(currentUserId);
            var roleIdUserSpecification = new UserRoleIdSpecification(RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currenIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to delete information");
            }
            var targetAuthorIdSpecification = new AuthorIdSpecification(id);
            var authors = await _authorRepository.FristOrDefaultAsync(targetAuthorIdSpecification) ?? throw new AuthorIdNotFoundException();
            _authorRepository.Delete(authors);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<AuthorsResponseDto>> GetAllAuthorAsync()
        {
            var author = await _authorRepository.ToListAsync();
            return _mapper.Map<List<AuthorsResponseDto>>(author);
        }

        public async Task<AuthorsResponseDto> UpdateAuthorAsync(int id, UpdateAuthorsDto authors)
        {
            var currentUserId = _currentUserService.UserId;
            var currentUserIdSpecification = new UserIdSpecification(currentUserId);
            var roleIdUserSpecification = new UserRoleIdSpecification(RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currentUserIdSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to update information");
            }
            var authorSpec = new AuthorIdSpecification(id);
            var existingAuthor = await _authorRepository.FristOrDefaultAsync(authorSpec) ?? throw new AuthorIdNotFoundException();
            _mapper.Map(authors, existingAuthor);
            _authorRepository.Update(existingAuthor);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AuthorsResponseDto>(existingAuthor);
        }
    }
}
