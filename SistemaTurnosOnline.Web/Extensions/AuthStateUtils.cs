using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SistemaTurnosOnline.Web.Extensions
{
    public static class AuthStateUtils
    {
        /// <summary>
        /// Gets the user ID from the provided AuthenticationState instance.
        /// </summary>
        /// <param name="authState">The AuthenticationState instance to get the user ID from.</param>
        /// <returns>The user ID if found, or null if not found.</returns>
        public static string? GetUserIdFromAuthState(AuthenticationState authState)
        {
            var userId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
