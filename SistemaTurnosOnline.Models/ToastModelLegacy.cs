namespace SistemaTurnosOnline.Shared
{
    public class ToastModelLegacy
    {
        public enum Status
        {
            Success,
            Error
        }
        public Status status { get; set; }
        public string Id { get; set; }
        public string HeaderClass { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
        public ToastModelLegacy(Status status, string headerClass, string id, string icon, string title, string time, string text)
        {
            this.status = status;
            Id = id;
            Icon = icon;
            HeaderClass = headerClass;
            Title = title;
            Time = time;
            Text = text;
        }
    }
}
