-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Gangers_AddGanger]
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100) OUTPUT,
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
	SET @GangerId = NEWID();

    -- Insert statements for procedure here
	INSERT INTO dbo.Gangers 
	(gangerId, gangId, name, type,  move, weaponSkill, ballisticSkill, strength, toughness, 
	wounds, initiative, attack, leadership, experience, active, isOneEyed,
	isDeafened, isOneHanded, rightHandFingers, leftHandFingers, hasHorribleScars,
	hasImpressiveScars, hasHeadWound, isCaptured, hasBitterEnmity)
	VALUES
	(@GangerId, @GangId, @Name, @Type, @Move, @WeaponSkill, @BallisticSkill, @Strength, @Toughness,
	@Wounds, @Initiative, @Attack, @Leadership, @Experience, @Active, @IsOneEyed,
	@IsDeafened, @IsOneHanded, @RightHandFingers, @LeftHandFingers, @HasHorribleScars,
	@HasImpressiveScars, @HasHeadWound, @IsCaptured, @HasBitterEnmity);

	RETURN 0;
END
