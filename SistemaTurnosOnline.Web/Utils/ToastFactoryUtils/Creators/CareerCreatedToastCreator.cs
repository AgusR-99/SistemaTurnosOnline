﻿using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;

namespace SistemaTurnosOnline.Web.Utils.ToastFactoryUtils.Creators
{
    public class CareerCreatedToastCreator : ToastCreator
    {
        public override ToastModel CreateToast()
        {
            return new ToastModel(
                id: "career-created-toast",
                title: ToastNotificationTitle.CreatedTitle,
                text: CareerToastNotificationText.CareerCreated
            );
        }
    }
}
