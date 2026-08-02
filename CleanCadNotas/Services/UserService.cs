using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesBS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CleanCadNotas.Services
{
    public class UserService : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public int UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User ?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(value, out var userId))
                {
                    throw new UnauthorizedAccessException(
                    "User ID claim not found or invalid.");
                }
                return userId;
            }
        }



    }
}
