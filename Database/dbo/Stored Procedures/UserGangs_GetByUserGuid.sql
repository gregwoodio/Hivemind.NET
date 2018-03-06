-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UserGangs_GetByUserGuid]
	-- Add the parameters for the stored procedure here
	@UserGUID nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT g.gangId, g.gangName FROM dbo.Gangs g 
	INNER JOIN dbo.UserGangs ug
	ON ug.gangId = g.gangId
	INNER JOIN dbo.Users u
	ON u.userGUID = ug.userId
	WHERE u.userGUID = @UserGUID;
END