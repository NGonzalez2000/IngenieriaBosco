CREATE PROCEDURE [dbo].[spClient_DeleteEmail]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[ClientEmails]
	WHERE [Id] = @Id

END
