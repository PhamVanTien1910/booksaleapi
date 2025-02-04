using BookSale.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId {
            get 
            {
                var userIdClaim = (_httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value)
                    ?? throw new CustomExceptions(StatusCodes.Status401Unauthorized, Domain.Enums.ErrorCodeEnums.NotLogedIn, "User must log in to access this resource");
                return int.Parse(userIdClaim);
            } 
        }
    }
}
