using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorRepository profesorRepository;

        public ProfesorController(IProfesorRepository profesorRepository)
        {
            this.profesorRepository = profesorRepository;
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

        [HttpPost]
        public async Task<IActionResult> CreateProfesor([FromBody] Profesor profesor)
        {
            try
            {
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

                profesor.Id = new MongoDB.Bson.ObjectId(id);

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
