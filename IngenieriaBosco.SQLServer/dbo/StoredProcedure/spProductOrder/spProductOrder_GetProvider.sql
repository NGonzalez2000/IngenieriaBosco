CREATE PROCEDURE [dbo].[spProductOrder_GetProvider]
	@Id INT
AS

BEGIN 

SET NOCOUNT ON

SELECT [Id], [ProviderName] as [Name], [CUIT], [PhoneNumber], [Web]
	FROM [dbo].[Providers]
		WHERE [Id] = ANY(SELECT [ProviderId] FROM [dbo].[ProductOrders] WHERE [Id] = @Id)

END
