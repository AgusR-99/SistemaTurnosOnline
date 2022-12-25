using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Api.Extensions
{
    public static class Conversions
    {
        public static Profesor ConvertToProfesor(this ProfesorForm profesorForm)
        {
            return new Profesor
            {
                Dni = profesorForm.Dni,
                Email = profesorForm.Email,
                Nombre = profesorForm.Nombre,
                Password = profesorForm.Password,
                CarrerasId = profesorForm.CarrerasId
            };
        }
    }
}
