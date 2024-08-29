using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Modelo
{
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }
        [Required(ErrorMessage = "el Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Descripcion { get; set; }

        [Display (Name = "Fecha de Creacion")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display (Name = "Imagen")]

        public string urlImagen { get; set; }

        [Required (ErrorMessage ="La Categoria es Obligatoria")]
        public int CategoriaId { get; set; }

        [ForeignKey ("CategoriaId")]
        public Categoria Categoria { get; set; }



    }
}
