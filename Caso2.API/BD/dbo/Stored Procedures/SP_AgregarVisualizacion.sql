CREATE PROCEDURE [dbo].[SP_AgregarVisualizacion]
    @ID UNIQUEIDENTIFIER,
    @UsuarioID UNIQUEIDENTIFIER,
    @TMDB_ID INT,
    @Tipo NVARCHAR(10),
    @Prioridad INT,
    @Comentario NVARCHAR(500)
AS
BEGIN
    INSERT INTO ListaVisualizacion (ID, UsuarioID, TMDB_ID, Tipo, Prioridad, Comentario)
    VALUES (@ID, @UsuarioID, @TMDB_ID, @Tipo, @Prioridad, @Comentario);
END