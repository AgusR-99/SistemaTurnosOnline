﻿using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Web.Services.Contracts
{
    public interface IProfesorService
    {
        Task<List<Profesor>> GetProfesores();
        Task<Profesor> GetProfesor(string id);
        Task<Profesor> CreateProfesor(Profesor profesor);
        Task<Profesor> UpdateProfesor(Profesor profesor);
        Task DeleteProfesor(string id);
    }
}
