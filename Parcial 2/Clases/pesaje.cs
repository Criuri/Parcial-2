using Parcial_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parcial_2.Clases
{
    public class clsPesaje
    {
        private DBExamenEntities db = new DBExamenEntities();
        public Pesaje pesaje { get; set; }
        public Camion camion { get; set; }
        public List<FotoPesaje> fotos { get; set; }

        public string Insertar()
        {
            try
            {
                Camion camionExistente = db.Camions.FirstOrDefault(c => c.Placa == camion.Placa);
                if (camionExistente == null)
                {
                    db.Camions.Add(camion);
                    db.SaveChanges();
                }

                db.Pesajes.Add(pesaje);
                db.SaveChanges();

                if (fotos != null)
                {
                    foreach (var foto in fotos)
                    {
                        foto.idPesaje = pesaje.id;
                        db.FotoPesajes.Add(foto);
                    }
                    db.SaveChanges();
                }

                return "Pesaje insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el pesaje: " + ex.Message;
            }
        }

        public List<object> ConsultarPorPlaca(string placa)
        {
            var consulta = from p in db.Pesajes
                           join c in db.Camions on p.PlacaCamion equals c.Placa
                           where c.Placa == placa
                           select new
                           {
                               c.Placa,
                               c.Marca,
                               c.NumeroEjes,
                               p.FechaPesaje,
                               p.Peso,
                               Imagenes = db.FotoPesajes
                                   .Where(f => f.idPesaje == p.id)
                                   .Select(f => f.ImagenVehiculo)
                                   .ToList()
                           };

            return consulta.ToList<object>();
        }
    }
}