
CREATE PROCEDURE [dbo].[SP_DetalleFavorito]
    @ID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT ID, UsuarioID, TMDB_ID, Tipo, Comentario, Puntuacion, FechaRegistro
    FROM Favoritos
    WHERE ID = @ID;
END