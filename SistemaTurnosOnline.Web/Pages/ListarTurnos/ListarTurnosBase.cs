using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.ListarTurnos
{
    public class ListarTurnosBase : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public ITurnoService TurnoService { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }

        public List<TurnoListado> TurnoListado { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "Orden en cola", "DNI", "Usuario", "Descripcion", "" };
        [Parameter]
        public string TableId { get; set; } = "turnosTable";

        protected override async Task OnInitializedAsync()
        {
            var turnos = (await TurnoService.GetTurnos()).ToList();

            List<TurnoListado> turnoListados = new List<TurnoListado>();

            foreach (var turno in turnos)
            {
                var profesor = await ProfesorService.GetProfesor(turno.UserId);

                TurnoListado turnoList = new TurnoListado
                {
                    TurnoId = turno.UserId,
                    Dni = profesor.Dni,
                    UserName = profesor.Nombre,
                    Descripcion = turno.Descripcion,
                    Orden = turno.OrdenEnCola
                };

                turnoListados.Add(turnoList);
            }

            // Se realiza la asignacion aca porque sino se fetchean los datos luego de un OnAfterRenderAsync
            // Por lo tanto, no se debe inicializar la propiedad "TurnoListado" antes del llamado de este metodo
            TurnoListado = turnoListados;
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
            await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
        }
    }
}
