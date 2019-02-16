using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Dominio
{
    public class PreguntaQueue
    {
        public string idPregunta { get; set; }
        public string PreguntaTxt { get; set; }
        public string idCurso { get; set; }
        public string idUsuario { get; set; }
    }
}