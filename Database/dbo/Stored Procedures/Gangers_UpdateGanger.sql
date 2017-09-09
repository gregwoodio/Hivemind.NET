-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Gangers_UpdateGanger]
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100),
	@GangId NVARCHAR(100),
	@Name VARCHAR(100),
	@Type INT,
	@Move INT,
	@WeaponSkill INT,
	@BallisticSkill INT,
	@Strength INT,
	@Toughness INT,
	@Wounds INT,
	@Initiative INT,
	@Attack INT,
	@Leadership INT,
	@Experience INT,
	@Active TINYINT,
	@IsOneEyed TINYINT,
	@IsDeafened TINYINT,
	@IsOneHanded TINYINT,
	@RightHandFingers INT,
	@LeftHandFingers INT,
	@HasHorribleScars TINYINT,
	@HasImpressiveScars TINYINT,
	@HasOldBattleWound TINYINT,
	@HasHeadWound TINYINT,
	@IsCaptured TINYINT,
	@HasBitterEnmity TINYINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Gangers] SET
	gangId = @GangId,
	name = @Name,
	type = @Type,
	move = @Move,
	weaponSkill = @WeaponSkill,
	ballisticSkill = @BallisticSkill,
	strength = @Strength,
	toughness = @Toughness,
	wounds = @Wounds,
	initiative = @Initiative,
	attack = @Attack,
	leadership = @Leadership,
	experience = @Experience,
	active = @Active,
	isOneEyed = @IsOneEyed,
	isDeafened = @IsDeafened,
	isOneHanded = @IsOneHanded,
	rightHandFingers = @RightHandFingers,
	leftHandFingers = @LeftHandFingers,
	hasHorribleScars = @HasHorribleScars,
	hasImpressiveScars = @HasImpressiveScars,
	hasHeadWound = @HasHeadWound,
	isCaptured = @IsCaptured,
	hasBitterEnmity = @HasBitterEnmity
	WHERE gangerId = @GangerId;
END
