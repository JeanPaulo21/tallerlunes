
-- Editar Usuario
CREATE PROCEDURE SP_EditarUsuario
    @ID UNIQUEIDENTIFIER,
    @Nombre NVARCHAR(100),
    @Correo NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Usuarios]
    SET Nombre = @Nombre,
        Correo = @Correo
    WHERE ID = @ID;
END