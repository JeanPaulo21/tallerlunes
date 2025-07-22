using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IListaVisualizacionController
    {
        Task<IActionResult> ObtenerLista(Guid usuarioId);
        Task<IActionResult> ObtenerItemPorId(Guid itemId);
        Task<IActionResult> AgregarItem(ListaVisualizacionRequest item);
        Task<IActionResult> EditarItem(Guid itemId, ListaVisualizacionRequest item);
        Task<IActionResult> EliminarItem(Guid itemId);

        Task<IActionResult> ObtenerVisualizaciones();
    }
}
