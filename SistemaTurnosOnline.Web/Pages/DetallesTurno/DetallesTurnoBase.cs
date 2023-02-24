using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.DetallesTurno
{
    public class DetallesTurnoBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ITurnoService TurnoService { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TurnoListado TurnoListado { get; set; } = new TurnoListado();
        public Turno Turno { get; set; } = new Turno();
        public List<Carrera> CarrerasProfesor { get; set; }
        public static string CarreraCheckedNoneValue { get; set; } = "0";
        public string SelectedCarreraId { get; set; } = CarreraCheckedNoneValue;
        public string TurnoActualizado_Modal { get; set; } = "updatedModal";
        public string TurnoFinalizado_Modal { get; set; } = "deletedModal";
        public ToastModel Toast { get; set; } =
            new(
                status: ToastModel.Status.Error,
                id: "toastError",
                headerClass: "bg-danger",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
            );

        protected override async Task OnInitializedAsync()
        {
            Turno = await TurnoService.GetTurno(Id);

            var profesor = await ProfesorService.GetProfesor(Turno.UserId);

            TurnoListado = new TurnoListado
            {
                TurnoId = Turno.UserId,
                Dni = profesor.Dni,
                UserName = profesor.Nombre,
                Descripcion = Turno.Descripcion,
                Orden = Turno.OrdenEnCola.ToString()
            };

            CarrerasProfesor = (List<Carrera>)await CarreraService.GetCarrerasByUserId(Turno.UserId);
        }

        protected async Task UpdateTurno_Click()
        {
            try
            {
                Turno.Descripcion = TurnoListado.Descripcion;
                Turno.OrdenEnCola = Convert.ToInt64(TurnoListado.Orden);
                Turno.CarreraId = SelectedCarreraId;

                var turnoToUpdate = await TurnoService.UpdateTurno(Turno);

                if (turnoToUpdate != null)
                {
                    await TurnoActualizado_Modal.ShowModal(Js);
                }
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
        }

        protected async Task DeleteTurno_Click()
        {
            try
            {
                var deletedTurno = await TurnoService.DeleteTurno(Id);

                if (deletedTurno != null) await TurnoFinalizado_Modal.ShowModal(Js);
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("turno/readall");
        }
    }
}
