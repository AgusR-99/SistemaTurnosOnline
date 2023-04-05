using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ModalAlertContracts;
using SistemaTurnosOnline.Web.Components.CssConstants;

namespace SistemaTurnosOnline.Web.Components.ModalComponent.ModalAlertComponent.DangerModalAlert
{
    public abstract class DangerModalAlertBase : ComponentBase, IHasText, IModalAlertCss
    {
        public string AlertType => AlertTypeConstants.Danger;

        public string Icon => IconConstants.CircleX;

        [Parameter] public string Text { get; set; }
    }
}