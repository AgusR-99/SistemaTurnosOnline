using Microsoft.AspNetCore.Components;

namespace SistemaTurnosOnline.Web.Components.FormComponent
{
    public abstract class FormBase : ComponentBase
    {
        [Parameter] public string Icon { get; set; }
        [Parameter] public string Header { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
