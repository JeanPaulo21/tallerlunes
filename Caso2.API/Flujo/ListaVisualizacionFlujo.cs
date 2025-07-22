using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ListaVisualizacionFlujo : IListaVisualizacionFlujo
    {
        private readonly IListaVisualizacionDA _listaVisualizacionDA;

        public ListaVisualizacionFlujo(IListaVisualizacionDA listaVisualizacionDA)
        {
            _listaVisualizacionDA = listaVisualizacionDA;
        }

        public async Task<IEnumerable<ListaVisualizacionResponse>> ObtenerLista(Guid usuarioId)
        {
            return await _listaVisualizacionDA.ObtenerListaVisualizacionPorUsuario(usuarioId);
        }

        public async Task<ListaVisualizacionResponse> ObtenerItemPorId(Guid itemId)
        {
            return await _listaVisualizacionDA.ObtenerListaVisualizacionPorId(itemId);
        }

        public async Task<Guid> AgregarItem(ListaVisualizacionRequest item)
        {
            return await _listaVisualizacionDA.AgregarListaVisualizacion(item);
        }

        public async Task<Guid> EditarItem(Guid itemId, ListaVisualizacionRequest item)
        {
            return await _listaVisualizacionDA.EditarListaVisualizacion(itemId, item);
        }

        public async Task<Guid> EliminarItem(Guid itemId)
        {
            return await _listaVisualizacionDA.EliminarListaVisualizacion(itemId);
        }

        public async Task<IEnumerable<ListaVisualizacionResponse>> ObtenerVisualizaciones()
        {
            return await _listaVisualizacionDA.ObtenerVisualizaciones();
        }
    }
}
