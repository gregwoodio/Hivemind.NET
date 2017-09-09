-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GangTerritories_Add]
	-- Add the parameters for the stored procedure here
	@GangId NVARCHAR(100),
	@TerritoryId INT,
	@GangTerritoryId NVARCHAR(100) = null OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @GangTerritoryId = NEWID();

    -- Insert statements for procedure here
	INSERT INTO dbo.GangTerritories
	(gangTerritoryId, gangId, territoryId)
	VALUES 
	(@GangTerritoryId, @GangId, @TerritoryId);

	RETURN 0;
END
