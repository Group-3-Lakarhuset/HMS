using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace HMS.Services
{
    public class UserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private ClaimsPrincipal _cachedUser;

        public UserService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<ClaimsPrincipal> GetUserAsync()
        {
            if (_cachedUser == null)
            {
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                _cachedUser = authState.User;
            }

            return _cachedUser;
        }
    }
}
