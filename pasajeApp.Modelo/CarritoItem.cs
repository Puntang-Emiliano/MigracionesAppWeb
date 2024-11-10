using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Modelo
{
    [Table("CarritoItem")]
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }

        [Required]
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Cantidad { get; set; }

        // Cálculo del subtotal para el artículo en base a la cantidad y el precio
        [NotMapped]
        public decimal Subtotal => Articulo.precio * Cantidad;
    }
}
