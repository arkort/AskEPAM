USE AskEpam
GO

DROP PROCEDURE dbo.UsersAdd
GO
CREATE PROCEDURE dbo.UsersAdd
	@DomainName NVARCHAR(100)
AS
BEGIN
	
	IF EXISTS(SELECT * FROM dbo.Users WHERE DomainName = @DomainName)
	BEGIN
		THROW 50001, 'User with the same name already exists.', 1;
	END

	INSERT INTO dbo.Users(DomainName) VALUES (@DomainName)


END
GO