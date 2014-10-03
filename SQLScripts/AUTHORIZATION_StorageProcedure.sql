USE [AskEpamDB]
GO

/****** Object:  StoredProcedure [dbo].[AUTHORIZATION]    Script Date: 10/03/2014 17:48:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AUTHORIZATION]
	-- Add the parameters for the stored procedure here
		@Login nvarchar(50), 
    @Pwd nvarchar(50) 
AS
BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
		SELECT id,login,skill   
  FROM [AskEpamDB].[dbo].[Users] WHERE [AskEpamDB].[dbo].[Users].login  = @Login
  AND [AskEpamDB].[dbo].[Users].password  = CONVERT(NVARCHAR(32),HASHBYTES('MD5',@Pwd),2); 
END

GO

