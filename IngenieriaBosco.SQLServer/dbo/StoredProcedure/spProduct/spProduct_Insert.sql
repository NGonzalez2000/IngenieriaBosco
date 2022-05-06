CREATE PROCEDURE [dbo].[spProduct_Insert]
	@Description text,
	@Code NVARCHAR(40),
	@CategoryId INT,
	@BrandId INT,
	@ListingPrice MONEY,
	@RetailPrice MONEY,
	@WholesalerPrice MONEY,
	@Stock INT,
	@WarningStock INT
AS
BEGIN

SET NOCOUNT ON

	INSERT INTO [dbo].[Products]
		VALUES(@Description,@Code,@CategoryId,@BrandId,
			@ListingPrice,@RetailPrice,@WholesalerPrice,
				@Stock,@WarningStock)

END
