namespace SistemaTurnosOnline.Web.Services.CarreraManagement
{
    public class CarreraListManager
    {
        public List<string> CarrerasValues { get; set; } = new List<string>();

        public List<string> GetCarrerasValues()
        {
            return CarrerasValues;
        }

        public void SetCarrerasValues(List<string> carrerasValue)
        {
            CarrerasValues = carrerasValue;
        }
    }
}
