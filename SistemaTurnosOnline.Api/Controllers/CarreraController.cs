using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Extensions;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly ICarreraRepository carreraRepository;
        private readonly IValidator<Carrera> validator;

        public CarreraController(ICarreraRepository carreraRepository, IValidator<Carrera> validator)
        {
            this.carreraRepository = carreraRepository;
            this.validator = validator;
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
                var result = await validator.ValidateAsync(carrera);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

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

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetCarreraByName(string name)
        {
            try
            {
                var carrera = await carreraRepository.GetCarreraByName(name);

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

        [HttpGet("GetByCode/{code}")]
        public async Task<IActionResult> GetCarreraByCode(string code)
        {
            try
            {
                var carrera = await carreraRepository.GetCarreraByCode(code);

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

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetCarrerasByUserId(string userId)
        {
            try
            {
                var carrera = await carreraRepository.GetCarrerasByUserId(userId);

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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCarrera([FromBody] Carrera carrera, string id)
        {
            try
            {
                var result = await validator.ValidateAsync(carrera);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                carrera.Id = id;

                var newCarrera = await carreraRepository.UpdateCarrera(carrera, id);

                return CreatedAtAction(nameof(GetCarrera), new { id = newCarrera.Id }, newCarrera);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(string id)
        {
            try
            {
                var deletedCarrera = await carreraRepository.DeleteCarrera(id);

                if (deletedCarrera == null)
                {
                    return NotFound();
                }

                return Ok(deletedCarrera);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
