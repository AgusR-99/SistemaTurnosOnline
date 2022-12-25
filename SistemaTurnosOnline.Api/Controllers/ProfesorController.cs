using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Extensions;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Models.Validators;
using System;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorRepository profesorRepository;
        private readonly IValidator<ProfesorForm> validator;

        public ProfesorController(IProfesorRepository profesorRepository, IValidator<ProfesorForm> validator)
        {
            this.profesorRepository = profesorRepository;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfesores()
        {
            try
            {
                var profesores = await profesorRepository.GetProfesores();

                if (profesores == null)
                {
                    return NotFound();
                }

                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpGet("GetInactive")]
        public async Task<IActionResult> GetProfesoresInactive()
        {
            try
            {
                var profesores = await profesorRepository.GetProfesoresInactive();

                if (profesores == null)
                {
                    return NotFound();
                }

                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfesor(string id)
        {
            try
            {
                var profesor = await profesorRepository.GetProfesor(id);
                
                if (profesor == null)
                {
                    return NotFound();
                }

                return Ok(profesor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetProfesorByEmail(string email)
        {
            try
            {
                var profesor = await profesorRepository.GetProfesorByParam(email, p => p.Email);

                if (profesor == null)
                {
                    return NotFound();
                }

                return Ok(profesor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpGet("GetByDni/{dni}")]
        public async Task<IActionResult> GetProfesorByDni(string dni)
        {
            try
            {
                var profesor = await profesorRepository.GetProfesorByParam(dni, p => p.Dni);

                if (profesor == null)
                {
                    return NotFound();
                }

                return Ok(profesor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfesor([FromBody] ProfesorForm profesorForm)
        {
            try
            {
                var result = await validator.ValidateAsync(profesorForm);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                var profesor = profesorForm.ConvertToProfesor();

                var newProfesor = await profesorRepository.CreateProfesor(profesor);

                if (newProfesor == null)
                {
                    return NoContent();
                }

                return CreatedAtAction(nameof(GetProfesor), new {id = newProfesor.Id}, newProfesor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProfesor([FromBody] Profesor profesor, string id)
        {
            try
            {
                if (profesor == null)
                {
                    return BadRequest();
                }

                if (profesor.Nombre == String.Empty)
                {
                    ModelState.AddModelError("Nombre", "Ingrese el nombre del profesor");
                }

                profesor.Id = id;

                var newProfesor = await profesorRepository.UpdateProfesor(profesor);

                return CreatedAtAction(nameof(GetProfesor), new { id = newProfesor.Id }, newProfesor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
