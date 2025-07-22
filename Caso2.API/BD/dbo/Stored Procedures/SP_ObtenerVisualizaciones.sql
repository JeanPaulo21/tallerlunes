CREATE PROCEDURE SP_ObtenerVisualizaciones  
AS
BEGIN
    SELECT ID, UsuarioID, TMDB_ID, Tipo, Prioridad, Comentario, FechaRegistro
    FROM ListaVisualizacion;
END;