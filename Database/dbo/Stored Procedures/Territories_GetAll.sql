-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Territories_GetAll] 
AS
BEGIN
	SELECT 
	territoryId,
	name,
	description,
	income
	FROM dbo.Territories;
END
