using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;

namespace SistemaTurnosOnline.Web.Components.ModalComponent
{
    public abstract class ModalContainerBase : ComponentBase, IHasId, IHasLabel
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
