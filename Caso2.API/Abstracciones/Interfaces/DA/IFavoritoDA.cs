using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IFavoritoDA
    {
        Task<Guid> AgregarFavorito(FavoritoRequest favorito);

        Task<Guid> EditarFavorito(Guid favoritoId, FavoritoRequest favorito);

        Task<Guid> EliminarFavorito(Guid favoritoId);

        Task<FavoritoResponse> ObtenerFavoritoPorId(Guid favoritoId);

        Task<IEnumerable<FavoritoResponse>> ObtenerFavoritos(Guid usuarioId);

        Task<IEnumerable<FavoritoResponse>> ObtenerTodosFavoritos();

    }
}
