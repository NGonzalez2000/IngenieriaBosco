﻿CREATE TABLE [dbo].[ClientEmails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ClientId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Clients](Id),
	[Email] NVARCHAR(100) NOT NULL
)
