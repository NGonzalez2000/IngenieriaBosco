CREATE PROCEDURE [dbo].[spProduct_GetCategory]
	@Id int
AS

BEGIN

SET NOCOUNT ON
SELECT  [dbo].[Categories].[Id], [dbo].[Categories].[CategoryName] as [Name]       
	FROM dbo.Products INNER JOIN
         dbo.Categories ON dbo.Products.CategoryId = dbo.Categories.Id
		 WHERE [dbo].[Products].[Id] = @Id

END

