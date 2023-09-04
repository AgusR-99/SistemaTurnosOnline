using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaTurnosOnline.Api.Repositories.Contracts;

namespace SistemaTurnosOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraControllerEf : ControllerBase
    {
        private readonly ICarreraRepositoryEf carreraRepositoryEf;

        public CarreraControllerEf(ICarreraRepositoryEf carreraRepositoryEf)
        {
            this.carreraRepositoryEf = carreraRepositoryEf;
        }

        [HttpGet]
        public IActionResult GetCarreras()
        {
            throw new NotImplementedException();
        }
    }
}
