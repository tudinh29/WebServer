
-- Login store
create PROC sp_Login
	@username VARCHAR(10),
	@password VARCHAR(32)
AS
BEGIN 
	BEGIN TRY
		SELECT UserName, UserType, Status FROM USER_INFORMATION U WHERE U.UserName = @username AND U.Password = @password
	END TRY
	BEGIN CATCH
		RAISERROR (N'LỖI HỆ THỐNG',16,1)
		RETURN
	END CATCH
END
go
-- find all Agent store
create proc sp_FindAllAgent
as
begin
	select * 
	from Agent a
	order by a.AgentCode
	
end
go
create  proc sp_GetAgent
@AgentCode VARCHAR(10)
as
begin
	select *
	from AGENT a
	where AgentCode = @AgentCode 
end
go
create  proc sp_GetMerchant
@MerchantCode VARCHAR(10)
as
begin
	select *
	from Merchant a
	where MerchantCode = @MerchantCode 
end
go
create  proc sp_GetMerchantByAgentCode
@AgentCode VARCHAR(10)
as
begin
	select *
	from Merchant a
	where a.AgentCode = @AgentCode 
	order by a.MerchantCode 
end
go
exec 
ALTER  proc sp_FindAllMerchant
as
begin
	select *
	from Merchant a
	order by a.MerchantCode
	
end
go
create PROC sp_InactiveOrActive_Agent
	@AgentCode VARCHAR(10)
AS
BEGIN
	BEGIN TRY
		DECLARE @currentDate DATE
		DECLARE @currentStatus char(1)
		SET @currentStatus = (Select AgentStatus from AGENT where AgentCode = @AgentCode)
		SET @currentDate = GETDATE()
		UPDATE AGENT 
		SET LastActiveDate =
                 CASE
                   WHEN AgentStatus = 'A' THEN LastActiveDate
                   WHEN AgentStatus = 'I' THEN @currentDate
				END,
			CloseDate =
                 CASE
                   WHEN AgentStatus = 'A' THEN @currentDate
                   WHEN AgentStatus = 'I' THEN CloseDate
				END,
			AgentStatus =
                 CASE
                   WHEN AgentStatus = 'A' THEN 'I'
                   WHEN AgentStatus = 'I' THEN 'A'
				END
		WHERE AgentCode = @AgentCode
	END TRY
	BEGIN CATCH
		RAISERROR (N'Lỗi hệ thống',16,1)
		RETURN
	END CATCH
END

go


alter PROC sp_InactiveOrActive_Merchant
	@MerchantCode VARCHAR(10)
AS
BEGIN
	BEGIN TRY
		DECLARE @currentDate DATE
		DECLARE @currentStatus char(1)
		SET @currentStatus = (Select Status from Merchant where MerchantCode = @MerchantCode)
		SET @currentDate = GETDATE()
		UPDATE Merchant 
		SET LastActiveDate =
                 CASE
                   WHEN Status = 'A' THEN LastActiveDate
                   WHEN Status = 'I' THEN @currentDate
				END,
			CloseDate =
                 CASE
                   WHEN Status = 'A' THEN @currentDate
                   WHEN Status = 'I' THEN CloseDate
				END,
			Status =
                 CASE
                   WHEN Status = 'A' THEN 'I'
                   WHEN Status = 'I' THEN 'A'
				END
		WHERE MerchantCode = @MerchantCode
	END TRY
	BEGIN CATCH
		RAISERROR (N'Lỗi hệ thống',16,1)
		RETURN
	END CATCH
END
