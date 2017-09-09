-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Territories_GetByGangId]
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	t.territoryId,
	t.name,
	t.income,
	t.description,
	gt.gangId,
	gt.gangTerritoryId
	FROM Territories t
	JOIN GangTerritories gt ON t.territoryId = gt.territoryId
	WHERE gt.gangId = @GangId;
END
