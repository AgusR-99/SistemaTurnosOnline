using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts;
using System.Security.Claims;

namespace SistemaTurnosOnline.Web.Pages.ListarProfesores
{
    public class ListarTodosProfesorBase : ComponentBase, IDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }
        public List<Profesor> Profesores { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "DNI", "Nombre", "Email", "" };
        [Parameter]
        public string TableId { get; set; } = "profesoresTable";

        private bool listLoaded = false;

        private bool tableInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            Profesores = (await ProfesorService.GetProfesores()).ToList();

            listLoaded = true;

            var authState = await AuthenticationState;

            var userId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            Profesor profesorToRemove = Profesores.SingleOrDefault(profesor => profesor.Id == userId);

            if (profesorToRemove != null) Profesores.Remove(profesorToRemove);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!tableInitialized && listLoaded)
            {
                await Js.InvokeAsync<object>("datatableInit", "#" + TableId);
                tableInitialized = true;
            }
        }

        //https://datatables.net/forums/discussion/56389/datatables-with-blazor
        public async void Dispose()
        {
            try
            {
                if (tableInitialized)
                    await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
            }
            catch
            {
            }
        }
    }
}
