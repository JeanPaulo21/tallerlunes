using Abstracciones.Interfaces.Reglas;

public class ValidacionPuntuacion : IValidacionPuntuacion
{
    public bool EsValida(decimal? puntuacion)
    {
        return puntuacion.HasValue && puntuacion.Value >= 0 && puntuacion.Value <= 10;
    }
}
