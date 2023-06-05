using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;

namespace SistemaTurnosOnline.Web.Utils.ToastFactoryUtils.Creators
{
    public class NewUserToastCreator : ToastCreator
    {
        public override ToastModel CreateToast()
        {
            return new ToastModel(
                id: "new-user-toast",
                title: ToastNotificationTitle.IncomingNotificationTitle,
                text: SignalRToastNotificationText.NewUser
            );
        }
    }
}
