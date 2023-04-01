using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ToastContracts;
using SistemaTurnosOnline.Web.Components.CssExtensions;

namespace SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast
{
    public class DangerToastBase : ComponentBase, IHasId, IToastCss, IToastOptions
    {
        public string Icon => IconExtensions.CircleX;
        public string HeaderClass => BgColorExtensions.BgDanger;
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Text { get; set; }
    }
}
