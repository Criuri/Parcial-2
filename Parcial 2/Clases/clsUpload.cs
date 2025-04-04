using Parcial_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Parcial_2.Clases
{
    public class clsUpload
    {
        public string Datos { get; set; }
        public string Proceso { get; set; }
        public HttpRequestMessage request { get; set; }

        public async Task<HttpResponseMessage> GrabarArchivo(bool actualizar)
        {
            string ruta = HttpContext.Current.Server.MapPath("~/Fotos/");
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            var provider = new MultipartMemoryStreamProvider();
            await request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.Contents)
            {
                var buffer = await file.ReadAsByteArrayAsync();
                string nombre = file.Headers.ContentDisposition.FileName.Trim('\"');
                string fullPath = Path.Combine(ruta, nombre);

                if (actualizar && File.Exists(fullPath))
                    File.Delete(fullPath);

                File.WriteAllBytes(fullPath, buffer);
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Archivo cargado correctamente")
            };
        }

        public HttpResponseMessage DescargarArchivo(string nombre)
        {
            string ruta = HttpContext.Current.Server.MapPath("~/Fotos/" + nombre);
            if (!File.Exists(ruta))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Archivo no encontrado")
                };
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = nombre
            };

            return result;
        }
    }
}