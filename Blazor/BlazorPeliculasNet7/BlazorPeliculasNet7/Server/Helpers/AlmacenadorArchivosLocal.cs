namespace BlazorPeliculasNet7.Server.Helpers
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task EliminarArchivo(string ruta, string nombreContenedor)
        {
            var nombreArchivo = Path.GetFileName(ruta);
            var directorioArivo = Path.Combine(env.WebRootPath, nombreContenedor, nombreArchivo);

            if (File.Exists(directorioArivo))
            {
                File.Delete(directorioArivo);
            }

            return Task.CompletedTask;
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var folder = Path.Combine(env.WebRootPath, nombreContenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string rutaGuardado = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(rutaGuardado, contenido);

            var urlActual = $"{httpContextAccessor!.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

            // carpeta\directorio\archivo
            var rutaParaBD = Path.Combine(urlActual, nombreContenedor, nombreArchivo).Replace("\\","/");
            return rutaParaBD;
        }
    }
}
