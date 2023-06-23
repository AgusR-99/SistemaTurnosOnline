namespace SistemaTurnosOnline.Web.Components.Contracts
{
    public interface IModal
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string HeaderText { get; set; }
        public string AlertText { get; set; }
    }
}
