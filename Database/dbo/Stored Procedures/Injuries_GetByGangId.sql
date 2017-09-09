-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Injuries_GetByGangId]
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT i.injuryId, i.injuryName, i.description
	FROM Injuries i 
	JOIN GangerInjuries gi ON i.injuryId = gi.injuryId
	JOIN Gangers g ON g.gangerId = gi.gangerId
	JOIN Gangs ON Gangs.gangId = g.gangId;
END
