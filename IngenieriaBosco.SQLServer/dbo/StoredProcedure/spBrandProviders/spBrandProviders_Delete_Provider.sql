CREATE PROCEDURE [dbo].[spBrandProviders_Delete_Provider]
	@BrandId INT,
	@ProviderId INT

AS

BEGIN

SET NOCOUNT ON

DELETE FROM [dbo].[BrandProviders]
	WHERE [BrandId] = @BrandId and [ProviderId] = @ProviderId

END
