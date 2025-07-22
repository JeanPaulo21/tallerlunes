namespace Abstracciones.Modelos.Servicios.TMDB
{
    public class GeneroTMDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GeneroTMDBRespuesta
    {
        public List<GeneroTMDB> Genres { get; set; }
    }
}
