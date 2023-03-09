using Microsoft.JSInterop;

namespace SistemaTurnosOnline.Web.Extensions
{
    public static class JsUtils
    {
        public static void SetReadOnly(this string id, IJSRuntime Js)
        {
            Js.InvokeVoidAsync(identifier: "setReadOnly", id);
        }
    }
}
