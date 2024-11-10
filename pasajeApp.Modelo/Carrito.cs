using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Modelo
{
    [Table("Carrito")]
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Relación uno a muchos con CarritoItem
        public ICollection<CarritoItem> CarritoItems { get; set; } = new List<CarritoItem>();

        // Método para calcular el total del carrito
        [NotMapped]
        public decimal Total => CarritoItems.Sum(item => item.Subtotal);
    }
}
