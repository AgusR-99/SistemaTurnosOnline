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
                    Class = "oi oi-plus"
                },
                new TabModel()
                {
                    Text = "Listar todos",
                    Class = "oi oi-grid-four-up"
                },
                new TabModel()
                {
                    Text = "Buscar",
                    Class = "oi oi-magnifying-glass"
                },
                new TabModel()
                {
                    Text = "Actualizar",
                    Class = "oi oi-loop-circular"
                },
                new TabModel()
                {
                    Text = "Eliminar",
                    Class = "oi oi-trash"
                },
            };

            tabsCarreras = new List<TabModel>()
            {
                new TabModel()
                {
                    Text = "Agregar",
                    Class = "oi oi-plus"
                },
                new TabModel()
                {
                    Text = "Listar todos",
                    Class = "oi oi-grid-four-up"
                },
                new TabModel()
                {
                    Text = "Buscar",
                    Class = "oi oi-magnifying-glass"
                },
                new TabModel()
                {
                    Text = "Eliminar",
                    Class = "oi oi-trash"
                },
            };
        }
    }
}
