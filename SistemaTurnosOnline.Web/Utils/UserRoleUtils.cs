namespace SistemaTurnosOnline.Web.Utils
{
    public enum UserRole
    {
        Admin,
        Guest
    }

    public static class UserRoleUtils
    {
        private static readonly List<UserRole> userRoles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToList();

        /// <summary>
        /// Gets a list of all user roles.
        /// </summary>
        /// <returns>A list of all user roles.</returns>
        public static List<UserRole> GetUserRoles()
        {
            return userRoles;
        }
    }
}
