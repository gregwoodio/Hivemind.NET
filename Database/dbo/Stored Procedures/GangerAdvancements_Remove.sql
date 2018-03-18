-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GangerAdvancements_Remove] 
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100),
	@AdvancementId NVARCHAR(100)
AS
BEGIN
	DELETE FROM dbo.GangerAdvancements 
	WHERE
	gangerId = @GangerId 
	AND
	advancementId = @AdvancementId;
END