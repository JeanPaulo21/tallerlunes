public class SerieTMDB
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Overview { get; set; }
    public string Poster_path { get; set; }
}

public class SerieTMDBRespuesta
{
    public List<SerieTMDB> Results { get; set; }
}
