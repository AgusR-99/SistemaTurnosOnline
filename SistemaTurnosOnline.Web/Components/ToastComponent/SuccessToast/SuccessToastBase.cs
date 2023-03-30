using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ToastContracts;
using SistemaTurnosOnline.Web.Components.CssExtensions;

namespace SistemaTurnosOnline.Web.Components.ToastComponent.SuccessToast
{
    public class SuccessToastBase : ComponentBase, IHasId, IToastCss, IToastOptions
    {
        public string Icon => IconExtensions.CircleCheck;
        public string HeaderClass => BgColorExtensions.BgSuccess;
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Text { get; set; }
    }
}
