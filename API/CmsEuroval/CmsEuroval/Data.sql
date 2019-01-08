IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[Pistas]'))
    SET IDENTITY_INSERT [Pistas] ON;
INSERT INTO [Pistas] ([Id], [Nombre])
VALUES (1, N'Padding'),
(2, N'Football'),
(3, N'Soccer');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[Pistas]'))
    SET IDENTITY_INSERT [Pistas] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Nombre') AND [object_id] = OBJECT_ID(N'[Socios]'))
    SET IDENTITY_INSERT [Socios] ON;
INSERT INTO [Socios] ([Id], [Email], [Nombre])
VALUES (1, N'micorreo@euroval.com', N'Jose'),
(2, N'micorre2o@euroval.com', N'Juan'),
(3, N'micorre3o@euroval.com', N'Miguel');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Nombre') AND [object_id] = OBJECT_ID(N'[Socios]'))
    SET IDENTITY_INSERT [Socios] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Duracion', N'FechaReserva', N'PistaId', N'SocioId') AND [object_id] = OBJECT_ID(N'[Reservas]'))
    SET IDENTITY_INSERT [Reservas] ON;
INSERT INTO [Reservas] ([Id], [Duracion], [FechaReserva], [PistaId], [SocioId])
VALUES (1, '02:54:00', '2019-01-30T21:31:13.9510000+01:00', 1, 1),
(4, '02:01:00', '2019-02-06T21:31:13.9540000+01:00', 2, 1),
(7, '00:10:00', '2019-06-26T21:31:13.9540000+02:00', 3, 1),
(2, '03:04:00', '2019-07-04T21:31:13.9540000+02:00', 1, 2),
(5, '01:06:00', '2019-08-27T21:31:13.9540000+02:00', 2, 2),
(8, '03:06:00', '2019-03-11T21:31:13.9540000+01:00', 3, 2),
(3, '02:19:00', '2019-04-24T21:31:13.9540000+02:00', 1, 3),
(6, '02:08:00', '2019-02-08T21:31:13.9540000+01:00', 2, 3),
(9, '01:40:00', '2019-02-10T21:31:13.9540000+01:00', 3, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Duracion', N'FechaReserva', N'PistaId', N'SocioId') AND [object_id] = OBJECT_ID(N'[Reservas]'))
    SET IDENTITY_INSERT [Reservas] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190103203114_DemoData', N'2.1.4-rtm-31024');

GO

