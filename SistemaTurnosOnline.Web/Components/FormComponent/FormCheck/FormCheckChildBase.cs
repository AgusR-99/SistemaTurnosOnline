using Microsoft.AspNetCore.Components;

namespace SistemaTurnosOnline.Web.Components.FormComponent.FormCheck
{
    public abstract class FormCheckChildBase : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
