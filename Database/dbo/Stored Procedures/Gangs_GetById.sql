CREATE PROCEDURE [dbo].[Gangs_GetById]
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
	gangId,
	gangName,
	house,
	credits
	FROM Gangs WHERE gangId = @GangId;
END
