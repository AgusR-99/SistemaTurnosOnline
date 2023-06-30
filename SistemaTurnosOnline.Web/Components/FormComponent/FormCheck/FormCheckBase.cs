using Microsoft.AspNetCore.Components;

namespace SistemaTurnosOnline.Web.Components.FormComponent.FormCheck
{
    public abstract class FormCheckBase : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
