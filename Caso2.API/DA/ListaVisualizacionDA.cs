using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DA
{
    public class ListaVisualizacionDA : IListaVisualizacionDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public ListaVisualizacionDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerReposiorio();
        }

        public async Task<Guid> AgregarListaVisualizacion(ListaVisualizacionRequest listaVisualizacion)
        {
            var idNuevo = Guid.NewGuid();
            var sp = "SP_AgregarVisualizacion";

            await _sqlConnection.ExecuteAsync(sp, new
            {
                ID = idNuevo,
                UsuarioID = listaVisualizacion.UsuarioID,
                TMDB_ID = listaVisualizacion.TMDB_ID,
                Tipo = listaVisualizacion.Tipo,
                Prioridad = listaVisualizacion.Prioridad,
                Comentario = listaVisualizacion.Comentario
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idNuevo;
        }

        public async Task<Guid> EditarListaVisualizacion(Guid id, ListaVisualizacionRequest listaVisualizacion)
        {
            await VerificarExiste(id);

            var sp = "SP_EditarVisualizacion";

            await _sqlConnection.ExecuteAsync(sp, new
            {
                ID = id,
                Prioridad = listaVisualizacion.Prioridad,
                Comentario = listaVisualizacion.Comentario
            }, commandType: System.Data.CommandType.StoredProcedure);

            return id;
        }

        public async Task<Guid> EliminarListaVisualizacion(Guid id)
        {
            await VerificarExiste(id);

            var sp = "SP_EliminarVisualizacion";

            await _sqlConnection.ExecuteAsync(sp, new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);

            return id;
        }

        public async Task<ListaVisualizacionResponse> ObtenerListaVisualizacionPorId(Guid id)
        {
            var sp = "SP_DetalleVisualizacion";

            var resultado = await _sqlConnection.QueryAsync<ListaVisualizacionResponse>(sp, new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);

            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<ListaVisualizacionResponse>> ObtenerListaVisualizacionPorUsuario(Guid usuarioId)
        {
            var sp = "SP_ConsultarVisualizacion";

            var resultado = await _sqlConnection.QueryAsync<ListaVisualizacionResponse>(sp, new { UsuarioID = usuarioId }, commandType: System.Data.CommandType.StoredProcedure);

            return resultado;
        }

        public async Task<IEnumerable<ListaVisualizacionResponse>> ObtenerVisualizaciones()
        {
            string sp = "SP_ObtenerVisualizaciones";

            var resultado = await _sqlConnection.QueryAsync<ListaVisualizacionResponse>(
                sp,
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultado;
        }

        private async Task VerificarExiste(Guid id)
        {
            var entidad = await ObtenerListaVisualizacionPorId(id);
            if (entidad == null)
                throw new Exception("Elemento no encontrado");
        }
    }
}
