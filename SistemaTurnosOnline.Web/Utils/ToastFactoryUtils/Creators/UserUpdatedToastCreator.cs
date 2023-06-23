using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;

namespace SistemaTurnosOnline.Web.Utils.ToastFactoryUtils.Creators
{
    public class UserUpdatedToastCreator : ToastCreator
    {
        public override ToastModel CreateToast()
        {
            return new ToastModel(
                id: "user-updated-toast",
                title: ToastNotificationTitle.UpdatedTitle,
                text: UserToastNotificationText.Updated
            );
        }
    }
}
