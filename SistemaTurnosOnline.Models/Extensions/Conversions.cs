﻿using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Shared.Extensions
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
                Rol = profesorForm.Rol,
                CarrerasId = profesorForm.CarrerasId
            };
        }

        public static Profesor ConvertToProfesor(this ProfesorSecure profesorSecure)
        {
            return new Profesor
            {
                Id = profesorSecure.Id,
                Dni = profesorSecure.Dni,
                Email = profesorSecure.Email,
                Nombre = profesorSecure.Nombre,
                Estado = profesorSecure.Estado,
                Rol = profesorSecure.Rol,
                CarrerasId = profesorSecure.CarrerasId
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
                Rol = profesor.Rol,
                CarrerasId = profesor.CarrerasId
            };
        }

        public static ProfesorSecure ConvertToProfesorSecure(this Profesor profesor)
        {
            return new ProfesorSecure
            {
                Id = profesor.Id,
                Dni = profesor.Dni,
                Email = profesor.Email,
                Nombre = profesor.Nombre,
                Estado = profesor.Estado,
                Rol = profesor.Rol,
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

        public static TurnoForm ConvertToTurnoForm(this Turno turno)
        {
            return new TurnoForm
            {
                Descripcion = turno.Descripcion
            };
        }

        public static Turno ConvertToTurno(this TurnoForm turnoForm)
        {
            return new Turno
            {
                Descripcion = turnoForm.Descripcion,
                UserId = turnoForm.UserId,
                CarreraId = turnoForm.CarreraId
            };
        }
    }
}
