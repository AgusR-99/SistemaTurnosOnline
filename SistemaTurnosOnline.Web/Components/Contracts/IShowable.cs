using Microsoft.JSInterop;

namespace SistemaTurnosOnline.Web.Components.Contracts
{
    public interface IShowable
    {
        public Task Show(IJSRuntime Js);
    }
}
