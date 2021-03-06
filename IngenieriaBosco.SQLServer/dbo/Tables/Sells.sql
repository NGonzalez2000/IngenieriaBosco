CREATE TABLE [dbo].[Sells]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[FactN] VARCHAR(15) NOT NULL,
	[PtoVta] VARCHAR(8) NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[CUIL] VARCHAR(11) NOT NULL,
	[ResponsabilidadIVA] NVARCHAR(40) NOT NULL,
	[Currency] VARCHAR(3) NOT NULL,
	[TotalPrice] MONEY NOT NULL,
	[IsPayed] BIT NOT NULL,
	[IsRetailPrice] BIT NOT NULL,
	[Date] DATETIME NOT NULL

)
