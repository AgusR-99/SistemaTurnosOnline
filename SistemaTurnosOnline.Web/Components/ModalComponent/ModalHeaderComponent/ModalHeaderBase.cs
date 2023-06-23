using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Web.Components.Contracts;

namespace SistemaTurnosOnline.Web.Components.ModalComponent.ModalHeaderComponent
{
    public abstract class ModalHeaderBase : ComponentBase, IHasId, IHasText
    {
        [Parameter] public string Id { get; set; }

        [Parameter] public string Text { get; set; }
    }
}
