CREATE PROCEDURE [dbo].[spProvider_SelectEmails]
	@ProviderId INT
AS

BEGIN

SET NOCOUNT ON

SELECT [Id], [Email] 
	FROM [dbo].[ProviderEmails]
		WHERE [ProviderId] = @ProviderId

END
