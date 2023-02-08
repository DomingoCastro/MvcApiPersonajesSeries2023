using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesSeries2023.Helpers;
using MvcApiPersonajesSeries2023.Models;
using MvcApiPersonajesSeries2023.Services;

namespace MvcApiPersonajesSeries2023.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceSeries service;
        private HelperPathProvider helper;
        private ServiceStorageBlobs serviceStorage;

        public PersonajesController(ServiceSeries service,
            HelperPathProvider helper,
            ServiceStorageBlobs serviceStorage)
        {
            this.service = service;
            this.helper = helper;
            this.serviceStorage = serviceStorage;

        }

        public async Task<IActionResult> PersonajesSerie(int idserie)
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesSerieAsync(idserie);
            return View(personajes);
        }

        public async Task<IActionResult> CreatePersonaje()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonaje(Personaje personaje , IFormFile fichero)
        {
            //DEBEMOS SUBIR EL FICHERO A NUESTRO SERVIDOR
            string fileName = fichero.FileName;
            //SUBIMOS EL FICHERO A AZURE Y EXTRAEMOS LA URL
            string urlBlob = "";
            using (Stream stream = fichero.OpenReadStream())
            {
                urlBlob = await this.serviceStorage.UploadBlobAsync(fileName, stream);
            }
            //GUARDAMOS EN LA CLASE PERSONAJE LA URL DEL BLOB
            personaje.Imagen = urlBlob;
            //string path = this.helper.GetMapPath(Folders.Imagenes, fileName);
            //using (Stream stream = new FileStream(path, FileMode.Create))
            //{
            //    await fichero.CopyToAsync(stream);
            //}
            ////POR ULTIMO, GUARDAMOS EN EL API LA URL DE NUESTRO SERVIDOR
            ////LA IMAGEN DEL PERSONAJE
            //string folder = this.helper.GetNameFolder(Folders.Imagenes);
            //personaje.Imagen = this.helper.GetWebHostUrl() + folder + "/" + fileName;
            await this.service.CreatePersonajeAsync(personaje.IdPersonaje
                , personaje.Nombre, personaje.Imagen, personaje.IdSerie);
            //LO VAMOS A LLEVAR A LA VISTA PERSONAJES SERIE
            return RedirectToAction("PersonajesSerie", new { idserie = personaje.IdSerie });
        }

        public async Task<IActionResult> UpdatePersonajeSerie()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["PERSONAJES"] = personajes;
            ViewData["SERIES"] = series;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonajeSerie
            (int idpersonaje, int idserie)
        {
            await this.service.UpdateSeriePersonajeAsync(idpersonaje, idserie);
            return RedirectToAction("PersonajesSerie"
                , new { idserie = idserie });
        }
    }
}
