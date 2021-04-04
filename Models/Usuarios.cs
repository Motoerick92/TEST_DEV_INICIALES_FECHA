using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaDev.Models
{
    public partial class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El usuario no puede estar vacio.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña no debe estar vacia.")]
        public string Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Las constraseñas no coinciden")]
        [NotMapped]
        public string ConfirmaClave { get; set; }
        public string Sal { get; set; }
    }
}