CREATE PROCEDURE [dbo].[SP_AgregarFavorito]
    @ID UNIQUEIDENTIFIER,
    @UsuarioID UNIQUEIDENTIFIER,
    @TMDB_ID INT,
    @Tipo NVARCHAR(10),
    @Comentario NVARCHAR(500),
    @Puntuacion DECIMAL(3,1)
AS
BEGIN
    INSERT INTO Favoritos (ID, UsuarioID, TMDB_ID, Tipo, Comentario, Puntuacion)
    VALUES (@ID, @UsuarioID, @TMDB_ID, @Tipo, @Comentario, @Puntuacion);
END