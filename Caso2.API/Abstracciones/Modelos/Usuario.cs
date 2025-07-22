using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos {
public class UsuarioBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo debe tener un formato válido")]
        public string Correo { get; set; }
    }
    public class UsuarioRequest : UsuarioBase
    {
        // Para creación usualmente no envías ID, 
        // pero si quieres actualizar debe venir el ID para PUT
        // Puedes agregarlo opcionalmente aquí si gustas:

        [Required(ErrorMessage = "El ID es obligatorio para editar")]
        public Guid? ID { get; set; }
    }
    public class UsuarioResponse : UsuarioBase
    {
        public Guid ID { get; set; }
    }
}