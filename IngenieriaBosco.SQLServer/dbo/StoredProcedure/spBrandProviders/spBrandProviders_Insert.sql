CREATE PROCEDURE [dbo].[spBrandProviders_Insert]
	@BrandId INT,
	@ProviderId INT
AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[BrandProviders]
	VALUES(@BrandId,@ProviderId)

END
