
CREATE PROCEDURE [dbo].[SP_DetalleVisualizacion]
    @ID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT ID, UsuarioID, TMDB_ID, Tipo, Prioridad, Comentario, FechaRegistro
    FROM ListaVisualizacion
    WHERE ID = @ID;
END