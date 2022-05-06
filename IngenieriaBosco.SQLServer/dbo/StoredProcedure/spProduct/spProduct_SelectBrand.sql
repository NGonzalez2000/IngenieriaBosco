CREATE PROCEDURE [dbo].[spProduct_SelectBrand]
	@Id INT
AS

BEGIN

SET NOCOUNT ON

SELECT        dbo.Brands.Id, dbo.Brands.BrandName as [Name],dbo.Brands.IsDolarValue
FROM            dbo.Products INNER JOIN
                         dbo.Brands ON dbo.Products.BrandId = dbo.Brands.Id
						 WHERE dbo.Products.Id = @Id

END
