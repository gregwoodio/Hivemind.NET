-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Gangs_AddGang]
	-- Add the parameters for the stored procedure here
	@GangName NVARCHAR(100),
	@House INT, 
	@Credits INT,
	@GangId NVARCHAR(100) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @GangId = NEWID();

    -- Insert statements for procedure here
	INSERT INTO dbo.Gangs (gangId, gangName, house, credits) VALUES (
		@GangId, @GangName, @House, @Credits
	);
	
	RETURN 0;
END
