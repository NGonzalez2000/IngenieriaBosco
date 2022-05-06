CREATE PROCEDURE [dbo].[spProvider_DeleteEmail]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[ProviderEmails]
	WHERE [Id] = @Id

END
