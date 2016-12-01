----Ngay			NguoiChinhSua				Version-----
----01/12/2016		Nguyen Pham Hoang Diem		1.0

DROP PROCEDURE [dbo].[sp_FindMerchantAvailable]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_FindMerchantAvailable] 
	@AgentCode varchar(50),
	@RegionCode varchar(50)
AS

BEGIN
	SELECT top 100 [MerchantCode]
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
			  ,[AgentCode]
			  ,[CityName]
			  ,[RegionCode]
			  ,[RegionName]
			  ,[Description]
	FROM [dbo].[MERCHANT] a
	WHERE  a.AgentCode != @AgentCode
		AND a.RegionCode = @RegionCode
	ORDER BY a.MerchantCode
END
GO

----Ngay			NguoiChinhSua				Version-----
----01/12/2016		Nguyen Pham Hoang Diem		1.0

DROP PROCEDURE [dbo].[sp_UpdateAgentOfMerchant]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_UpdateAgentOfMerchant] 
	@MerchantCode varchar(50),
	@AgentCode varchar(50)
AS

BEGIN
	UPDATE MERCHANT SET AgentCode = @AgentCode WHERE MerchantCode = @MerchantCode
END
GO