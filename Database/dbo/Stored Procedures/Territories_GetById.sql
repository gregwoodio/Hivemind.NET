-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Territories_GetById]
	-- Add the parameters for the stored procedure here
	@TerritoryId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
	territoryId,
	name,
	description,
	income
	FROM Territories WHERE territoryId = @TerritoryId;
END
