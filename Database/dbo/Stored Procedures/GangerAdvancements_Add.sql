-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GangerAdvancements_Add] 
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100),
	@AdvancementId NVARCHAR(100) OUTPUT
AS
BEGIN
	SET @AdvancementId = newid();
	INSERT INTO dbo.GangerAdvancements (gangerId, advancementId)
	VALUES (@GangerId, @AdvancementId);
END