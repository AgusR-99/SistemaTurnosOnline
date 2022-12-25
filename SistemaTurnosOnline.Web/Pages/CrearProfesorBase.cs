﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages
{
    public class CrearProfesorBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        public ProfesorForm ProfesorForm { get; set; } = new ProfesorForm();
        public string ErrorMessage { get; set; }
        public List<Carrera> Carreras { get; set; }
        public List<ToastModel> Toasts { get; set; } = new List<ToastModel>
        {
              new ToastModel(
                status: ToastModel.Status.Success,
                id: "toastCreado",
                @class: "toast text-bg-success",
                icon: "oi oi-circle-check",
                title: "Solicitud exitosa!",
                time: "Ahora",
                text: "Se ha enviado la solicitud de alta al administrador"
                ),
             new ToastModel(
                status: ToastModel.Status.Error,
                id: "toastError",
                @class: "toast text-bg-danger",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
                )
        };
        protected override async Task OnInitializedAsync()
        {
            try
            {
                CarreraService.SetCarrerasValues(new List<string> { });
                Carreras = await CarreraService.GetCarreras();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async Task ShowToast(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showToast", id);
        }

        protected void Checkbox_Click(string id)
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
            catch (Exception)
            {
                throw;
            }
            
        }

        protected async Task CreateProfesor_Click()
        {
            try
            {
                Profesor profesor = new Profesor()
                {
                    Dni = ProfesorForm.Dni,
                    Nombre = ProfesorForm.Nombre,
                    Email = ProfesorForm.Email,
                    Password = ProfesorForm.Password,
                    Estado = false,
            };

                var CarrerasValues = CarreraService.GetCarrerasValues();

                var carrerasId =
                    from carrera in Carreras
                    join carrerasValues in CarrerasValues
                    on carrera.Id equals carrerasValues
                    select carrera.Id;

                profesor.CarrerasId = carrerasId.ToList();

                var profesorToAdd = await ProfesorService.CreateProfesor(profesor);

                if (profesorToAdd != null)
                {
                    var toast = Toasts.Find(t => t.status == ToastModel.Status.Success);

                    if (toast != null)
                    {
                        await ShowToast(toast.Id);
                    }
                    else throw new NullReferenceException($"No se ha encontrado {nameof(ToastModel)} con {nameof(ToastModel.Status.Success)}" +
                        $"asegurese que dicho parametro se encuentre presente en la lista");
                }
                else
                {
                    var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                    if (toast != null)
                    {
                        await ShowToast(toast.Id);
                    }
                    else throw new NullReferenceException($"No se ha encontrado {nameof(ToastModel)} con {nameof(ToastModel.Status.Error)}:" +
                        $"asegurese que dicho parametro se encuentre presente en la lista");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}