using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;

namespace SistemaTurnosOnline.Web.Utils.ToastFactoryUtils.Creators
{
    public class FirstInQueueToastCreator : ToastCreator
    {
        public override ToastModel CreateToast()
        {
            return new ToastModel(
                id: "first-in-queue-toast",
                title: ToastNotificationTitle.IncomingNotificationTitle,
                text: SignalRToastNotificationText.FirstInQueue
            );
        }
    }
}
