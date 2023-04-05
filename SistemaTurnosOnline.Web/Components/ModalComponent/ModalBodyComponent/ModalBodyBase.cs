using Microsoft.AspNetCore.Components;

namespace SistemaTurnosOnline.Web.Components.ModalComponent.ModalBodyComponent
{
    public abstract class ModalBodyBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
