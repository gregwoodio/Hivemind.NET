-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [GangTerritories_Remove]
	-- Add the parameters for the stored procedure here
	@GangTerritoryId NVarChar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM dbo.GangTerritories WHERE gangTerritoryId = @GangTerritoryId;
END
