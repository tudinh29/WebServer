
DROP PROCEDURE [dbo].[sp_FindAgentElement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_FindAgentElement] @Element varchar(50)
AS

BEGIN
	IF (ISNUMERIC(@Element) = 1)
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
		FROM [dbo].[AGENT] a 
		
		WHERE a.Zip = @Element
	END
	ELSE
		IF (ISDATE(@Element) = 1)
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
			  FROM [dbo].[AGENT] a
			WHERE a.FirstActiveDate = @Element
			   OR a.LastActiveDate = @Element
			   OR a.ApprovalDate = @Element
			   OR a.CloseDate = @Element
			ORDER BY a.AgentCode
		END
		ELSE
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
			  FROM [dbo].[AGENT] a
			WHERE  a.AgentCode = @Element 
				   OR a.AgentName = @Element 
				   OR a.Owner = @Element
				   OR a.AgentStatus = @Element
				   OR a.Address1 = @Element
				   OR a.Address2 = @Element
				   OR a.Address3 = @Element
				   OR a.CityCode = @Element
				   OR a.Phone = @Element
				   OR a.Fax = @Element
				   OR a.Email = @Element
			ORDER BY a.AgentCode
		END
END
GO


DROP PROCEDURE [dbo].[sp_FindMerchantElement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_FindMerchantElement] @Element varchar(50)
AS

BEGIN
	IF (ISNUMERIC(@Element) = 1)
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
		  FROM [dbo].[MERCHANT] a
		
		WHERE a.Zip = @Element
			OR a.BackEndProcessor = @Element
		ORDER BY a.MerchantCode
	END
	ELSE
		IF (ISDATE(@Element) = 1)
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
			  FROM [dbo].[MERCHANT] a
			WHERE a.FirstActiveDate = @Element
			   OR a.LastActiveDate = @Element
			   OR a.ApprovalDate = @Element
			   OR a.CloseDate = @Element
			ORDER BY a.MerchantCode
		END
		ELSE
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
			  FROM [dbo].[MERCHANT] a
			WHERE  a.MerchantCode = @Element 
				   OR a.MerchantName = @Element 
				   OR a.Owner = @Element
				   OR a.Status = @Element
				   OR a.Address1 = @Element
				   OR a.Address2 = @Element
				   OR a.Address3 = @Element
				   OR a.CityCode = @Element
				   OR a.Phone = @Element
				   OR a.Fax = @Element
				   OR a.Email = @Element
				   OR a.MerchantType = @Element
			ORDER BY a.MerchantCode
		END
END
GO

DROP PROCEDURE [dbo].[sp_FindMerchantByAgentCodeAndElement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_FindMerchantByAgentCodeAndElement] 
	@Element varchar(50),
	@AgentCode varchar(50)
AS

BEGIN
	IF (ISNUMERIC(@Element) = 1)
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
		  FROM [dbo].[MERCHANT] a
		
		WHERE a.AgentCode = @AgentCode
			AND ( a.Zip = @Element
			OR a.BackEndProcessor = @Element )
		ORDER BY a.MerchantCode
	END
	ELSE
		IF (ISDATE(@Element) = 1)
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
			  FROM [dbo].[MERCHANT] a
			WHERE a.AgentCode = @AgentCode
				AND ( a.FirstActiveDate = @Element
			   OR a.LastActiveDate = @Element
			   OR a.ApprovalDate = @Element
			   OR a.CloseDate = @Element )
			ORDER BY a.MerchantCode
		END
		ELSE
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
			  FROM [dbo].[MERCHANT] a
			WHERE  a.AgentCode = @AgentCode
					AND( a.MerchantCode = @Element 
				   OR a.MerchantName = @Element 
				   OR a.Owner = @Element
				   OR a.Status = @Element
				   OR a.Address1 = @Element
				   OR a.Address2 = @Element
				   OR a.Address3 = @Element
				   OR a.CityCode = @Element
				   OR a.Phone = @Element
				   OR a.Fax = @Element
				   OR a.Email = @Element
				   OR a.MerchantType = @Element)
			ORDER BY a.MerchantCode
		END
END
GO