using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;
using SistemaTurnosOnline.Web.Components.Contracts.ModalAlertContracts;

namespace SistemaTurnosOnline.Web.Components.ModalComponent.ModalAlertComponent.Parent
{
    public abstract class ModalAlertBase : ComponentBase, IHasText, IModalAlertCss
    {
        [Parameter] public string AlertType { get; set; }

        [Parameter] public string Icon { get; set; }

        [Parameter] public string Text { get; set; }
    }
}
