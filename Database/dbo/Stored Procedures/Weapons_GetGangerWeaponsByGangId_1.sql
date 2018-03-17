-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Weapons_GetGangerWeaponsByGangId]
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	w.weaponId,
	w.weaponName,
	w.shortRange,
	w.longRange,
	w.hitShort,
	w.hitLong,
	w.strength,
	w.damage,
	w.saveMod,
	w.ammoRoll,
	w.type,
	w.cost,
	availability,
	description
	FROM Weapons w
	JOIN GangerWeapons gw ON w.weaponId = gw.weaponId
	JOIN Gangers g ON gw.gangerId = g.gangerId
	WHERE g.gangId = @GangId;
END