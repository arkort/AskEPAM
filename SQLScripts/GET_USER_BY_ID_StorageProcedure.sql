USE [AskEpamDB]
GO

/****** Object:  StoredProcedure [dbo].[GET_USER_BY_ID]    Script Date: 10/06/2014 18:13:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GET_USER_BY_ID] 
		@id_user INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT login,skill FROM [AskEpamDB].[dbo].[Users]
	WHERE id = @id_user
END

GO

