CREATE TABLE [dbo].[Activities]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	UserID NVARCHAR(128) NOT NULL FOREIGN KEY REFERENCES AspNetUsers(Id), 
	[Description] varchar (MAX),
	[Type] INT NOT NULL, 
	[Verified] bit NOT NULL,
	[TimeStamp] timestamp
)
