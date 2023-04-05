using Microsoft.JSInterop;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Extensions;

namespace SistemaTurnosOnline.Web.Components.ModalComponent
{
    public class ModalModel : IModal, IShowable
    {
        public ModalModel(string id, string label, string headerText, string alertText)
        {
            Id = id;
            Label = label;
            HeaderText = headerText;
            AlertText = alertText;
        }

        public string Id { get; set; }
        public string Label { get; set; }
        public string HeaderText { get; set; }
        public string AlertText { get; set; }

        public async Task Show(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync(JsInteropFunctions.ShowModal, Id);
        }
    }
}
