
CREATE PROCEDURE [dbo].[SP_ConsultarVisualizacion]
    @UsuarioID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT ID, UsuarioID, TMDB_ID, Tipo, Prioridad, Comentario, FechaRegistro
    FROM ListaVisualizacion
    WHERE UsuarioID = @UsuarioID
    ORDER BY Prioridad ASC, FechaRegistro DESC;
END