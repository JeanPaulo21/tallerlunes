using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Abstracciones.Modelos
{
    public class ListaVisualizacionRequest : TMDBBase
    {
        [Required]
        [JsonPropertyName("usuarioID")]
        public Guid UsuarioID { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "La prioridad debe estar entre 1 y 5")]
        [JsonPropertyName("prioridad")]
        public int Prioridad { get; set; }

        [StringLength(500, ErrorMessage = "El comentario no puede exceder los 500 caracteres")]
        [JsonPropertyName("comentario")]
        public string Comentario { get; set; }
    }

    public class ListaVisualizacionResponse : ListaVisualizacionRequest
    {
        [JsonPropertyName("id")]
        public Guid ID { get; set; }

        [JsonPropertyName("fechaRegistro")]
        public DateTime? FechaRegistro { get; set; }
    }
}
