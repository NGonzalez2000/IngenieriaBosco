CREATE TABLE [dbo].[Currency]
(
	[Type] VARCHAR(3) NOT NULL UNIQUE,
	[Amount] MONEY NOT NULL
)
