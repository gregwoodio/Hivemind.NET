-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Weapons_GetByGangerId]
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	w.weaponId,
	gw.gangerId,
	weaponName,
	shortRange,
	longRange,
	hitShort,
	hitLong,
	strength,
	damage,
	saveMod,
	ammoRoll,
	type,
	cost,
	availability,
	description
	FROM Weapons w
	JOIN GangerWeapons gw
	ON w.weaponId = gw.weaponId
	WHERE gw.gangerId = @GangerId;
END
