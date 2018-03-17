-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GangerInjuries_Add]
	-- Add the parameters for the stored procedure here
	@GangerInjuryId NVARCHAR(100) OUTPUT,
	@GangerId NVARCHAR(100),
	@InjuryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @GangerInjuryId = NEWID();

    -- Insert statements for procedure here
	INSERT INTO dbo.GangerInjuries 
	(gangerInjuryId, gangerId, injuryId)
	VALUES
	(@GangerInjuryId, @GangerId, @InjuryId);

	RETURN 0;
END