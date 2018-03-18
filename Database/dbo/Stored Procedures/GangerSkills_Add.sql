-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GangerSkills_Add]
	-- Add the parameters for the stored procedure here
	@GangerId NVARCHAR(100), 
	@SkillId INT,
	@GangerSkillId NVARCHAR(100) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET @GangerSkillId = NEWID();

    -- Insert statements for procedure here
	INSERT INTO dbo.GangerSkills (gangerId, gangerSkillId, skillId)
	VALUES (@GangerId, @GangerSkillId, @SkillId);
END