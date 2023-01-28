using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Shared
{
    public class NavMenuBase : ComponentBase
    {
        protected bool collapseNavMenu = true;

        protected string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        public List<TabModel> tabsProfesor { get; set; }
        public List<TabModel> tabsCarreras { get; set; }
        public List<TabModel> tabsTurnos { get; set; }
        public List<TabModel> tabsTurnosAdmin { get; set; }

        public NavMenuBase()
        {
            tabsProfesor = new List<TabModel>()
            {
                new()
                {
                    Text = "Agregar",
                    Class = "oi-plus",
                    Href = "/profesor/create"
                },
                new()
                {
                    Text = "Administrar",
                    Class = "oi-wrench",
                    Href = "/profesor/readall"
                },
                new()
                {
                    Text = "Activar",
                    Class = "oi-task",
                    Href = "/profesor/readinactive"
                }
            };

            tabsCarreras = new List<TabModel>()
            {
                new()
                {
                    Text = "Agregar",
                    Class = "oi-plus",
                    Href = "/carrera/create"
                },
                new()
                {
                    Text = "Administrar",
                    Class = "oi-wrench",
                    Href = "/carrera/readall"
                }
            };

            tabsTurnos = new List<TabModel>()
            {
                new()
                {
                    Text = "Mis turnos",
                    Class = "oi-clock",
                    Href = "/turno/user-items"
                },
                new()
                {
                    Text = "Agregar",
                    Class = "oi-plus",
                    Href = "/turno/create"
                }
            };

            tabsTurnosAdmin = new List<TabModel>()
            {
                new()
                {
                    Text = "Administrar",
                    Class = "oi-wrench",
                    Href = "/turno/readall"
                }
            };
        }
    }
}
