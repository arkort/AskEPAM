USE master
GO

IF NOT EXISTS (select * from sys.databases where name = 'AskEpam')
	CREATE DATABASE [AskEpam]
GO
