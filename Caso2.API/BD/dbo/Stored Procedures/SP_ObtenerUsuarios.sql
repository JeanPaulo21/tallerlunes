
-- Obtener todos los Usuarios
CREATE PROCEDURE SP_ObtenerUsuarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID, Nombre, Correo
    FROM [dbo].[Usuarios];
END