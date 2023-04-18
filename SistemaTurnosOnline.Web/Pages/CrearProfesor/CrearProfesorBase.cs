﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.CarreraService;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Web.Shared;
using System.Security.Claims;

namespace SistemaTurnosOnline.Web.Pages.CrearProfesor
{
    public class CrearProfesorBase : ComponentBase, IAsyncDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        public CarreraListManager CarreraListManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }


        public string CrearProfesorModal { get; set; } = "createdModal";
        public ProfesorForm ProfesorForm { get; set; } = new ProfesorForm();
        public string ErrorMessage { get; set; }
        public List<Carrera> Carreras { get; set; }
        public string? UserId { get; set; }

        private HubConnection HubConnection;

        public ToastModelLegacy Toast { get; set; } = new ToastModelLegacy(
                status: ToastModelLegacy.Status.Error,
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
                CarreraListManager.SetCarrerasValues(new List<string> { });

                Carreras = await CarreraService.GetCarreras();

                HubConnection = new HubConnectionBuilder().WithUrl(NavigationManager.ToAbsoluteUri("/inactiveUsersHub"))
                .Build();

                await HubConnection.StartAsync();
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
                var CarrerasValues = CarreraListManager.GetCarrerasValues();

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

                    await Send(profesorToAdd);
                }
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
        }

        private async Task Send(Profesor profesor) => await HubConnection.InvokeAsync("Send", profesor);

        public async ValueTask DisposeAsync()
        {
            await HubConnection.DisposeAsync();
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
