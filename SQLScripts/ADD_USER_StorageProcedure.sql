USE [AskEpamDB]
GO

/****** Object:  StoredProcedure [dbo].[ADD_USER]    Script Date: 10/03/2014 17:35:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ADD_USER] 
	-- Add the parameters for the stored procedure here
	@Login nvarchar(50), 
    @Pwd nvarchar(50) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [AskEpamDB].[dbo].[Users]
           (
           [login]
           ,[password])
     OUTPUT INSERTED.id
     VALUES
           (@Login,
           CONVERT(NVARCHAR(32),HASHBYTES('MD5',@Pwd),2))
END

GO

