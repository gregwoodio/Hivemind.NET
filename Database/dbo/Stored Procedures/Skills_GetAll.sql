-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Skills_GetAll] 
	
AS
BEGIN
	
	SELECT s.category, s.description, s.skillId, s.skillName
	FROM dbo.Skills s;

END