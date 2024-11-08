using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace pasajeApp.Modelo
{
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = "El Campo Nombre es Obligatorio")]
        public string Name { get; set; }
        public int Edad { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
