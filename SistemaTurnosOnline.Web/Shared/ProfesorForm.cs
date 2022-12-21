using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Web.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SistemaTurnosOnline.Models
{
    public class ProfesorForm
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
