namespace Abstracciones.Modelos.Servicios.TMDB
{
    public class PeliculaTMDB
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Poster_path { get; set; }
        public string Release_date { get; set; }
        public double Vote_average { get; set; }
    }

    public class PeliculasTMDBRespuesta
    {
        public int Page { get; set; }
        public List<PeliculaTMDB> Results { get; set; }
        public int Total_results { get; set; }
        public int Total_pages { get; set; }
    }
}
