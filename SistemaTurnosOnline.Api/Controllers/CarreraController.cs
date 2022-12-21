using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Repositories;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly ICarreraRepository carreraRepository;

        public CarreraController(ICarreraRepository carreraRepository)
        {
            this.carreraRepository = carreraRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarreras()
        {
            try
            {
                var carreras = await carreraRepository.GetCarreras();

                if (carreras == null)
                {
                    return NotFound();
                }

                return Ok(carreras);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarrera(string id)
        {
            try
            {
                var carrera = await carreraRepository.GetCarrera(id);

                if (carrera == null)
                {
                    return NotFound();
                }

                return Ok(carrera);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarrera([FromBody] Carrera carrera)
        {
            try
            {
                var newCarrera = await carreraRepository.CreateCarrera(carrera);

                if (newCarrera == null)
                {
                    return NoContent();
                }

                return CreatedAtAction(nameof(GetCarrera), new { id = newCarrera.Id }, newCarrera);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
