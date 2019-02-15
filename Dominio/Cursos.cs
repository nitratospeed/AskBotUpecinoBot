using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Dominio
{
    public class Cursos
    {
        public int IDAlumno { get; set; }
        public int IDCurso { get; set; }
        public string Descripcion { get; set; }
        public string Nivel { get; set; }
        public string respuesta { get; set; }
    }
}