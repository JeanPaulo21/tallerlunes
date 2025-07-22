CREATE TABLE [dbo].[Favoritos] (
    [ID]            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UsuarioID]     UNIQUEIDENTIFIER NOT NULL,
    [TMDB_ID]       INT              NOT NULL,
    [Tipo]          NVARCHAR (10)    NOT NULL,
    [Comentario]    NVARCHAR (500)   NULL,
    [Puntuacion]    DECIMAL (3, 1)   NULL,
    [FechaRegistro] DATETIME         DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CHECK ([Tipo]='Serie' OR [Tipo]='Pelicula'),
    CONSTRAINT [FK_Favoritos_Usuarios] FOREIGN KEY ([UsuarioID]) REFERENCES [dbo].[Usuarios] ([ID])
);

