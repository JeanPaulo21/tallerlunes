CREATE PROCEDURE SP_ObtenerFavoritos
AS
BEGIN
    SELECT ID, UsuarioID, TMDB_ID, Tipo, Comentario, Puntuacion, FechaRegistro
    FROM Favoritos;
END;