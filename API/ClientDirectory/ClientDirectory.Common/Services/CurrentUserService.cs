using ClientDirectory.Common.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClientDirectory.Common.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var currentContext = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(currentContext)) UserId = currentContext;
            else UserId = "";
            IsAuthenticated = !string.IsNullOrEmpty(UserId);
        }

        public string UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
