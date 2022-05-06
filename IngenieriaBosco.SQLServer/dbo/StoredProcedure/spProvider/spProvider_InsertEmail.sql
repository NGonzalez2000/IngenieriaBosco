CREATE PROCEDURE [dbo].[spProvider_InsertEmail]
	@ProviderId INT,
	@Email NVARCHAR(100)
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[ProviderEmails]
	VALUES(@ProviderId,@Email)

END
