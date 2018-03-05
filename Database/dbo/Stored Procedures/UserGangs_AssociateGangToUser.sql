-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UserGangs_AssociateGangToUser]
	-- Add the parameters for the stored procedure here
	@UserGUID nvarchar(100),
	@GangId nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.UserGangs (userId, gangId) VALUES (@UserGUID, @GangId);
END