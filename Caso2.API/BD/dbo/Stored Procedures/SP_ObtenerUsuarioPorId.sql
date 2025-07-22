
-- Obtener Usuario por ID
CREATE PROCEDURE SP_ObtenerUsuarioPorId
    @ID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID, Nombre, Correo
    FROM [dbo].[Usuarios]
    WHERE ID = @ID;
END