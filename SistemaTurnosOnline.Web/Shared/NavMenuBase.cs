using Microsoft.AspNetCore.Components;
using SistemaTurnosOnline.Models;

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

        public NavMenuBase()
        {
            tabsProfesor = new List<TabModel>()
            {
                new TabModel()
                {
                    Text = "Agregar",
                    Class = "oi-plus",
                    Href = "/profesor/create"
                },
                new TabModel()
                {
                    Text = "Administrar",
                    Class = "oi-wrench",
                    Href = "/profesor/readall"
                },
                new TabModel()
                {
                    Text = "Activar",
                    Class = "oi-task",
                    Href = "/profesor/readinactive"
                }
            };

            tabsCarreras = new List<TabModel>()
            {
                new TabModel()
                {
                    Text = "Agregar",
                    Class = "oi-plus",
                    Href = "/carrera/create"
                },
                new TabModel()
                {
                    Text = "Administrar",
                    Class = "oi-wrench",
                    Href = "/carrera/readall"
                }
            };
        }
    }
}
