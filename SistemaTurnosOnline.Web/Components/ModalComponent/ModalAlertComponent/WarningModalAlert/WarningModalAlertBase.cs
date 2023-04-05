using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ModalAlertContracts;
using SistemaTurnosOnline.Web.Components.CssConstants;

namespace SistemaTurnosOnline.Web.Components.ModalComponent.ModalAlertComponent.WarningModalAlert
{
    public abstract class WarningModalAlertBase : ComponentBase, IHasText, IModalAlertCss
    {
        public string AlertType => AlertTypeConstants.Warning;

        public string Icon => IconConstants.ExclamationTriangle;

        [Parameter] public string Text { get; set; }
    }
}