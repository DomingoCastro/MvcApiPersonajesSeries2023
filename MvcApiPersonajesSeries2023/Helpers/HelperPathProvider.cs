namespace MvcApiPersonajesSeries2023.Helpers
{
    public enum Folders
    {
        Uploads, Imagenes, Documentos, Temporal
    }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        private string HostUrl;


        public HelperPathProvider(IWebHostEnvironment hostEnvironment,
            IHttpContextAccessor accessor)
        {
            this.hostEnvironment = hostEnvironment;
            this.HostUrl = accessor.HttpContext.Request.Host.Value;
        }

        public string GetWebHostUrl()
        {
            return "https://" + this.HostUrl + "/";
        }
        public string GetNameFolder(Folders folders)
        {
            string carpeta = "";
            if (folders == Folders.Uploads)
            {
                return "uploads";
            }
            else if (folders == Folders.Documentos)
            {
                return "documentos";
            }
            else if (folders == Folders.Temporal)
            {
                return "temp";
            }
            else if (folders == Folders.Imagenes)
            {
                return "img";
            }
            return "";
        }
        //TENDREMOS UN METODO QUE NOS DEVOLVERA LA RUTA DEPENDIENDO
        //DE LA CARPETA SELECCIONADA
        public string GetMapPath(Folders folder, string filename)
        {
            string carpeta = "";
            if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Documentos)
            {
                carpeta = "documents";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Imagenes)
            {
                carpeta = "img";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, filename);
            return path;
        }
    }
}
