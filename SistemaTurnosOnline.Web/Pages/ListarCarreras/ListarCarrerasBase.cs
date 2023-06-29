using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;

namespace SistemaTurnosOnline.Web.Pages.ListarCarreras
{
    public class ListarCarrerasBase : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        public IEnumerable<Carrera> Carreras { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "Nombre", "Codigo de carrera", "" };
        [Parameter]
        public string TableId { get; set; } = "carrerasTable";

        private bool listLoaded = false;

        private bool tableInitialized = false;

        protected async override Task OnInitializedAsync()
        {
            Carreras = await CarreraService.GetCarreras();

            listLoaded = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!tableInitialized && listLoaded)
            {
                tableInitialized = true;

                await Js.InvokeAsync<object>("datatableInit", "#" + TableId);
            }
        }

        //https://datatables.net/forums/discussion/56389/datatables-with-blazor
        public async void Dispose()
        {
            try
            {
                if (tableInitialized)
                    await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
            }
            catch
            {
            }
        }
    }
}
