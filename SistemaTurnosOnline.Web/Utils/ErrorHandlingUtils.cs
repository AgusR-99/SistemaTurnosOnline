using Microsoft.JSInterop;
using SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast;

namespace SistemaTurnosOnline.Web.Utils
{
    public static class ErrorHandlingUtils
    {
        public static async Task ShowServerErrorToast(Exception ex, IJSRuntime js, DangerToast serverErrorToast)
        {
            try
            {
                serverErrorToast.Text = ex.Message;
                await serverErrorToast.Show(js);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error displaying server error toast: {e.Message}");
            }
        }
    }
}
