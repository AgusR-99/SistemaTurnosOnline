namespace SistemaTurnosOnline.Models
{
    public class ProfesorForm
    {
        public string? Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public List<string>? CarrerasId { get; set; }
    }
}
