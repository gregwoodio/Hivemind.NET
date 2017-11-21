-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.Users_GetUserByGuid 
	-- Add the parameters for the stored procedure here
	@UserGUID NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT u.userId, u.username, u.password, u.userGUID FROM [dbo].[Users] u WHERE u.userGUID = @UserGUID;
END