CREATE PROCEDURE [dbo].[spProduct_Update]
	@Id INT,
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

UPDATE [dbo].[Products]
	SET [Description] = @Description,
		[Code] = @Code,
		[CategoryId] = @CategoryId,
		[BrandId] = @BrandId,
		[ListingPrice] = @ListingPrice,
		[RetailPrice] = @RetailPrice,
		[WholesalerPrice] = @WholesalerPrice,
		[Stock] = @Stock,
		[WarningStock] = @WarningStock
	WHERE [Id] = @Id

END
