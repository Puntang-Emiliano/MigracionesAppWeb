using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Modelo
{
    [Table("Articulo")]
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese nombre del artículo")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese nombre de la Categoria")]
        [ForeignKey(nameof(CategoriaId))]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public int? habilitada { get; set; }

        [Required]
        public decimal precio { get; set; }

        public string? Imagen { get; set; }

    }
}
