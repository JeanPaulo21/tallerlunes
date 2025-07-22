
CREATE PROCEDURE [dbo].[SP_EditarFavorito]
    @ID UNIQUEIDENTIFIER,
    @Comentario NVARCHAR(500),
    @Puntuacion DECIMAL(3,1)
AS
BEGIN
    UPDATE Favoritos
    SET Comentario = @Comentario,
        Puntuacion = @Puntuacion
    WHERE ID = @ID;
END