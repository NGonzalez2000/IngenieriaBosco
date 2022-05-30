CREATE PROCEDURE [dbo].[spProductOrder_Insert]
	@ProviderId INT,
	@Date VARCHAR(10),
	@ARGPrice MONEY,
	@USDPrice MONEY,
	@IsRecived BIT,
	@IsPayed BIT
AS

BEGIN

SET NOCOUNT ON

SET DATEFORMAT DMY

INSERT INTO [dbo].[ProductOrders]
	VALUES(@ProviderId,@Date,@ARGPrice,@USDPrice,@IsRecived,@IsPayed)

END
