CREATE PROCEDURE [dbo].[spClient_Update]
	@Id INT,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@CUIL NVARCHAR(11),
	@PhoneNumber NVARCHAR(50)
AS

BEGIN

SET NOCOUNT ON

UPDATE [dbo].[Clients]
	SET [FirstName] = @FirstName,
		[LastName] = @LastName,
		[CUIL] = @CUIL,
		[PhoneNumber] = @PhoneNumber
		WHERE [Id] = @Id

END

