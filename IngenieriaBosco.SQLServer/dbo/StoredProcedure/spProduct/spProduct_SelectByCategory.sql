CREATE PROCEDURE [dbo].[spProduct_SelectByCategory]
	@CategoryId INT
AS
BEGIN

SET NOCOUNT ON

SELECT [Id], [Code], [Description], [ListingPrice], [RetailPrice], [WholesalerPrice], [Stock], [WarningStock]
	FROM [dbo].[Products]
		WHERE [CategoryId] = @CategoryId

END
