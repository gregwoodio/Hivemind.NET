-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Gangers_GetById]
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
	gangerId,
	gangId,
	name,
	type,
	move,
	weaponSkill,
	ballisticSkill,
	strength,
	toughness, 
	wounds,
	initiative,
	attack,
	leadership,
	experience,
	active,
	isOneEyed,
	isDeafened,
	isOneHanded,
	rightHandFingers,
	leftHandFingers,
	hasHorribleScars,
	hasImpressiveScars,
	hasHeadWound,
	hasOldBattleWound,
	IsCaptured,
	hasBitterEnmity,
	hasSporeSickness
	FROM Gangers WHERE gangerId = @GangerId;
END
