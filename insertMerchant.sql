USE [SERVER]
GO
/****** Object:  UserDefinedFunction [dbo].[GetMerchantCode]    Script Date: 11/12/2016 8:03:07 AM ******/
DROP FUNCTION [dbo].[GetMerchantCode]
GO

/****** Object:  UserDefinedFunction [dbo].[GetMerchantCode]    Script Date: 11/12/2016 8:03:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetMerchantCode]()
RETURNS VARCHAR(10)
AS
BEGIN
	DECLARE @MerchantCode VARCHAR(10)
	SET @MerchantCode = (SELECT Top 1 MerchantCode FROM MERCHANT ORDER BY MerchantCode desc)
	SET @MerchantCode = RIGHT(@MerchantCode, 8)
	SET @MerchantCode = CAST (@MerchantCode AS INT) + 1 
	SET @MerchantCode = CAST (@MerchantCode AS VARCHAR)
	SET @MerchantCode = CONCAT('MC',@MerchantCode)
	RETURN @MerchantCode 

END
GO




/****** Object:  StoredProcedure [dbo].[sp_AddNewMerchant]    Script Date: 11/12/2016 7:57:01 AM ******/
DROP PROCEDURE [dbo].[sp_AddNewMerchant]
GO

/****** Object:  StoredProcedure [dbo].[sp_AddNewMerchant]    Script Date: 11/12/2016 7:57:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_AddNewMerchant]
			
		    @MerchantName nvarchar(50)
           ,@BackEndProcessor int
           ,@Status char(1)
           ,@Owner nvarchar(50)
           ,@MerchantType varchar(10)
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
           ,@BankCardDBA varchar(50)
           ,@FirstActiveDate date
           ,@LastActiveDate date
           ,@AgentCode varchar(10)
AS
BEGIN TRY
	DECLARE @MerchantCode	VARCHAR(10)
	SET @MerchantCode = dbo.GetMerchantCode()
	IF (EXISTS (SELECT MERCHANTCODE FROM MERCHANT WHERE MerchantCode = @MerchantCode))
	BEGIN
		RAISERROR ('Error! Merchant code has existed.',16, 1)
		RETURN
	END
	
	INSERT INTO MERCHANT (
			[MerchantCode]
           ,[MerchantName]
           ,[BackEndProcessor]
           ,[Status]
           ,[Owner]
           ,[MerchantType]
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
           ,[BankCardDBA]
           ,[FirstActiveDate]
           ,[LastActiveDate]
           ,[AgentCode])
	VALUES (
			@MerchantCode
           ,@MerchantName
           ,@BackEndProcessor
           ,@Status
           ,@Owner
           ,@MerchantType
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
           ,@BankCardDBA
           ,@FirstActiveDate
           ,@LastActiveDate
           ,@AgentCode
		   )
END TRY
BEGIN CATCH
	
		RAISERROR (N'Lỗi hệ thống',16,1)
		RETURN
	
END CATCH

GO
DROP PROCEDURE [dbo].[sp_SelectAllCity]
GO

/****** Object:  StoredProcedure [dbo].[sp_SelectAllCity]    Script Date: 11/12/2016 7:58:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_SelectAllCity]
AS
BEGIN TRY
	SELECT *
	FROM CITY
	ORDER BY CityName;
END TRY
BEGIN CATCH 
	RAISERROR (N'Lỗi hệ thống',16,1)
	RETURN
END CATCH
GO

DROP PROCEDURE [dbo].[sp_SelectAllMerchantType]
GO

/****** Object:  StoredProcedure [dbo].[sp_SelectAllMerchantType]    Script Date: 11/12/2016 7:59:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_SelectAllMerchantType]
AS
BEGIN TRY
	SELECT MerchantType, Description
	FROM MERCHANT_TYPE
	ORDER BY MerchantType
END TRY
BEGIN CATCH 
	RAISERROR (N'Lỗi hệ thống',16,1)
	RETURN
END CATCH

GO
DROP PROCEDURE [dbo].[sp_SelectAllProcessor]
GO

/****** Object:  StoredProcedure [dbo].[sp_SelectAllProcessor]    Script Date: 11/12/2016 8:00:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_SelectAllProcessor]
AS
BEGIN TRY
	SELECT [ID]
      ,[ProcessorCode]
      ,[ProcessorName]
      ,[Status]
      ,[ApprovalDate]
      ,[CloseDate]
      ,[FirstActiveDate]
      ,[LastActiveDate]
	FROM PROCESSOR
	ORDER BY ProcessorName
END TRY
BEGIN CATCH
	RAISERROR (N'Lỗi hệ thống',16,1)
	RETURN
END CATCH

--------------------------
go
CREATE PROC [dbo].[Sp_Editmerchant](
			@MerchantCode varchar(10),
			@MerchantName nvarchar(50)  ,
			@BackEndProcessor int ,
			@Status char(1) ,
			@Owner nvarchar(50) ,
			@MerchantType varchar(10) ,
			@Address1 nvarchar(50) ,
			@Address2 nvarchar(50) ,
			@Address3 nvarchar(50) ,
			@CityCode varchar(10) ,
			@Zip int ,
			@Phone varchar(20) ,
			@Fax varchar(20) ,
			@Email varchar(30) ,
			@ApprovalDate date ,
			@CloseDate date ,
			@BankCardDBA varchar(50) ,
			@FirstActiveDate date ,
			@LastActiveDate date ,
			@AgentCode varchar(10)
)
AS
BEGIN TRY
			UPDATE DBO.MERCHANT
			SET
				MerchantName = @MerchantName,
				BackEndProcessor = @BackEndProcessor,
				Status = @Status,
				Owner = @Owner,
				MerchantType = @MerchantType,
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
				BankCardDBA = @BankCardDBA,
				FirstActiveDate = @FirstActiveDate,
				LastActiveDate = @LastActiveDate,
				AgentCode = @AgentCode
			WHERE MerchantCode = @MerchantCode
END TRY
BEGIN CATCH
	
		RAISERROR (N'Lỗi hệ thống',16,1)
		RETURN
	
END CATCH


