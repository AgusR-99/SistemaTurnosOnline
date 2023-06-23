using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;

namespace SistemaTurnosOnline.Web.Utils.ToastFactoryUtils
{
    public static class ToastFactory
    {
        public static ToastModel CreateToast(ToastCreator creator)
        {
            return creator.CreateToast();
        }
    }
}
