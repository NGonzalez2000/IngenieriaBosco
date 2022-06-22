CREATE PROCEDURE [dbo].[spSellItem_Insert]
	@SellId INT,
	@Code NVARCHAR(40),
	@Description TEXT,
	@Amount INT,
	@Price MONEY,
	@SubTotal MONEY,
	@Currency VARCHAR(3)
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[SellItems]
	VALUES(@SellId,@Code,@Description,@Amount,@Price,@SubTotal,@Currency)

END
