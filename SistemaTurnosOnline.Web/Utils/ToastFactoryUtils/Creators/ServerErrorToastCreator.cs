using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;

namespace SistemaTurnosOnline.Web.Utils.ToastFactoryUtils.Creators
{
    public class ServerErrorToastCreator : ToastCreator
    {
        public override ToastModel CreateToast()
        {
            return new ToastModel(
                id: "server-error-toast",
                text: GenericToastNotificationText.ServerErrorText,
                title: ToastNotificationTitle.ServerErrorTitle
            );
        }
    }
}
