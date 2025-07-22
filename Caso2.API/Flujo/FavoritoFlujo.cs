using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;


namespace Flujo
{
    public class FavoritoFlujo : IFavoritoFlujo
    {
        private readonly IFavoritoDA _favoritoDA;

        public FavoritoFlujo(IFavoritoDA favoritoDA)
        {
            _favoritoDA = favoritoDA;
        }

        public async Task<IEnumerable<FavoritoResponse>> ObtenerFavoritos(Guid usuarioId)
        {
            return await _favoritoDA.ObtenerFavoritos(usuarioId);
        }

        public async Task<FavoritoResponse> ObtenerFavoritoPorId(Guid favoritoId)
        {
            return await _favoritoDA.ObtenerFavoritoPorId(favoritoId);
        }

        public async Task<Guid> AgregarFavorito(FavoritoRequest favorito)
        {
            return await _favoritoDA.AgregarFavorito(favorito);
        }

        public async Task<Guid> EditarFavorito(Guid favoritoId, FavoritoRequest favorito)
        {
            return await _favoritoDA.EditarFavorito(favoritoId, favorito);
        }

        public async Task<Guid> EliminarFavorito(Guid favoritoId)
        {
            return await _favoritoDA.EliminarFavorito(favoritoId);
        }
        public async Task<IEnumerable<FavoritoResponse>> ObtenerTodosFavoritos()
        {
            return await _favoritoDA.ObtenerTodosFavoritos();
        }
    }
}
