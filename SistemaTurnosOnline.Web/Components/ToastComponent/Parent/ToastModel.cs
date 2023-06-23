using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Web.Components.Contracts.ToastContracts;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Extensions;

namespace SistemaTurnosOnline.Web.Components.ToastComponent.Parent
{
    public class ToastModel : IHasId, IToastOptions, IShowable
    {
        public ToastModel(string id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }

        // The ID of the toast element.
        public string Id { get; set; }

        // The title to display in the toast.
        public string Title { get; set; }

        // The text to display in the body of the toast.
        public string Text { get; set; }

        // Shows the toast using the specified JS runtime.
        public async Task Show(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync(JsInteropFunctions.ShowToast, Id);
        }
    }
}
