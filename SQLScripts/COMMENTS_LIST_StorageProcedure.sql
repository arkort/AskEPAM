USE [AskEpamDB]
GO

/****** Object:  StoredProcedure [dbo].[COMMENTS_LIST]    Script Date: 10/09/2014 09:42:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[COMMENTS_LIST]
	 	 @idQuestion int
AS
BEGIN
	SELECT  c.text,c.dateTimeCreation,u.login  
	FROM  [AskEpamDB].[dbo].[Comments] c LEFT JOIN [AskEpamDB].[dbo].[Users] u 
	ON c.idUser =  u.id 
	WHERE idQuestion=@idQuestion
END

GO

