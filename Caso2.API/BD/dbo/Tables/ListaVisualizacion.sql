CREATE TABLE [dbo].[ListaVisualizacion] (
    [ID]            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UsuarioID]     UNIQUEIDENTIFIER NOT NULL,
    [TMDB_ID]       INT              NOT NULL,
    [Tipo]          NVARCHAR (10)    NOT NULL,
    [Prioridad]     INT              NOT NULL,
    [Comentario]    NVARCHAR (500)   NULL,
    [FechaRegistro] DATETIME         DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CHECK ([Prioridad]>=(1) AND [Prioridad]<=(5)),
    CHECK ([Tipo]='Serie' OR [Tipo]='Pelicula'),
    CONSTRAINT [FK_ListaVisualizacion_Usuarios] FOREIGN KEY ([UsuarioID]) REFERENCES [dbo].[Usuarios] ([ID])
);

