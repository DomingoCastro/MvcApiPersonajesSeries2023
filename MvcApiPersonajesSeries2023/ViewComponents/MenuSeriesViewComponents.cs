using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesSeries2023.Models;
using MvcApiPersonajesSeries2023.Services;

namespace MvcApiPersonajesSeries2023.ViewComponents
{
    public class MenuSeriesViewComponent : ViewComponent
    {
        private ServiceSeries service;

        public MenuSeriesViewComponent(ServiceSeries service)
        {
            this.service = service;
        }

        //EL METODO InvokeAsync() ES EL ENCARGADO DE ENVIAR UN 
        //MODEL HACIA NUESTRO LAYOUT.
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }
    }
}