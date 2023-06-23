using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Extensions
{
    public static class CarreraExtensions
    {
        /// <summary>
        /// Returns a list of IDs for each checked CarreraForm object in the specified list.
        /// </summary>
        /// <param name="carrerasForm">The list of CarreraForm objects to check.</param>
        /// <returns>A list of IDs for each checked CarreraForm object.</returns>
        public static List<string> GetCheckedCarrerasIds(this List<CarreraForm> carrerasForm)
        {
            return carrerasForm
                .Where(carrera => carrera.IsChecked)
                .Select(carrera => carrera.Id!)
                .ToList();
        }

        /// <summary>
        /// Sets the 'IsChecked' property of each CarreraForm object in the specified list to 'true'
        /// if its 'Id' property is contained in the specified 'carreraIds' list.
        /// </summary>
        /// <param name="carrerasForm">The list of CarreraForm objects to check.</param>
        /// <param name="carreraIds">The list of carrera IDs to match against.</param>
        public static void CheckCarrerasById(this List<CarreraForm> carrerasForm, List<string> carreraIds)
        {
            if (carreraIds != null)
            {
                carrerasForm.Where(carrera => carreraIds.Contains(carrera.Id))
                    .ToList()
                    .ForEach(carrera => carrera.IsChecked = true);
            }
        }
    }
}
