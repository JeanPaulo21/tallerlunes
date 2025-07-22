using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IListaVisualizacionFlujo
    {
        Task<IEnumerable<ListaVisualizacionResponse>> ObtenerLista(Guid usuarioId);

        Task<ListaVisualizacionResponse> ObtenerItemPorId(Guid itemId);

        Task<Guid> AgregarItem(ListaVisualizacionRequest item);

        Task<Guid> EditarItem(Guid itemId, ListaVisualizacionRequest item);

        Task<Guid> EliminarItem(Guid itemId);

        Task<IEnumerable<ListaVisualizacionResponse>> ObtenerVisualizaciones();
    }
}
