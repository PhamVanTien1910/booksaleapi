using AutoMapper;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Dtos.Response;
using BookSale.Application.Exceptions;
using BookSale.Application.Repositories;
using BookSale.Application.Services.CurrentUser;
using BookSale.Domain.Entities;
using BookSale.Domain.Payloads;
using BookSale.Domain.Specifications.Books;
using BookSale.Domain.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin.Book
{
    public class BookService : IBookService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public BookService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper,
                IBookRepository bookRepository, IUserRepository userRepository, IAuthorRepository authorRepository,
                ICategoriesRepository categoriesRepository
            )
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _authorRepository = authorRepository;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<BookResponseDto> CreateBookAsync(BookRequestDto bookRequestDto)
        {
            var userId = _currentUserService.UserId;
            var currentIdUserSpecification = new UserIdSpecification(userId);
            var roleIdUserSpecification = new UserRoleIdSpecification(Domain.Enums.RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currentIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to create information");
            }
            var bookTitleSpecification = new BookTitleSpecification(bookRequestDto.Title); 
            if (await _bookRepository.ExistsAsync(bookTitleSpecification))
            {
                throw new BookTitleExistedException();
            }
            var bookEntity = _mapper.Map<BookSale.Domain.Entities.Book>(bookRequestDto);
            await _bookRepository.AddAsync(bookEntity);
            await _unitOfWork.SaveChangesAsync();
            var author = await _authorRepository.GetByIdAsync(bookRequestDto.AuthorsId)
                  ?? throw new AuthorIdNotFoundException();
            var categories = await _categoriesRepository.GetByIdAsync(bookRequestDto.CategorysId)
                  ?? throw new CategoryIdNotFoundException();
            var bookResponseDto = _mapper.Map<BookResponseDto>(bookEntity);
            bookResponseDto.AuthorsName = author.Name;
            bookResponseDto.CategorysName = categories.Name;
            return bookResponseDto;
        }

        public async Task DaleteBookAsync(int id)
        {
            var userId = _currentUserService.UserId;
            var currentIdUserSpecification = new UserIdSpecification(userId);
            var roleIdUserSpecification = new UserRoleIdSpecification(Domain.Enums.RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currentIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to delete information");
            }
            var targetIdBookSpecification = new BookIdSpecification(id);
            var books = await _bookRepository.FristOrDefaultAsync(targetIdBookSpecification) ?? throw new AuthorIdNotFoundException();
            _bookRepository.Delete(books);
            _unitOfWork.SaveChangesAsync();
          
        }

        public async Task<PaginatedResult<BookResponseDto>> GetPaginatedBookAsync(int page, int pagesize)
        {
                var bookDtos = await _bookRepository.ToListAsync<BookResponseDto>(
                    includes: new List<Expression<Func<BookSale.Domain.Entities.Book, object>>> 
                    { b => b.Authors,
                      b => b.Categorys
                    },
                    orderBy: query => query.OrderBy(book => book.Id),
                    page: page,
                    size: pagesize);

                var bookCount = await _bookRepository.CountAsync();

                return new PaginatedResult<BookResponseDto>(bookCount, bookDtos);
        }

        public async Task<BookResponseDto> UpdateBookAsync(int id, BookRequestDto bookRequestDto)
        {
            var userId = _currentUserService.UserId;
            var currentIdUserSpecification = new UserIdSpecification(userId);
            var roleIdUserSpecification = new UserRoleIdSpecification(Domain.Enums.RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currentIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to update information");
            }
            var targetIdBookSpecification = new BookIdSpecification(id);
            var books = await _bookRepository.FristOrDefaultAsync(targetIdBookSpecification) ?? throw new AuthorIdNotFoundException();
            _mapper.Map(bookRequestDto, books);
            _bookRepository.Update(books);
            await _unitOfWork.SaveChangesAsync();
            var author = await _authorRepository.GetByIdAsync(bookRequestDto.AuthorsId)
                  ?? throw new AuthorIdNotFoundException();
            var categories = await _categoriesRepository.GetByIdAsync(bookRequestDto.CategorysId)
                  ?? throw new CategoryIdNotFoundException();
            var bookResponseDto = _mapper.Map<BookResponseDto>(books);
            bookResponseDto.AuthorsName = author.Name;
            bookResponseDto.CategorysName = categories.Name;
            return _mapper.Map<BookResponseDto>(books);
        }
    }
}
