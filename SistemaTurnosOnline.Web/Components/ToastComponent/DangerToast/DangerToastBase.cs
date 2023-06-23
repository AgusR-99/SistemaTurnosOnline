using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ToastContracts;
using SistemaTurnosOnline.Web.Components.CssConstants;

namespace SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast
{
    public abstract class DangerToastBase : ComponentBase, IHasId, IToastCss, IToastOptions
    {
        public string Icon => IconConstants.CircleX;
        public string HeaderClass => BgColorConstants.BgDanger;
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Text { get; set; }
    }
}
