using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Extensions;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Extensions;
using System.Security.Cryptography;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorRepository profesorRepository;
        private readonly IValidator<ProfesorForm> profesorValidator;
        private readonly IValidator<ProfesorSecure> profesorSecureValidator;
        private readonly IValidator<SignInForm> signInValidator;

        public ProfesorController(IProfesorRepository profesorRepository, IValidator<ProfesorForm> profesorValidator,
            IValidator<ProfesorSecure> profesorSecureValidator, IValidator<SignInForm> signInValidator)
        {
            this.profesorRepository = profesorRepository;
            this.profesorValidator = profesorValidator;
            this.profesorSecureValidator = profesorSecureValidator;
            this.signInValidator = signInValidator;
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

                var profesorSecured = profesor.ConvertToProfesorSecure();
                
                if (profesorSecured == null)
                {
                    return NotFound();
                }

                return Ok(profesorSecured);
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

        [HttpGet("GetByEmail/{id}/{email}")]
        public async Task<IActionResult> GetProfesorByEmail(string email, string id)
        {
            try
            {
                var profesor = await profesorRepository.GetProfesorByParam(email, p => p.Email);

                if (profesor == null || profesor.Id == id)
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

        [HttpGet("GetByDni/{id}/{dni}")]
        public async Task<IActionResult> GetProfesorByDni(string dni, string id)
        {
            try
            {
                var profesor = await profesorRepository.GetProfesorByParam(dni, p => p.Dni);

                if (profesor == null || profesor.Id == id)
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

        [HttpGet("GetByDni/Active/{dni}")]
        public async Task<IActionResult> GetActiveProfesorByDni(string dni)
        {
            try
            {
                var profesor = await profesorRepository.GetProfesorByParam(dni, p => p.Dni);

                if (profesor == null || !profesor.Estado)
                {
                    return NotFound();
                }

                var profesorSecure = profesor.ConvertToProfesorSecure();

                return Ok(profesorSecure);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInValidation([FromBody] SignInForm form)
        {
            try
            {
                var result = await signInValidator.ValidateAsync(form);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                var profesor = await profesorRepository.GetProfesorByParam(form.Dni, p => p.Dni);

                Task<bool> validPass = new Task<bool>(() =>
                {
                    return BCrypt.Net.BCrypt.Verify(form.Password, profesor.Password);
                });

                validPass.Start();

                if (profesor == null || !profesor.Estado || !await validPass)
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
                var result = await profesorValidator.ValidateAsync(profesorForm);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                var profesor = profesorForm.ConvertToProfesor();

                if (string.IsNullOrWhiteSpace(profesor.Rol))
                    profesor.Rol = "guest";

                profesor.Password = BCrypt.Net.BCrypt.HashPassword(profesor.Password);

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
        public async Task<IActionResult> UpdateProfesor([FromBody] ProfesorSecure profesorSecure, string id)
        {
            try
            {
                var result = await profesorSecureValidator.ValidateAsync(profesorSecure);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);

                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                var profesorToUpdate = profesorSecure.ConvertToProfesor();

                profesorToUpdate.Password = (await profesorRepository.GetProfesor(id)).Password;

                var newProfesor = await profesorRepository.UpdateProfesor(profesorToUpdate, id);

                return CreatedAtAction(nameof(GetProfesor), new { id = newProfesor.Id }, newProfesor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPatch("ResetPassword/{id}")]
        public async Task<IActionResult> ResetPassword(string id)
        {
            try
            {
                var profesor = await profesorRepository.GetProfesor(id);

                var randomNumber = new byte[8];
                string refreshToken = "";

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    refreshToken = Convert.ToBase64String(randomNumber);
                }

                profesor.Password = BCrypt.Net.BCrypt.HashPassword(refreshToken);

                var updatedProfesor = await profesorRepository.UpdateProfesor(profesor, id);

                return CreatedAtAction(nameof(GetProfesor), new { id = updatedProfesor.Id }, refreshToken);
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
                var deletedProfesor = await profesorRepository.DeleteProfesor(id);

                if (deletedProfesor == null)
                {
                    return NotFound();
                }

                return Ok(deletedProfesor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
