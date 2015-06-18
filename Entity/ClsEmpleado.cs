using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ClsEmpleado
    {
        public string TipoDocumento { get; set; }
        public int Identificación { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }       
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Cargo { get; set; }
    }
}
