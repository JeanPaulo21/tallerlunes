using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IListaVisualizacionDA
    {
        Task<Guid> AgregarListaVisualizacion(ListaVisualizacionRequest listaVisualizacion);

        Task<Guid> EditarListaVisualizacion(Guid id, ListaVisualizacionRequest listaVisualizacion);

        Task<Guid> EliminarListaVisualizacion(Guid id);

        Task<ListaVisualizacionResponse> ObtenerListaVisualizacionPorId(Guid id);

        Task<IEnumerable<ListaVisualizacionResponse>> ObtenerListaVisualizacionPorUsuario(Guid usuarioId);

        Task<IEnumerable<ListaVisualizacionResponse>> ObtenerVisualizaciones();

    }
}
