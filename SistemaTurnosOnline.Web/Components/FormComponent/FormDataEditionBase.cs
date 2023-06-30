using Microsoft.AspNetCore.Components;

namespace SistemaTurnosOnline.Web.Components.FormComponent
{
    public abstract class FormDataEditionBase : ComponentBase
    {
        [Parameter] public string Href { get; set; }
        [Parameter] public string Icon { get; set; }
        [Parameter] public string Header { get; set; }
        [Parameter] public string HeaderSecondary { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
