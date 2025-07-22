using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IFavoritoFlujo
    {
        Task<IEnumerable<FavoritoResponse>> ObtenerFavoritos(Guid usuarioId);

        Task<FavoritoResponse> ObtenerFavoritoPorId(Guid favoritoId);

        Task<Guid> AgregarFavorito(FavoritoRequest favorito);

        Task<Guid> EditarFavorito(Guid favoritoId, FavoritoRequest favorito);

        Task<Guid> EliminarFavorito(Guid favoritoId);

        Task<IEnumerable<FavoritoResponse>> ObtenerTodosFavoritos();

    }
}
