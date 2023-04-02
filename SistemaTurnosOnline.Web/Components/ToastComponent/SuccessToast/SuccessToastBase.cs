using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ToastContracts;
using SistemaTurnosOnline.Web.Components.CssExtensions;

namespace SistemaTurnosOnline.Web.Components.ToastComponent.SuccessToast
{
    public abstract class SuccessToastBase : ComponentBase, IHasId, IToastCss, IToastOptions
    {
        public string Icon => IconConstants.CircleCheck;
        public string HeaderClass => BgColorConstants.BgSuccess;
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Text { get; set; }
    }
}
