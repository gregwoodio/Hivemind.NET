CREATE PROCEDURE [dbo].[GangerWeapons_GetByGangId]
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		g.gangerId, 
		w.weaponName
	FROM Gangers g
	JOIN GangerWeapons gw ON g.gangerId = gw.gangerId
	JOIN Weapons w ON gw.weaponId = w.weaponId
	WHERE g.gangId = @GangId;
	
END