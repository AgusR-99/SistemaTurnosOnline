namespace SistemaTurnosOnline.Shared
{
    public class ProfesorSecure
    {
        public string? Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
        public string Rol { get; set; }
        public List<string>? CarrerasId { get; set; }
    }
}
