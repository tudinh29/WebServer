
DROP PROCEDURE [dbo].[sp_FindAgentElement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----Ngay			NguoiChinhSua				Version-----
----26/11/2016		Nguyen Pham Hoang Diem		1.0
----26/11/2016		Nguyen Pham Hoang Diem		1.1

CREATE proc [dbo].[sp_FindAgentElement] @Element varchar(50)
AS

BEGIN
	SELECT [AgentCode]
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
			  ,[RegionName]
	FROM [dbo].[AGENT] a
	WHERE  a.AgentCode like '%'+@Element+'%' 
			OR a.AgentName like '%'+@Element+'%' 
			OR a.Owner like '%'+@Element+'%'
			OR a.AgentStatus like '%'+@Element+'%'
			OR a.Address1 like '%'+@Element+'%'
			OR a.Address2 like '%'+@Element+'%'
			OR a.Address3 like '%'+@Element+'%'
			OR a.CityCode like '%'+@Element+'%'
			OR a.Phone like '%'+@Element+'%'
			OR a.Fax like '%'+@Element+'%'
			OR a.Email like '%'+@Element+'%'
			OR a.Zip like '%'+@Element+'%'
			OR a.ApprovalDate like '%'+@Element+'%'
			OR a.CloseDate like '%'+@Element+'%'
			OR a.FirstActiveDate like '%'+@Element+'%'
			OR a.LastActiveDate like '%'+@Element+'%'
			OR a.CityName like '%'+@Element+'%'
			OR a.RegionCode like '%'+@Element+'%'
			OR a.RegionName like '%'+@Element+'%'
	ORDER BY a.AgentCode
END
GO


DROP PROCEDURE [dbo].[sp_FindMerchantElement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----Ngay			NguoiChinhSua				Version-----
----26/11/2016		Nguyen Pham Hoang Diem		1.0
----26/11/2016		Nguyen Pham Hoang Diem		1.1

CREATE proc [dbo].[sp_FindMerchantElement] @Element varchar(50)
AS

BEGIN
	SELECT [MerchantCode]
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
	WHERE  a.MerchantCode like '%'+@Element+'%' 
			OR a.MerchantName like '%'+@Element+'%' 
			OR a.Owner like '%'+@Element+'%'
			OR a.Status like '%'+@Element+'%'
			OR a.MerchantType like '%'+@Element+'%'
			OR a.Address1 like '%'+@Element+'%'
			OR a.Address2 like '%'+@Element+'%'
			OR a.Address3 like '%'+@Element+'%'
			OR a.CityCode like '%'+@Element+'%'
			OR a.Phone like '%'+@Element+'%'
			OR a.Fax like '%'+@Element+'%'
			OR a.Email like '%'+@Element+'%'
			OR a.Zip like '%'+@Element+'%'
			OR a.ApprovalDate like '%'+@Element+'%'
			OR a.CloseDate like '%'+@Element+'%'
			OR a.FirstActiveDate like '%'+@Element+'%'
			OR a.LastActiveDate like '%'+@Element+'%'
			OR a.BankCardDBA like '%'+@Element+'%'
			OR a.AgentCode like '%'+@Element+'%'
			OR a.BackEndProcessor like '%'+@Element+'%'
			OR a.CityName like '%'+@Element+'%'
			OR a.RegionCode like '%'+@Element+'%'
			OR a.RegionName like '%'+@Element+'%'
			OR a.Description like '%'+@Element+'%'
	ORDER BY a.AgentCode
END
GO

DROP PROCEDURE [dbo].[sp_FindMerchantByAgentCodeAndElement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----Ngay			NguoiChinhSua				Version-----
----26/11/2016		Nguyen Pham Hoang Diem		1.0
----26/11/2016		Nguyen Pham Hoang Diem		1.1

CREATE proc [dbo].[sp_FindMerchantByAgentCodeAndElement] 
	@Element varchar(50),
	@AgentCode varchar(50)
AS

BEGIN
	SELECT [MerchantCode]
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
	WHERE  a.AgentCode = @AgentCode
		AND( a.MerchantCode like '%'+@Element+'%'
		OR a.MerchantName like '%'+@Element+'%'
		OR a.Owner like '%'+@Element+'%'
		OR a.Status like '%'+@Element+'%'
		OR a.Address1 like '%'+@Element+'%'
		OR a.Address2 like '%'+@Element+'%'
		OR a.Address3 like '%'+@Element+'%'
		OR a.CityCode like '%'+@Element+'%'
		OR a.Phone like '%'+@Element+'%'
		OR a.Fax like '%'+@Element+'%'
		OR a.Email like '%'+@Element+'%'
		OR a.MerchantType like '%'+@Element+'%'
		OR a.Zip like '%'+@Element+'%'
		OR a.ApprovalDate like '%'+@Element+'%'
		OR a.CloseDate like '%'+@Element+'%'
		OR a.FirstActiveDate like '%'+@Element+'%'
		OR a.LastActiveDate like '%'+@Element+'%'
		OR a.BankCardDBA like '%'+@Element+'%'
		OR a.BackEndProcessor like '%'+@Element+'%'
		OR a.CityName like '%'+@Element+'%'
		OR a.RegionCode like '%'+@Element+'%'
		OR a.RegionName like '%'+@Element+'%'
		OR a.Description like '%'+@Element+'%')
	ORDER BY a.MerchantCode
END
GO
