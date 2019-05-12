using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class E_OrdenDeArrendamiento
    {
        public int OrdenID { get; set; }
        public string ClienteID { get; set; }
        public int PuertoID { get; set; }
        public int PuestoID { get; set; }
        public string EmpleadoID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double Cuota { get; set; }


    }
}
