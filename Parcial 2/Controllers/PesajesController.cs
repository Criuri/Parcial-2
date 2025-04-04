using Parcial_2.Clases;
using Parcial_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Parcial_2.Controllers
{
    public class PesajeController : Controller
    {
        public ActionResult InsertarPesaje(Pesaje pesaje, Camion camion, List<FotoPesaje> fotos)
        {
            clsPesaje obj = new clsPesaje
            {
                pesaje = pesaje,
                camion = camion,
                fotos = fotos
            };

            string resultado = obj.Insertar();
            ViewBag.Mensaje = resultado;
            return View();
        }

        public ActionResult ConsultarPorPlaca(string placa)
        {
            clsPesaje obj = new clsPesaje();
            var resultado = obj.ConsultarPorPlaca(placa);
            return View(resultado);
        }
    }
}