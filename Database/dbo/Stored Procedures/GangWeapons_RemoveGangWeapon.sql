-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.[GangWeapons_RemoveGangWeapon]
	-- Add the parameters for the stored procedure here
	@GangWeaponId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure hre
	DELETE FROM dbo.GangWeapons WHERE gangWeaponId = @GangWeaponId;
	
	RETURN 0; 
END
