CREATE PROCEDURE [dbo].[spClient_UpdateEmail]
	@Id INT,
	@Email NVARCHAR(100)	
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[ClientEmails]
	SET [Email] = @Email
		WHERE [Id] = @Id
		
END