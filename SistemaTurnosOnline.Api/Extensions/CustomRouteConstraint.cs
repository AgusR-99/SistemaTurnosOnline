namespace SistemaTurnosOnline.Api.Extensions
{
    public class CustomRouteConstraint<TEnum> : IRouteConstraint
             where TEnum : struct, Enum
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // retrieve the candidate value
            var candidate = values[routeKey]?.ToString();
            // attempt to parse the candidate to the required Enum type, and return the result
            return Enum.TryParse(candidate, true, out TEnum _);
        }
    }
}
