using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Security.Claims;

namespace SistemaTurnosOnline.Web.Pages.CrearProfesor
{
    public class CrearProfesorBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }


        public string CrearProfesorModal { get; set; } = "createdModal";
        public ProfesorForm ProfesorForm { get; set; } = new ProfesorForm();
        public string ErrorMessage { get; set; }
        public List<Carrera> Carreras { get; set; }
        public string? UserId { get; set; }

        public ToastModel Toast { get; set; } = new ToastModel(
                status: ToastModel.Status.Error,
                id: "toastError",
                headerClass: "bg-danger",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
                );

        private async Task StartTimerAsync(int time)
        {
            while (time > 0)
            {
                time--;
                StateHasChanged();
                await Task.Delay(1000);
            }
            NavigationManager.NavigateTo("turno/user-items", true);
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CarreraService.SetCarrerasValues(new List<string> { });

                Carreras = await CarreraService.GetCarreras();
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;

                await Toast.Id.ShowToast(Js);
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authState = await AuthenticationState;

                if (authState.User.Claims.Any())
                {
                    UserId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

                    await StartTimerAsync(3);
                }
            }
        }

        protected async void Checkbox_Click(string id)
        {
            try
            {
                var carrerasValues = CarreraService.GetCarrerasValues();

                if (carrerasValues.Contains(id))
                {
                    carrerasValues.Remove(id);
                }
                else
                {
                    carrerasValues.Add(id);
                }

                CarreraService.SetCarrerasValues(carrerasValues);
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;

                await Toast.Id.ShowToast(Js);
            }

        }

        protected async Task CreateProfesor_Click()
        {
            try
            {
                var CarrerasValues = CarreraService.GetCarrerasValues();

                var carrerasId =
                    from carrera in Carreras
                    join carrerasValues in CarrerasValues
                    on carrera.Id equals carrerasValues
                    select carrera.Id;

                ProfesorForm.CarrerasId = carrerasId.ToList();

                ProfesorForm.Rol = "Guest";

                var profesorToAdd = await ProfesorService.CreateProfesor(ProfesorForm);

                if (profesorToAdd != null)
                {
                    await CrearProfesorModal.ShowModal(Js);
                }
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
