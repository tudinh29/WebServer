CREATE proc sp_CountFindMerchantByAgentCodeAndElement_ForQuery
	@Element varchar(50),
	@AgentCode varchar(50)
AS

BEGIN
	SELECT Count(*)
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
END
GO

exec sp_CountFindMerchantByAgentCodeAndElement_ForQuery 'Store','AG00026090',0,10

CREATE proc sp_FindMerchantByAgentCodeAndElement_ForQuery
	@Element varchar(50),
	@AgentCode varchar(50),
	@pageIndex int, 
	@pageSize int
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
	Offset (@pageIndex - 1)*@pageSize row 
	fetch next @pageSize row only 
END
GO



create  proc sp_CountMerchantByAgentCode_ForQuery
		@AgentCode VARCHAR(10)
as
begin
	select Count(*)
	from Merchant a
	where a.AgentCode = @AgentCode 
end
go


create  proc sp_GetMerchantByAgentCode_ForQuery
		@AgentCode VARCHAR(10),
		@pageIndex int, 
		@pageSize int
as
begin
	select [MerchantCode]
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
	from Merchant a
	where a.AgentCode = @AgentCode 
	order by a.MerchantCode 
	Offset(@pageIndex - 1)*@pageSize row 
	fetch next @pageSize row only 
end
go




CREATE PROC sp_CountMerchant
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT
END
GO

CREATE PROC sp_FindAllMerchant_ForQuery
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT
	order by MerchantCode asc
	Offset (@pageIndex - 1)*@pageSize row 
	fetch next @pageSize row only 
end
GO

exec sp_FindMerchantElement_ForQuery 'Store', 0, 10 
exec sp_FindMerchantElement 'MC00029533'
drop proc sp_FindMerchantElement_ForQuery

create proc [dbo].[sp_FindMerchantElement_ForQuery] @Element varchar(50), @pageIndex int, @pageSize int
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
	Offset(@pageIndex - 1)*@pageSize row 
	fetch next @pageSize row only 
END
GO

exec sp_CountMerchantElement_ForQuery 'Store'
create proc [dbo].[sp_CountMerchantElement_ForQuery] @Element varchar(50)
AS

BEGIN
	SELECT COUNT(*)
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
END
GO