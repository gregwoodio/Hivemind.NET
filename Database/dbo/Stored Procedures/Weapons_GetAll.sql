-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Weapons_GetAll]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
	weaponId,
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
	FROM dbo.Weapons;
END
