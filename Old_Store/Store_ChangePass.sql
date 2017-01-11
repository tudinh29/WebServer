alter PROC [dbo].[sp_ChangePassword]
	@username VARCHAR(10),
		@newpassword VARCHAR(32),
	@currentPassword VARCHAR(32)
AS
BEGIN 
	BEGIN TRY
		IF (EXISTS(SELECT * FROM USER_INFORMATION WHERE Password = @currentPassword AND UserName = @username))
		BEGIN
			UPDATE USER_INFORMATION
			SET Password = @newpassword
			WHERE UserName = @username
			RETURN 1
		END
		ELSE
			RETURN 0
	END TRY
	BEGIN CATCH
		RAISERROR (N'System error',16,1)
		RETURN
	END CATCH
END