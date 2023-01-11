using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.ListarCarreras
{
    public class ListarCarrerasBase : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        public IEnumerable<Carrera> Carreras { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "Nombre", "" };
        [Parameter]
        public string TableId { get; set; } = "carrerasTable";

        protected async override Task OnInitializedAsync()
        {
            Carreras = await CarreraService.GetCarreras();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            {
                await Js.InvokeAsync<object>(identifier: "datatableInit", "#" + TableId);
                await base.OnAfterRenderAsync(firstRender);
            }
        }

        public async void Dispose()
        {
            await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
        }
    }
}
