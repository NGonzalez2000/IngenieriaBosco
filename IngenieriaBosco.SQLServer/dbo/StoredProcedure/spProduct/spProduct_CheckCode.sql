CREATE PROCEDURE [dbo].[spProduct_CheckCode]
	@Code NVARCHAR(40)
AS

BEGIN

SET NOCOUNT ON 

SELECT [Id], [Code], [Description], [ListingPrice], [RetailPrice], [WholesalerPrice], [Stock], [WarningStock]
	FROM [dbo].[Products]
		WHERE [Code] = @Code

END