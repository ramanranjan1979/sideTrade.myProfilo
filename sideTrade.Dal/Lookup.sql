select * from dbo.Profile
select * from dbo.Login
select * from dbo.ProfileType
select * from dbo.ProfileMapping

--INSERT INTO dbo.ProfileType values('Administrator',getdate(),null,1)
--INSERT INTO dbo.ProfileType values('User',getdate(),null,1)
--INSERT INTO dbo.Profile values('Raman','Ranjan','info@modizson.com',1,1,getdate(),null)
--INSERT INTO dbo.ProfileMapping values(1,1,GETDATE(),1)
--INSERT INTO dbo.Login values('F04949AF70B9EC13069FD8E32490445C8F17C2ED',getdate(),null,0,1,1)

select * from dbo.FileManagerType
--insert into dbo.FileManagertype values('.cs','3.0',1,getdate(),null)

select * from dbo.logtype
--insert into dbo.logtype values('APP_EXCEPTION')
--insert into dbo.logtype values('APP_INFORMATION')
--insert into dbo.logtype values('APP_DEBUGG')
--insert into dbo.logtype values('APP_USERTRACE')

select * from dbo.NotificationType

--insert into dbo.NotificationType values('INVITE','tmpjoining.html')
--insert into dbo.NotificationType values('RESETPASSWORD','tmpForgotPassword.html')
--insert into dbo.NotificationType values('FILEUPLOAD','tmpFileUpload.html')
--insert into dbo.NotificationType values('UPLOADREJECT','tmpUploadReject.html')


select * from dbo.Settings
--insert into dbo.Settings values('SMTP_HOST','auth.smtp.1and1.co.uk')
--insert into dbo.Settings values('SMTP_FROM','info@modizson.com')
--insert into dbo.Settings values('SYS_ADMIN_EMAIL','info@modizson.com')
--insert into dbo.Settings values('SMTP_USERNAME','m83510685-118013067')
--insert into dbo.Settings values('SMTP_PASSWORD','***************')