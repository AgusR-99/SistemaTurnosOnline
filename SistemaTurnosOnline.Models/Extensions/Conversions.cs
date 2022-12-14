using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Extensions
{
    public static class Conversions
    {
        public static Profesor ConvertToProfesor(this ProfesorForm profesorForm)
        {
            return new Profesor
            {
                Id = profesorForm.Id,
                Dni = profesorForm.Dni,
                Email = profesorForm.Email,
                Nombre = profesorForm.Nombre,
                Password = profesorForm.Password,
                Estado = profesorForm.Estado,
                CarrerasId = profesorForm.CarrerasId
            };
        }

        public static ProfesorForm ConvertToProfesorForm(this Profesor profesor)
        {
            return new ProfesorForm
            {
                Id = profesor.Id,
                Dni = profesor.Dni,
                Email = profesor.Email,
                Nombre = profesor.Nombre,
                Password = profesor.Password,
                Estado = profesor.Estado,
                CarrerasId = profesor.CarrerasId
            };
        }

        public static Carrera ConvertToCarrera(this CarreraForm carreraForm)
        {
            return new Carrera
            {
                Id = carreraForm.Id,
                Nombre = carreraForm.Nombre
            };
        }

        public static CarreraForm ConvertToCarreraForm(this Carrera carrera)
        {
            return new CarreraForm
            {
                Id = carrera.Id,
                Nombre = carrera.Nombre
            };
        }
    }
}
