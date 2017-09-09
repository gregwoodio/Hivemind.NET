-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.GangWeapons_AddGangWeapon
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100),
	@WeaponId INT,
	@GangWeaponId NVARCHAR(100) = null OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @GangWeaponId = NEWID();

    -- Insert statements for procedure hre
	INSERT INTO dbo.GangWeapons (gangId,weaponId, gangWeaponId)
	VALUES (@GangId, @WeaponId, @GangWeaponId);
	
	RETURN 0; 
END
