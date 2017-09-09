-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.GangerWeapons_RemoveGangerWeapon
	-- Add the parameters for the stored procedure here
	@GangerWeaponId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure hre
	DELETE FROM dbo.GangerWeapons WHERE gangerWeaponId = @GangerWeaponId;
	
	RETURN 0; 
END
