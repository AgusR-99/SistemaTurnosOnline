using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Utils
{
    public static class CarreraFormUtils
    {
        /// <summary>
        /// Toggles the specified Carrera ID in the specified list of Carrera IDs.
        /// If the ID is present, it is removed. If it is not present, it is added.
        /// </summary>
        /// <param name="carrerasValues">The list of Carrera IDs to modify.</param>
        /// <param name="id">The ID of the Carrera to toggle.</param>
        public static void ToggleCarreraValue(List<string> carrerasValues, string id)
        {
            if (carrerasValues.Contains(id))
            {
                carrerasValues.Remove(id);
            }
            else
            {
                carrerasValues.Add(id);
            }
        }
    }
}
