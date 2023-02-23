using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.ListarProfesores
{
    public class ListarTodosProfesorBase : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        public IEnumerable<Profesor> Profesores { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "DNI", "Nombre", "Email", "" };
        [Parameter]
        public string TableId { get; set; } = "profesoresTable";
        protected override async Task OnInitializedAsync()
        {
            Profesores = await ProfesorService.GetProfesores();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            {
                await Js.InvokeAsync<object>(identifier: "datatableInit", "#" + TableId);
                await base.OnAfterRenderAsync(firstRender);
            }
        }

        //https://datatables.net/forums/discussion/56389/datatables-with-blazor
        public async void Dispose()
        {
            try
            {
                await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
            }
            catch
            {

            }
        }
    }
}
