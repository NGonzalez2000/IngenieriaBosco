CREATE TABLE [dbo].[Providers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProviderName] NVARCHAR(100) NOT NULL,
	[PhoneNumber] NVARCHAR (30) NOT NULL,
	[CUIT] VARCHAR(11) NOT NULL UNIQUE,
	[Web] NVARCHAR(100) NOT NULL
)
