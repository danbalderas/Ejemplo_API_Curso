using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class CastForUpdateDto
    {
        [Required (ErrorMessage ="Nombre es requerido")]
        [MaxLength(50, ErrorMessage ="Maximo 50 caracteres")]
        public string Name { get; set; }
        [Required (ErrorMessage ="Personaje es requerido")]
        [MaxLength(50, ErrorMessage ="Maximo 50 caracteres")]
        public string Character { get; set; }
    }
}