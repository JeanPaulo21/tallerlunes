
CREATE PROCEDURE [dbo].[SP_EliminarVisualizacion]
    @ID UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM ListaVisualizacion
    WHERE ID = @ID;
END