
CREATE PROCEDURE [dbo].[SP_ConsultarFavoritos]
    @UsuarioID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT ID, UsuarioID, TMDB_ID, Tipo, Comentario, Puntuacion, FechaRegistro
    FROM Favoritos
    WHERE UsuarioID = @UsuarioID;
END