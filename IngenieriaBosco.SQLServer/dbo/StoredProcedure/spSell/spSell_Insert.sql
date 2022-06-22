CREATE PROCEDURE [dbo].[spSell_Insert]
	@FactN VARCHAR(15),
	@PtoVta VARCHAR(8),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@CUIL VARCHAR(11),
	@ResponsabilidadIVA NVARCHAR(40),
	@Currency VARCHAR(3),
	@TotalPrice MONEY,
	@IsPayed BIT,
	@IsRetailPrice BIT,
	@Date VARCHAR(10)
AS

BEGIN

SET NOCOUNT ON

SET DATEFORMAT DMY

INSERT INTO [dbo].[Sells]
	VALUES(@FactN,@PtoVta,@FirstName,@LastName,@CUIL,@ResponsabilidadIVA,@Currency,@TotalPrice,@IsPayed,@IsRetailPrice,@Date)

END
