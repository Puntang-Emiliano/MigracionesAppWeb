using System.ComponentModel.DataAnnotations;

namespace pasajeApp.Modelo
{
    public class Categoria
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre de Categoria")]
        [StringLength(50)]
        public string Nombre { get; set; }
        public int? habilitada { get; set; }

    }
}
