using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Shared.Validators.Contracts
{
    public interface ITurnoValidator
    {
        Task<bool> BeNotOutOfBounds(TurnoListado turnoListado, string orden);
    }
}
