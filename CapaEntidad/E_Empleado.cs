using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class E_Empleado
    {

        public string EmpleadoID { get; set; }

        public int PuertoID { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateTime FechaDeNacimiento { get; set; }

        public string Superior { get; set; }

        public double Salario { get; set; }

        public string Cargo { get; set; }

    }
}
