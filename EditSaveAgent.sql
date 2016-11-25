﻿USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[Sp_EditAgent]    Script Date: 21/11/2016 1:15:43 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Sp_EditAgent](
			@AgentCode varchar(10),
			@AgentName nvarchar(50),
			@Status char(1) ,
			@Owner nvarchar(50) ,
			@Address1 nvarchar(50) ,
			@Address2 nvarchar(50) ,
			@Address3 nvarchar(50) ,
			@CityCode varchar(10) ,
			@Zip int ,
			@Phone varchar(20) ,
			@Fax varchar(20) ,
			@Email varchar(30) ,
			@ApprovalDate date ,
			@CloseDate date,
			@FirstActiveDate date ,
			@LastActiveDate date
)
AS
BEGIN TRY
			declare @regionCode varchar(10)
			set @regionCode = (Select RegionCode from CITY where CityCode =  @CityCode)
			declare @CityName nvarchar(50)
			set @CityName = (Select CityName from CITY where CityCode =  @CityCode)
			declare @regionName nvarchar(50)
			set @regionName = (Select RegionName from REGION where RegionCode =  @regionCode)
			UPDATE DBO.AGENT
			SET
				AgentName = @AgentName,
				AgentStatus = @Status,
				Owner = @Owner,				
				Address1 = @Address1,
				Address2 = @Address2,
				Address3 = @Address3,
				CityCode = @CityCode,
				Zip = @Zip,
				Phone = @Phone,
				Fax = @Fax,
				Email = @Email,
				ApprovalDate = @ApprovalDate,
				CloseDate = @CloseDate,
				FirstActiveDate = @FirstActiveDate,
				LastActiveDate = @LastActiveDate,
				AgentCode = @AgentCode,
				CityName = @CityName,
				RegionCode = @regionCode,
				RegionName = @regionName
			WHERE AgentCode = @AgentCode
END TRY
BEGIN CATCH
	
		RAISERROR (N'Lỗi hệ thống',16,1)
		RETURN
	
END CATCH
go
----------------------------------------
CREATE FUNCTION [dbo].[GetAgentCode]()
RETURNS VARCHAR(10)
AS
BEGIN
	DECLARE @AgentCode VARCHAR(10)
	SET @AgentCode = (SELECT Top 1 AgentCode FROM AGENT ORDER BY AgentCode desc)
	SET @AgentCode = RIGHT(@AgentCode, 8)
	SET @AgentCode = CAST (@AgentCode AS INT) + 1 
	SET @AgentCode = CAST (@AgentCode AS VARCHAR)
	SET @AgentCode = CONCAT('AG',@AgentCode)
	RETURN @AgentCode 
	
END
--------------------------------
go
create PROC [dbo].[sp_AddNewAgent]
			
		    @AgentName nvarchar(50)
           ,@Status char(1)
           ,@Owner nvarchar(50)
           ,@Address1 nvarchar(50)
           ,@Address2 nvarchar(50)
           ,@Address3 nvarchar(50)
           ,@CityCode varchar(10)
           ,@Zip int
           ,@Phone varchar(20)
           ,@Fax varchar(20)
           ,@Email varchar(30)
           ,@ApprovalDate date
           ,@CloseDate date
           ,@FirstActiveDate date
           ,@LastActiveDate date
AS
BEGIN TRY
	DECLARE @AgentCode	VARCHAR(10)
	SET @AgentCode = dbo.GetAgentCode()
	IF (EXISTS (SELECT AGENTCODE FROM AGENT WHERE AgentCode = @AgentCode))
	BEGIN
		RAISERROR ('Error! Merchant code has existed.',16, 1)
		RETURN
	END
	declare @regionCode varchar(10)
	set @regionCode = (Select RegionCode from CITY where CityCode =  @CityCode)
	declare @CityName nvarchar(50)
	set @CityName = (Select CityName from CITY where CityCode =  @CityCode)
	declare @regionName nvarchar(50)
	set @regionName = (Select RegionName from REGION where RegionCode =  @regionCode)
	INSERT INTO AGENT (
			[AgentCode]
           ,[AgentName]
           ,[AgentStatus]
           ,[Owner]
           ,[Address1]
           ,[Address2]
           ,[Address3]
           ,[CityCode]
           ,[Zip]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[ApprovalDate]
           ,[CloseDate]
           ,[FirstActiveDate]
           ,[LastActiveDate]
		   ,[CityName]
		  ,[RegionCode]
		  ,[RegionName])
	VALUES (
			@AgentCode
           ,@AgentName
           ,@Status
           ,@Owner
           ,@Address1
           ,@Address2
           ,@Address3
           ,@CityCode
           ,@Zip
           ,@Phone
           ,@Fax
           ,@Email
           ,@ApprovalDate
           ,@CloseDate
           ,@FirstActiveDate
           ,@LastActiveDate
		   ,@CityName
		   ,@regionCode
		   ,@regionName
		   )

		insert into USER_INFORMATION (UserName,Password,UserType,Status)
	  SELECT AgentCode,'202cb962ac59075b964b07152d234b70','A','I'
	  from AGENT
	  where AgentCode = @AgentCode
END TRY
BEGIN CATCH
	
		RAISERROR (N'Lỗi hệ thống',16,1)
		RETURN
	
END CATCH
--------------------------------------------------------

------------------------------------------------------------