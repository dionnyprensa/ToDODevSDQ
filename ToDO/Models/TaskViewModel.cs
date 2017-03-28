using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDO.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [DisplayName("Titulo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Titulo requerido.")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Debe tener entre 1 y 32 caracteres.")]
        public string Title { get; set; }

        [DisplayName("Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Descripcion requerida.")]
        [StringLength(254, MinimumLength = 1, ErrorMessage = "Maximo 254 caracteres.")]
        public string Description { get; set; }

        [DisplayName("¿Completada?")]
        public bool IsCompleted { get; set; }
    }
}