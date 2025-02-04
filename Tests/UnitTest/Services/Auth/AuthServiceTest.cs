using AutoMapper;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Application.EmailHelper;
using BookSale.Application.Repositories;
using BookSale.Application.Services.Auth;
using BookSale.Application.Services.EmailSend;
using BookSale.Domain.Enums;
using BookSale.Domain.Payloads;
using BookSale.Domain.Specifications;
using BookSale.Domain.Specifications.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Services.Auth
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IEmailHelper> _mockEmailHelper;
        private readonly Mock<IEmailTemplateRender> _mockEmailTemplateRender;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockEmailHelper = new Mock<IEmailHelper>();
            _mockEmailTemplateRender = new Mock<IEmailTemplateRender>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _authService = new AuthService(
                _mockUnitOfWork.Object,
                _mockConfiguration.Object,
                _mockUserRepository.Object,
                _mockMapper.Object,
                _mockEmailHelper.Object,
                _mockEmailTemplateRender.Object,
                _mockHttpContextAccessor.Object,
                null
            );
        }

        [Fact]
        public async Task LoginAsync_WithValidCredentials_ReturnsToken()
        {
            var user = new User
            {
                Id = 1,
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Password123"),
                IsEmailConfirmed = true,
                IsActive = true,
                RoleId = (int)RoleEnums.Member,
            };

            var tokenObtainPairDto = new TokenObtainPairDto
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            _mockUserRepository.Setup(repo =>
                repo.FristOrDefaultAsync(It.IsAny<Specification<User>>(),
                                         It.IsAny<List<Expression<Func<User, object>>>>(),
                                         It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
            .ReturnsAsync(user); 
            _mockConfiguration.Setup(config => config.GetSection("JwtSettings:Secret").Value).Returns("");
            var result = await _authService.LoginAsync(tokenObtainPairDto);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Access); 
        }

    }
}
