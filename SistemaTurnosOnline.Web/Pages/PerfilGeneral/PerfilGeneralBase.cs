using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Security.Claims;
using SistemaTurnosOnline.Web.Extensions;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared.Extensions;
using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Services.CarreraManagement;
using SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts;

namespace SistemaTurnosOnline.Web.Pages.PerfilGeneral
{
    public class PerfilGeneralBase : ComponentBase
    {
        public ProfesorSecure Profesor { get; set; } = new ProfesorSecure();

        public List<Carrera> Carreras { get; set; }
        public List<CarreraForm> CarrerasForm { get; set; }
        public List<string> CarrerasValues { get; set; } = new List<string>();

        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IProfesorService ProfesorService { get; set; }

        [Inject]
        public ICarreraService CarreraService { get; set; }

        [Inject]
        public CarreraListManager CarreraListManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [CascadingParameter(Name = "ServerErrorToast")]
        private ToastModel ServerErrorToast { get; set; }

        public string ActualizadoProfesorModal { get; set; } = "updatedModal";

        protected override async Task OnInitializedAsync()
        {
            var userId = (await AuthenticationState)
                .User
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)
                .Value;

            Profesor = await ProfesorService.GetProfesor(userId);

            Carreras = await CarreraService.GetCarreras();

            CarrerasForm = Carreras.Select(carrera => carrera.ConvertToCarreraForm())
                .ToList();

            if (Profesor.CarrerasId != null)
                CarrerasForm.Where(carrera => Profesor.CarrerasId.Contains(carrera.Id))
                    .ToList()
                    .ForEach(carrera => carrera.IsChecked = true);

            var carrerasValues = CarreraListManager.GetCarrerasValues();

            carrerasValues
                .AddRange(CarrerasForm
                .Where(c => c.IsChecked)
                .Select(carrera => carrera.Id));

            CarreraListManager.SetCarrerasValues(carrerasValues);
        }

        protected async Task Checkbox_Click(string id)
        {
            try
            {
                var carrerasValues = CarreraListManager.GetCarrerasValues();

                if (carrerasValues.Contains(id))
                {
                    carrerasValues.Remove(id);
                }
                else
                {
                    carrerasValues.Add(id);
                }

                CarreraListManager.SetCarrerasValues(carrerasValues);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }

        }

        protected async Task UpdateProfesor_Click()
        {

            try
            {
                var CarrerasValues = CarreraListManager.GetCarrerasValues();

                var carrerasId =
                    from carrera in Carreras
                    join carrerasValues in CarrerasValues
                        on carrera.Id equals carrerasValues
                    select carrera.Id;

                Profesor.CarrerasId = carrerasId.ToList();

                var updatedProfesor = await ProfesorService.UpdateProfesor(Profesor);

                if (updatedProfesor != null) await ActualizadoProfesorModal.ShowModal(Js);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("turno/user-items");
        }
    }
}
