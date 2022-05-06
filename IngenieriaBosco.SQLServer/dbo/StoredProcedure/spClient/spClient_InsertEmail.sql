CREATE PROCEDURE [dbo].[spClient_InsertEmail]
	@ClientId INT,
	@Email NVARCHAR(100)
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[ClientEmails]
	VALUES(@ClientId, @Email)

END
