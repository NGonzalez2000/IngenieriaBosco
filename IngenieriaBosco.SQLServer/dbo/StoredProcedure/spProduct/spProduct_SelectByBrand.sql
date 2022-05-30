CREATE PROCEDURE [dbo].[spProduct_SelectByBrand]
	@BrandId INT
AS
BEGIN

SET NOCOUNT ON

SELECT [Id], [Code], [Description], [ListingPrice], [RetailPrice], [WholesalerPrice], [Stock], [WarningStock]
	FROM [dbo].[Products]
		WHERE BrandId = @BrandId

END
