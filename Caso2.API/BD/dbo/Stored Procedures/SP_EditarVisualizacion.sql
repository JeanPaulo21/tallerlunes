
CREATE PROCEDURE [dbo].[SP_EditarVisualizacion]
    @ID UNIQUEIDENTIFIER,
    @Prioridad INT,
    @Comentario NVARCHAR(500)
AS
BEGIN
    UPDATE ListaVisualizacion
    SET Prioridad = @Prioridad,
        Comentario = @Comentario
    WHERE ID = @ID;
END