using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Extensions;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoRepository turnoRepository;

        public TurnoController(ITurnoRepository turnoRepository)
        {
            this.turnoRepository = turnoRepository;
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
    }
}
