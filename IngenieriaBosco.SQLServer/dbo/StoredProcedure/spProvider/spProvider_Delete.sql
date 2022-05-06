CREATE PROCEDURE [dbo].[spProvider_Delete]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[ProviderEmails]
	WHERE [ProviderId] = @Id

DELETE FROM [dbo].[BrandProviders]
	WHERE [ProviderId] = @Id

DELETE FROM [dbo].[Providers]
	WHERE [Id] = @Id

END
