
-- Eliminar Usuario
CREATE PROCEDURE SP_EliminarUsuario
    @ID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Usuarios]
    WHERE ID = @ID;
END