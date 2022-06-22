CREATE PROCEDURE [dbo].[spSellItem_SelectBySellId]
	@SellId INT
AS

BEGIN

SET NOCOUNT ON

SELECT [Id],[Code],[Description],[Amount],[Price],[SubTotal],[Currency] FROM [dbo].[SellItems]
	WHERE [SellId] = @SellId

END
