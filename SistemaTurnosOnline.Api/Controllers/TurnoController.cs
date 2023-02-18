using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Extensions;
using SistemaTurnosOnline.Api.Repositories;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared.Extensions;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoRepository turnoRepository;
        private readonly IValidator<TurnoListado> validator;

        public TurnoController(ITurnoRepository turnoRepository, IValidator<TurnoListado> validator)
        {
            this.turnoRepository = turnoRepository;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTurnos()
        {
            try
            {
                var turnos = await turnoRepository.GetTurnos();

                if (turnos == null)
                    return NotFound();

                return Ok(turnos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTurno(string id)
        {
            try
            {
                var turno = await turnoRepository.GetTurno(id);

                return Ok(turno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpGet("UserId/{userId}")]
        public async Task<IActionResult> GetTurnoByUserId(string userId)
        {
            try
            {
                var turno = await turnoRepository.GetTurnosByUserId(userId);

                return Ok(turno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpGet("ValidateOutOfBounds/{orden}")]
        public async Task<IActionResult> ValidateOutOfBounds(string orden)
        {
            try
            {
                var count = await turnoRepository.GetTurnoCount();

                var longOrden = Convert.ToInt64(orden);

                return longOrden >= 1 && longOrden <= count
                    ? Ok()
                    : UnprocessableEntity();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTurno([FromBody] TurnoForm turnoForm)
        {
            try
            {
                var turno = turnoForm.ConvertToTurno();

                var newTurno = await turnoRepository.CreateTurno(turno);

                if (newTurno == null)
                {
                    return NoContent();
                }

                return CreatedAtAction(nameof(GetTurno), new { id = turno.Id }, newTurno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTurno([FromBody] Turno turno, string id)
        {
            try
            {
                var turnoListado = new TurnoListado
                {
                    Orden = turno.OrdenEnCola.ToString()
                };

                var result = await validator.ValidateAsync(turnoListado);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                var updateTurno = await turnoRepository.UpdateTurno(turno, id);

                return CreatedAtAction(nameof(GetTurno), new { id = updateTurno.Id }, updateTurno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurno(string id)
        {
            try
            {
                var deletedTurno = await turnoRepository.DeleteTurno(id);

                if (deletedTurno == null)
                {
                    return NotFound();
                }

                return Ok(deletedTurno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
