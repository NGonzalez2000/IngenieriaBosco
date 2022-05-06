CREATE PROCEDURE [dbo].[spProvider_Update]
	@Id INT,
	@Name NVARCHAR(100),
	@PhoneNumber NVARCHAR(30),
	@CUIT VARCHAR(11),
	@Web NVARCHAR(100)
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[Providers]
	SET [ProviderName] = @Name, [PhoneNumber] = @PhoneNumber,
		[CUIT] = @CUIT, [Web] = @Web
		WHERE [Id] = @Id

END