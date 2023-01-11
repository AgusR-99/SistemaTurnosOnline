using Microsoft.JSInterop;

namespace SistemaTurnosOnline.Web.Extensions
{
    // https://stackoverflow.com/questions/73985083/in-blazor-how-can-i-call-a-js-function-from-within-a-static-c-sharp-method
    public static class Show
    {
        public static async Task ShowPassword(this string id, IJSRuntime Js)
        {
            await Js.InvokeVoidAsync(identifier: "showPassword", id);
        }
        public static async Task ShowToast(this string id, IJSRuntime Js)
        {
            await Js.InvokeVoidAsync(identifier: "showToast", id);
        }

        public static async Task ShowModal(this string id, IJSRuntime Js)
        {
            await Js.InvokeVoidAsync(identifier: "showModal", id);
        }

        public static async Task HideModal(this string id, IJSRuntime Js)
        {
            await Js.InvokeVoidAsync(identifier: "hideModal", id);
        }
    }
}
