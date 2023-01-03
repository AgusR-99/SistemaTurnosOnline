using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.ListarProfesoresInactivos
{
    public class ListarProfesoresInactivosBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }

        public IEnumerable<Profesor> Profesores { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "DNI", "Nombre", "Email", "" };

        [Parameter]
        public string TableId { get; set; } = "tabla-Profesores";

        protected override async Task OnInitializedAsync()
        {
            Profesores = await ProfesorService.GetProfesoresInactive();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            {
                await Js.InvokeAsync<object>(identifier: "datatableInit", "#" + TableId);
                await base.OnAfterRenderAsync(firstRender);
            }
        }
    }
}
