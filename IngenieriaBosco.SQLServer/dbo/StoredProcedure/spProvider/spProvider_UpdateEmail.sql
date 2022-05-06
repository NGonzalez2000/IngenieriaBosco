CREATE PROCEDURE [dbo].[spProvider_UpdateEmail]
	@Id INT,
	@Email NVARCHAR(100)
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[ProviderEmails]
	SET [Email] = @Email
		WHERE [Id] = @Id

END
