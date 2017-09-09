-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Injuries_GetById]
	-- Add the parameters for the stored procedure here
	@InjuryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	injuryId,
	injuryName,
	description
	FROM dbo.Injuries WHERE injuryId = @InjuryId;
END
