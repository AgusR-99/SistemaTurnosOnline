using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ToastContracts;
using SistemaTurnosOnline.Web.Extensions;

namespace SistemaTurnosOnline.Web.Components.ToastComponent
{
    public class ToastBase : ComponentBase, IHasId, IToastCss, IToastOptions, IShowable
    {
        public ToastBase()
        {
        }

        public ToastBase(string id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }

        // The ID of the toast element.
        [Parameter] public string Id { get; set; }

        // The icon to use for the toast.
        [Parameter] public string Icon { get; set; }

        // The CSS class to use for the header of the toast.
        [Parameter] public string HeaderClass { get; set; }

        // The title to display in the toast.
        [Parameter] public string Title { get; set; }

        // The text to display in the body of the toast.
        [Parameter] public string Text { get; set; }

        // Shows the toast using the specified JS runtime.
        public async Task Show(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync(JsInteropFunctions.ShowToast, Id);
        }
    }
}