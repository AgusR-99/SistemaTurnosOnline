using SistemaTurnosOnline.Web.Utils;

namespace SistemaTurnosOnline.Web.Extensions
{
    public static class UserRoleExtension
    {
        /// <summary>
        /// Finds the specified <paramref name="searchRole"/> in the given <paramref name="userRoles"/> list and returns it. Returns null if not found.
        /// </summary>
        /// <param name="userRoles">The list of user roles to search in.</param>
        /// <param name="searchRole">The user role to search for.</param>
        /// <returns>The first matching user role found in the list, or null if not found.</returns>
        public static UserRole FindUserRole(this List<UserRole> userRoles, UserRole searchRole)
        {
            return userRoles.FirstOrDefault(role => role == searchRole);
        }

        /// <summary>
        /// Returns a string representation of the UserRole.
        /// </summary>
        /// <param name="role">The UserRole to convert to string.</param>
        /// <returns>A string representation of the UserRole.</returns>
        public static string ToRoleString(this UserRole role)
        {
            return role.ToString();
        }
    }
}
