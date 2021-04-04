using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaDev.Models
{
    [Table("Tb_PersonasFisicas")]
    public partial class TbPersonasFisicas
    {
        [Key]
        public int IdPersonaFisica { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? FechaRegistro { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? FechaActualizacion { get; set; }        

        [StringLength(50)]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(3, ErrorMessage = "El nombre debe ser minimo de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre debe ser maximo de 50 caracteres")]
        public string Nombre { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [MinLength(3, ErrorMessage = "El apellido paterno debe ser minimo de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El apellido paterno debe ser maximo de 50 caracteres")]
        public string ApellidoPaterno { get; set; }        

        [StringLength(50)]
        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        [MinLength(3, ErrorMessage = "El apellido materno debe ser minimo de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El apellido materno debe ser maximo de 50 caracteres")]
        public string ApellidoMaterno { get; set; }
        
        [Column("RFC")]        
        [StringLength(13)]
        [Required(ErrorMessage = "El codigo es obligatorio")]
        [MinLength(12, ErrorMessage = "El codigo debe ser minimo de 13 caracteres")]
        [MaxLength(14, ErrorMessage = "El codigo debe ser maximo de 13 caracteres")]        
        public string Rfc { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date, ErrorMessage = "La fecha no es valida")]
        public DateTime? FechaNacimiento { get; set; }
        public int? UsuarioAgrega { get; set; }
        public bool? Activo { get; set; }
     

       
        //Campos calculados
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // public string NombreApellido { get; set; }
    }
}
