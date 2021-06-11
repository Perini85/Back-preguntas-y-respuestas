IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [nombreUsuario] varchar(20) NOT NULL,
    [Password] varchar(50) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201106194858_Inicial', N'3.1.9');

GO

CREATE TABLE [Cuestionario] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] varchar(100) NOT NULL,
    [Descripcion] varchar(150) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [Activo] int NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_Cuestionario] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cuestionario_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Pregunta] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] varchar(100) NOT NULL,
    [CuestionarioId] int NOT NULL,
    CONSTRAINT [PK_Pregunta] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pregunta_Cuestionario_CuestionarioId] FOREIGN KEY ([CuestionarioId]) REFERENCES [Cuestionario] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Respuesta] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] varchar(50) NOT NULL,
    [esCorrecta] bit NOT NULL,
    [PreguntaId] int NOT NULL,
    CONSTRAINT [PK_Respuesta] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Respuesta_Pregunta_PreguntaId] FOREIGN KEY ([PreguntaId]) REFERENCES [Pregunta] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Cuestionario_UsuarioId] ON [Cuestionario] ([UsuarioId]);

GO

CREATE INDEX [IX_Pregunta_CuestionarioId] ON [Pregunta] ([CuestionarioId]);

GO

CREATE INDEX [IX_Respuesta_PreguntaId] ON [Respuesta] ([PreguntaId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201113052557_Inicial.2', N'3.1.9');

GO

