
-- Login store
CREATE PROC sp_Login
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
CREATE proc sp_FindAllAgent
as
begin
	select * 
	from Agent a
	order by a.AgentCode
	
end
go
CREATE  proc sp_GetAgent
@AgentCode VARCHAR(10)
as
begin
	select *
	from AGENT a
	where AgentCode = @AgentCode 
end
go
CREATE  proc sp_GetMerchant
@MerchantCode VARCHAR(10)
as
begin
	select *
	from Merchant a
	where MerchantCode = @MerchantCode 
end
go
CREATE  proc sp_GetMerchantByAgentCode
@AgentCode VARCHAR(10)
as
begin
	select *
	from Merchant a
	where a.AgentCode = @AgentCode 
	order by a.MerchantCode 
end
go

CREATE  proc sp_FindAllMerchant
as
begin
	select *
	from Merchant a
	order by a.MerchantCode
	
end
go
CREATE PROC sp_InactiveOrActive_Agent
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


CREATE PROC sp_InactiveOrActive_Merchant
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
go

Create proc sp_FindAllRetrival
as
begin
	select * 
	from RETRIVAL a
	order by a.RetrivalCode
	
end
go 

Create proc sp_GetRetrival
@RetrivalCode VARCHAR(10)
as
begin
	select *
	from RETRIVAL a
	where RetrivalCode = @RetrivalCode
end
go

-- PROC TÌM KIẾM TRONG RETRIVAL
DROP PROCEDURE [dbo].[sp_FindRetrivalElement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_FindRetrivalElement] @Element varchar(50)
AS
DECLARE @tmp_money MONEY

BEGIN
	IF (ISNUMERIC(@Element) = 1)
	BEGIN
		SET @tmp_money = CONVERT(MONEY, @ELEMENT)
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout 
		FROM RETRIVAL a
		WHERE a.Amout = @tmp_money OR a.AccountNumber = @Element
		ORDER BY a.RetrivalCode
	END
	ELSE
	IF (ISDATE(@Element) = 1)
	BEGIN
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout 
		FROM RETRIVAL a
		WHERE a.ReportDate = @Element
		   OR a.TransactionDate = @Element
		ORDER BY a.RetrivalCode
	END
	ELSE
	BEGIN
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout 
		FROM RETRIVAL a
		WHERE  a.RetrivalCode = @Element 
			   OR a.AccountNumber = @Element 
			   OR a.MerchantCode = @Element
			   OR a.TransactionCode = @Element
			   OR a.Amout = @tmp_money
		ORDER BY a.RetrivalCode
	END

END
GO

