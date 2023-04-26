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

        public static ToastModel CreateCareerCreatedToast()
        {
            return new ToastModel(
                id: "career-created-toast",
                title: ToastNotificationTitle.CreatedTitle,
                text: CareerToastNotificationText.CareerCreated
            );
        }

        public static ToastModel CreateNewUserToast()
        {
            return new ToastModel(
                id: "new-user-toast",
                title: ToastNotificationTitle.IncomingNotificationTitle,
                text: SignalRToastNotificationText.NewUser
            );
        }

        public static ToastModel CreateFirstInQueueToast()
        {
            return new ToastModel(
                id: "first-in-queue-toast",
                title: ToastNotificationTitle.IncomingNotificationTitle,
                text: SignalRToastNotificationText.FirstInQueue
            );
        }

    }
}
