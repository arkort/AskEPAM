USE [AskEpamDB]
GO

/****** Object:  Table [dbo].[UserQuestions]    Script Date: 10/03/2014 17:46:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserQuestions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUser] [int] NULL,
	[Question] [text] NULL,
	[idSection] [int] NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

