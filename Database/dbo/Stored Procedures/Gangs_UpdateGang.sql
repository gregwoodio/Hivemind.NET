CREATE PROCEDURE [dbo].[Gangs_UpdateGang]
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100),
	@Name VARCHAR(100),
	@House INT,
	@Credits INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Gangs] SET
	gangName = @Name,
	house = @House,
	credits = @Credits
	WHERE gangId = @GangId;
END
