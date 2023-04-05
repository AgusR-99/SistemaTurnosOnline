using Microsoft.AspNetCore.Components;

namespace SistemaTurnosOnline.Web.Components.ModalComponent.ModalFooterComponent
{
    public abstract class ModalFooterBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
