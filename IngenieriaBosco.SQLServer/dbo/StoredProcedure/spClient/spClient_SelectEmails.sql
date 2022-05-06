CREATE PROCEDURE [dbo].[spClient_SelectEmails]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

SELECT [Id], [Email] FROM [dbo].[ClientEmails]
	WHERE [ClientId] = @Id

END