CREATE PROCEDURE [dbo].[spCategory_SelectAll]

AS

BEGIN

SET NOCOUNT ON

SELECT [Id] AS Id, [CategoryName] AS [Name] FROM [dbo].[Categories]

END
