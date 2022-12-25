﻿using Microsoft.AspNetCore.Components;
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
                    Text = "Listar todos",
                    Class = "oi-grid-four-up",
                    Href = "/profesor/readall"
                },
                new TabModel()
                {
                    Text = "Buscar",
                    Class = "oi-magnifying-glass",
                    Href = "/profesor/search"
                },
                new TabModel()
                {
                    Text = "Actualizar",
                    Class = "oi-loop-circular",
                    Href = "/profesor/update"
                },
                new TabModel()
                {
                    Text = "Eliminar",
                    Class = "oi-trash",
                    Href = "/profesor/delete"
                },
            };

            tabsCarreras = new List<TabModel>()
            {
                new TabModel()
                {
                    Text = "Agregar",
                    Class = "oi-plus"
                },
                new TabModel()
                {
                    Text = "Listar todos",
                    Class = "oi-grid-four-up"
                },
                new TabModel()
                {
                    Text = "Buscar",
                    Class = "oi-magnifying-glass"
                },
                new TabModel()
                {
                    Text = "Eliminar",
                    Class = "oi-trash"
                },
            };

            foreach(var tab in tabsProfesor)
            {
                tab.Class += " oi ms-3";
            }

            foreach (var tab in tabsCarreras)
            {
                tab.Class += " oi ms-3";
            }
        }
    }
}
