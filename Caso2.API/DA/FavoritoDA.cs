using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DA
{
    public class FavoritoDA : IFavoritoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public FavoritoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerReposiorio();
        }

        public async Task<Guid> AgregarFavorito(FavoritoRequest favorito)
        {
            string sp = "SP_AgregarFavorito";
            var idNuevo = Guid.NewGuid();

            await _sqlConnection.ExecuteAsync(sp, new
            {
                ID = idNuevo,
                UsuarioID = favorito.UsuarioID,
                TMDB_ID = favorito.TMDB_ID,
                Tipo = favorito.Tipo,
                Comentario = favorito.Comentario,
                Puntuacion = favorito.Puntuacion
            }, commandType: System.Data.CommandType.StoredProcedure);

            return idNuevo;
        }

        public async Task<Guid> EditarFavorito(Guid favoritoId, FavoritoRequest favorito)
        {
            await VerificarFavoritoExiste(favoritoId);

            string sp = "SP_EditarFavorito";

            await _sqlConnection.ExecuteAsync(sp, new
            {
                ID = favoritoId,
                Comentario = favorito.Comentario,
                Puntuacion = favorito.Puntuacion
            }, commandType: System.Data.CommandType.StoredProcedure);

            return favoritoId;
        }

        public async Task<Guid> EliminarFavorito(Guid favoritoId)
        {
            await VerificarFavoritoExiste(favoritoId);

            string sp = "SP_EliminarFavorito";

            await _sqlConnection.ExecuteAsync(sp, new { ID = favoritoId }, commandType: System.Data.CommandType.StoredProcedure);

            return favoritoId;
        }

        public async Task<IEnumerable<FavoritoResponse>> ObtenerFavoritos(Guid usuarioId)
        {
            string sp = "SP_ConsultarFavoritos";
            var resultado = await _sqlConnection.QueryAsync<FavoritoResponse>(sp, new { UsuarioID = usuarioId }, commandType: System.Data.CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<FavoritoResponse> ObtenerFavoritoPorId(Guid favoritoId)
        {
            string sp = "SP_DetalleFavorito";
            var resultado = await _sqlConnection.QueryAsync<FavoritoResponse>(sp, new { ID = favoritoId }, commandType: System.Data.CommandType.StoredProcedure);
            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<FavoritoResponse>> ObtenerTodosFavoritos()
        {
            string sp = "SP_ObtenerFavoritos";

            var resultado = await _sqlConnection.QueryAsync<FavoritoResponse>(
                sp,
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultado;
        }

        private async Task VerificarFavoritoExiste(Guid favoritoId)
        {
            var favorito = await ObtenerFavoritoPorId(favoritoId);
            if (favorito == null)
                throw new Exception("No se encontró el favorito");
        }
    }
}
