-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GangerAdvancements_IsValid]
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100),
	@AdvancementId NVARCHAR(100),
	@Output INT OUTPUT
AS
BEGIN
	SELECT @Output = COUNT(*) FROM dbo.GangerAdvancements ga
	WHERE ga.gangerId = @GangerId
	AND ga.advancementId = @AdvancementId;
END