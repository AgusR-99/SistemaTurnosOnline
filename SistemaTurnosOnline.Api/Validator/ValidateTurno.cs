using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Api.Validator
{
    public class ValidateTurno : ITurnoValidator

    {
        private readonly ITurnoRepository turnoRepository;

        public ValidateTurno(ITurnoRepository turnoRepository)
        {
            this.turnoRepository = turnoRepository;
        }

        public async Task<bool> BeNotOutOfBounds(TurnoListado turnoListado, string orden)
        {
            var longOrden = Convert.ToInt64(orden);

            var turnoCount = await turnoRepository.GetTurnoCount();

            return longOrden > 0 && longOrden <= turnoCount;
        }
    }
}
