using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;

namespace SistemaTurnosOnline.Web.Utils
{
    public static class ToastFactory
    {
        public static ToastModel CreateServerErrorToast()
        {
            return new ToastModel(
                id: "server-error-toast",
                text: GenericToastNotificationText.ServerErrorText,
                title: ToastNotificationTitle.ServerErrorTitle
            );
        }


    }
}
