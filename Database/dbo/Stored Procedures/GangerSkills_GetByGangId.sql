-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GangerSkills_GetByGangId] 
	@GangId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT gs.gangerId, gs.skillId
	FROM GangerSkills gs
	JOIN Gangers g ON g.gangerId = gs.gangerId
	JOIN Gangs gangs ON g.gangId = gangs.GangId
	WHERE gangs.gangId = @GangId;

END