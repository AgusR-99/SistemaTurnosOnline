using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;
using SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts;
using SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts;

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
        [Inject]
        public ICarreraService CarreraService { get; set; }

        public List<TurnoListado> TurnoListado { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "Orden en cola", "DNI", "Usuario", "Carrera", "Descripcion", "" };
        [Parameter]
        public string TableId { get; set; } = "turnosTable";

        private bool listLoaded = false;

        private bool tableInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            var turnos = (await TurnoService.GetTurnos()).ToList();

            List<TurnoListado> turnoListados = new List<TurnoListado>();

            foreach (var turno in turnos)
            {
                var profesor = await ProfesorService.GetProfesor(turno.UserId);

                string carreraNombre;

                if (turno.CarreraId == null || turno.CarreraId == "0") carreraNombre = "Sin carrera asignada";
                else carreraNombre = (await CarreraService.GetCarrera(turno.CarreraId)).Nombre;

                TurnoListado turnoList = new TurnoListado
                {
                    TurnoId = turno.Id,
                    Dni = profesor.Dni,
                    UserName = profesor.Nombre,
                    Carrera = carreraNombre,
                    Descripcion = turno.Descripcion,
                    Orden = turno.OrdenEnCola.ToString()
                };

                turnoListados.Add(turnoList);
            }

            // Se realiza la asignacion aca porque sino se fetchean los datos luego de un OnAfterRenderAsync
            // Por lo tanto, no se debe inicializar la propiedad "TurnoListado" antes del llamado de este metodo
            TurnoListado = turnoListados;

            listLoaded = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!tableInitialized && listLoaded)
            {
                await Js.InvokeAsync<object>("datatableInit", "#" + TableId);
                tableInitialized = true;
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
