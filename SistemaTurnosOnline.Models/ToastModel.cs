namespace SistemaTurnosOnline.Web.Pages
{
    public class ToastModel
    {
        public enum Status
        {
            Success,
            Error,
            Pending
        }
        public Status status { get; set; }
        public string Id { get; set; }
        public string HeaderClass { get; set; }
        public string Class { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
        public ToastModel(Status status, string headerClass, string id, string @class, string icon, string title, string time, string text)
        {
            this.status = status;
            Id = id;
            Icon = icon;
            HeaderClass = headerClass;
            Class = @class;
            Title = title;
            Time = time;
            Text = text;
        }
    }
}
