CREATE PROCEDURE [dbo].[spClient_Delete]
	@Id INT

AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[ClientEmails]
	WHERE [ClientId] = @Id

DELETE FROM [dbo].[Clients]
	WHERE [Id] = @Id

END
