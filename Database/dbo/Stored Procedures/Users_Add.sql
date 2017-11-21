-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Users_Add] 
	-- Add the parameters for the stored procedure here
	@Username NVARCHAR(255),
	@Password NVARCHAR(100),
	@UserGUID NVARCHAR(100) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @UserGUID = NEWID();

    -- Insert statements for procedure here
	INSERT INTO dbo.Users (userGuid, username, password) VALUES (@UserGUID, @Username, @Password);

	RETURN 0;
END