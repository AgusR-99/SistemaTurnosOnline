using Microsoft.JSInterop;
using SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast;

namespace SistemaTurnosOnline.Web.Utils
{
    public static class ErrorHandlingExtensions
    {
        public static async Task ShowServerErrorToast(this DangerToast serverErrorToast, Exception ex, IJSRuntime js)
        {
            try
            {
                await serverErrorToast.Show(js);
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error displaying server error toast: {e.Message}");
            }
        }
    }
}
