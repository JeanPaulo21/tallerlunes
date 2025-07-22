-- Agregar Usuario
CREATE PROCEDURE SP_AgregarUsuario
    @ID UNIQUEIDENTIFIER,
    @Nombre NVARCHAR(100),
    @Correo NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Usuarios] (ID, Nombre, Correo)
    VALUES (@ID, @Nombre, @Correo);
END