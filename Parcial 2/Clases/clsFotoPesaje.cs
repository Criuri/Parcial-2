using Parcial_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parcial_2.Clases
{
    public class clsFotoPesaje
    {
        private DBExamenEntities db = new DBExamenEntities();
        public FotoPesaje fotoPesaje { get; set; }

        public string Insertar()
        {
            try
            {
                db.FotoPesajes.Add(fotoPesaje);
                db.SaveChanges();
                return "Foto insertada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar la foto: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                FotoPesaje f = db.FotoPesajes.FirstOrDefault(x => x.idFotoPesaje == fotoPesaje.idFotoPesaje);
                if (f == null) return "Foto no encontrada";

                f.ImagenVehiculo = fotoPesaje.ImagenVehiculo;
                db.SaveChanges();
                return "Foto actualizada";
            }
            catch (Exception ex)
            {
                return "Error al actualizar: " + ex.Message;
            }
        }

        public string Eliminar(int id)
        {
            try
            {
                FotoPesaje f = db.FotoPesajes.FirstOrDefault(x => x.idFotoPesaje == id);
                if (f == null) return "Foto no encontrada";

                db.FotoPesajes.Remove(f);
                db.SaveChanges();
                return "Foto eliminada";
            }
            catch (Exception ex)
            {
                return "Error al eliminar: " + ex.Message;
            }
        }

        public List<FotoPesaje> ConsultarPorPesaje(int idPesaje)
        {
            return db.FotoPesajes.Where(f => f.idPesaje == idPesaje).ToList();
        }
    }
}