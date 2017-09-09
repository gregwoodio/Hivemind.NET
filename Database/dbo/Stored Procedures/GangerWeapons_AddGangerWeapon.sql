-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.GangerWeapons_AddGangerWeapon
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100),
	@WeaponId INT,
	@GangerWeaponId NVARCHAR(100) = null OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @GangerWeaponId = NEWID();

    -- Insert statements for procedure hre
	INSERT INTO dbo.GangerWeapons (gangerId,weaponId, gangerWeaponId)
	VALUES (@GangerId, @WeaponId, @GangerWeaponId);
	
	RETURN 0; 
END
