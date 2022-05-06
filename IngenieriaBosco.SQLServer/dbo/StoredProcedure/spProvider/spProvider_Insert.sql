CREATE PROCEDURE [dbo].[spProvider_Insert]
	@Name NVARCHAR(100),
	@PhoneNumber NVARCHAR(30),
	@CUIT VARCHAR(11),
	@Web NVARCHAR(100)
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[Providers]
	VALUES(@Name,@PhoneNumber,@CUIT,@Web)

END
