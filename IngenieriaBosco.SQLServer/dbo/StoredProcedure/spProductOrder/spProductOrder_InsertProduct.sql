CREATE PROCEDURE [dbo].[spProductOrder_InsertProduct]
	@OrderId INT,
	@Code NVARCHAR(40),
	@Description TEXT,
	@ListingPrice MONEY,
	@Multiplier INT
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[ProductOrderList]
	VALUES(@OrderId,@Code,@Description,@ListingPrice,@Multiplier)

END
