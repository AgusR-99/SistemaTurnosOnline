using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace SistemaTurnosOnline.Web.Pages
{
    public class CrearProfesorBase : ComponentBase
    {
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        public ProfesorForm ProfesorForm { get; set; } = new ProfesorForm();
        public string ErrorMessage { get; set; }
        public List<Carrera> Carreras { get; set; }

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
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
