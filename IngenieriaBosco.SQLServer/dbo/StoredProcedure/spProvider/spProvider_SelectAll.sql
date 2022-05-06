CREATE PROCEDURE [dbo].[spProvider_SelectAll]

AS

BEGIN

SET NOCOUNT ON

SELECT [Id], [ProviderName] as [Name], [CUIT], [PhoneNumber], [Web]
	FROM [dbo].[Providers]

END
