using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Abstracciones.Modelos
{
    public class TMDBBase
    {
        [Required]
        public int TMDB_ID { get; set; }

        [Required]
        [RegularExpression("^(Pelicula|Serie)$", ErrorMessage = "Tipo debe ser 'Pelicula' o 'Serie'")]
        public string Tipo { get; set; }
    }

    public class FavoritoRequest : TMDBBase
    {
        [Required]
        [JsonPropertyName("usuarioID")]
        public Guid UsuarioID { get; set; }

        [StringLength(500, ErrorMessage = "El comentario no puede exceder los 500 caracteres")]
        public string Comentario { get; set; }

        [Range(0, 10, ErrorMessage = "La puntuación debe estar entre 0 y 10")]
        public decimal Puntuacion { get; set; }
    }

    public class FavoritoResponse : FavoritoRequest
    {
        [JsonPropertyName("id")]
        public Guid ID { get; set; }

        [JsonPropertyName("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }
    }

}
