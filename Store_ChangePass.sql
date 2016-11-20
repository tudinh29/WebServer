CREATE PROC [dbo].[sp_ChangePassword]
	@username VARCHAR(10),
	@newpassword VARCHAR(32)
AS
BEGIN 
	BEGIN TRY
		UPDATE USER_INFORMATION
		SET Password = @newpassword
		WHERE UserName = @username
	END TRY
	BEGIN CATCH
		RAISERROR (N'L?I H? TH?NG',16,1)
		RETURN
	END CATCH
END