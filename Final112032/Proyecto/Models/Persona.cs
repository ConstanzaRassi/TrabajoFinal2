using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Edad { get; set; }
        public decimal Telefono { get; set; }
        public decimal Sexo { get; set; }
        public bool Casado { get; set; }
    }
}
