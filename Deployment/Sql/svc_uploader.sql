CREATE LOGIN svc_uploader 
	WITH PASSWORD = '<set_password>' 
GO

GRANT CONNECT TO svc_uploader 

USE Olimp2019
GO

CREATE USER svc_uploader
	FOR LOGIN svc_uploader
	WITH DEFAULT_SCHEMA = dbo
GO

-- Add user to the database owner role
EXEC sp_addrolemember N'db_owner', N'svc_uploader'
GO

USE master 

CREATE USER svc_uploader
	FOR LOGIN svc_uploader
	WITH DEFAULT_SCHEMA = dbo
GO

-- Add user to the database owner role
EXEC sp_addrolemember N'dbmanager', N'svc_uploader'
GO
