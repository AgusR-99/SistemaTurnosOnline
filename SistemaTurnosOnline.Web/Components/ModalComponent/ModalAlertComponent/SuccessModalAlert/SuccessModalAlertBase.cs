using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ModalAlertContracts;
using SistemaTurnosOnline.Web.Components.CssConstants;

namespace SistemaTurnosOnline.Web.Components.ModalComponent.ModalAlertComponent.SuccessModalAlert
{
    public abstract class DangerModalAlertBase : ComponentBase, IHasText, IModalAlertCss
    {
        public string AlertType => AlertTypeConstants.Success;

        public string Icon => IconConstants.CircleCheck;

        [Parameter] public string Text { get; set; }
    }
}