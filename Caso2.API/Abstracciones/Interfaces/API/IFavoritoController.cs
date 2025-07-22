using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IFavoritoController
    {
        Task<IActionResult> ObtenerFavoritos(Guid usuarioId);
        Task<IActionResult> ObtenerFavoritoPorId(Guid favoritoId);
        Task<IActionResult> AgregarFavorito(FavoritoRequest favorito);
        Task<IActionResult> EditarFavorito(Guid favoritoId, FavoritoRequest favorito);
        Task<IActionResult> EliminarFavorito(Guid favoritoId);
        Task<IActionResult> ObtenerTodosFavoritos();
    }
}
