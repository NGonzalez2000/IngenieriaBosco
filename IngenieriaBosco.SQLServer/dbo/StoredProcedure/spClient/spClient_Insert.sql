CREATE PROCEDURE [dbo].[spClient_Insert]
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@CUIL NVARCHAR(11),
	@PhoneNumber NVARCHAR(50)
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[Clients]
	VALUES(@FirstName,@LastName,@CUIL,@PhoneNumber)

END
