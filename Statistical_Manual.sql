go 
drop Procedure SP_FindCountMerchantSummaryElementByAgent_ForQuery 
go

create Procedure SP_FindCountMerchantSummaryElementByAgent_ForQuery 
	@Element varchar(50),
	@AgentCode varchar(10)
As
Begin
SELECT Count(*)
  FROM [SERVER].[dbo].[MERCHANT_SUMMARY_DAILY]
  WHERE [AgentCode] = @AgentCode and ([MerchantCode] like '%'+@Element+'%' OR
		[ReportDate] like '%'+@Element+'%' OR
		  [SaleAmount] like '%'+@Element+'%' OR
		  [SaleCount] like '%'+@Element+'%' OR
		  [ReturnAmount] like '%'+@Element+'%' OR
		  [ReturnCount] like '%'+@Element+'%' OR
		  [NetAmount] like '%'+@Element+'%' OR
		  [TransactionCount] like '%'+@Element+'%' OR
		  [KeyedAmount] like '%'+@Element+'%' )
end
go 
drop Procedure SP_FindMerchantSummaryElementByAgent_ForQuery 
go

create Procedure SP_FindMerchantSummaryElementByAgent_ForQuery 
	@Element varchar(50),
	@AgentCode varchar(10),
	@pageIndex int,
	@pageSize int
As
Begin
SELECT *
  FROM [SERVER].[dbo].[MERCHANT_SUMMARY_DAILY]
  WHERE [AgentCode] = @AgentCode and ([MerchantCode] like '%'+@Element+'%' OR
		[ReportDate] like '%'+@Element+'%' OR
		  [SaleAmount] like '%'+@Element+'%' OR
		  [SaleCount] like '%'+@Element+'%' OR
		  [ReturnAmount] like '%'+@Element+'%' OR
		  [ReturnCount] like '%'+@Element+'%' OR
		  [NetAmount] like '%'+@Element+'%' OR
		  [TransactionCount] like '%'+@Element+'%' OR
		  [KeyedAmount] like '%'+@Element+'%' )
  ORDER BY ReportDate desc
  Offset (@pageIndex - 1)*@pageSize row 
  fetch next @pageSize row only 
end



go drop Procedure SP_GetCountMerchantSummaryForAgent_Default_ForQuery 
go

create Procedure SP_GetCountMerchantSummaryForAgent_Default_ForQuery
 @AgentCode varchar(10)
As
Begin
SELECT Count(*)
  FROM MERCHANT_SUMMARY_DAILY
  WHERE  AgentCode = @AgentCode
End
go

go drop Procedure SP_GetMerchantSummaryForAgent_Default_ForQuery 
go
create Procedure SP_GetMerchantSummaryForAgent_Default_ForQuery
 @AgentCode varchar(10),
 @pageIndex int,
 @pageSize int
As
Begin
SELECT *
  FROM MERCHANT_SUMMARY_DAILY
  WHERE  AgentCode = @AgentCode
  ORDER BY ReportDate desc
  Offset (@pageIndex - 1)*@pageSize row 
  fetch next @pageSize row only 
End
go


go drop Procedure SP_FindCountMerchantSummaryElement_ForQuery 
go


create Procedure SP_FindCountMerchantSummaryElement_ForQuery
	@Element varchar(50)	
As
Begin
SELECT Count(*)
  FROM [SERVER].[dbo].[MERCHANT_SUMMARY_DAILY]
  WHERE [MerchantCode] like '%'+@Element+'%' OR
		[ReportDate] like '%'+@Element+'%' OR
		  [SaleAmount] like '%'+@Element+'%' OR
		  [SaleCount] like '%'+@Element+'%' OR
		  [ReturnAmount] like '%'+@Element+'%' OR
		  [ReturnCount] like '%'+@Element+'%' OR
		  [NetAmount] like '%'+@Element+'%' OR
		  [TransactionCount] like '%'+@Element+'%' OR
		  [KeyedAmount] like '%'+@Element+'%' 
end
go
go drop Procedure SP_FindMerchantSummaryElement_ForQuery 
go
Create Procedure SP_FindMerchantSummaryElement_ForQuery
	@Element varchar(50), 	
	@pageIndex int,
	@pageSize int
As
Begin
SELECT *
  FROM [SERVER].[dbo].[MERCHANT_SUMMARY_DAILY]
  WHERE [MerchantCode] like '%'+@Element+'%' OR
		[ReportDate] like '%'+@Element+'%' OR
		  [SaleAmount] like '%'+@Element+'%' OR
		  [SaleCount] like '%'+@Element+'%' OR
		  [ReturnAmount] like '%'+@Element+'%' OR
		  [ReturnCount] like '%'+@Element+'%' OR
		  [NetAmount] like '%'+@Element+'%' OR
		  [TransactionCount] like '%'+@Element+'%' OR
		  [KeyedAmount] like '%'+@Element+'%' 
  ORDER BY ReportDate desc
  Offset (@pageIndex - 1)*@pageSize row 
  fetch next @pageSize row only 
end
go
go drop Procedure SP_GetMerchantSummary_Default_ForQuery 
go
Create Procedure SP_GetMerchantSummary_Default_ForQuery
	@pageIndex int, 
	@pageSize int
As
Begin
SELECT*
  FROM MERCHANT_SUMMARY_DAILY
  ORDER BY ReportDate desc
  Offset (@pageIndex - 1)*@pageSize row 
  fetch next @pageSize row only 
end
go drop Procedure SP_GetCountMerchantSummary_Default_ForQuery 
go
create Procedure SP_GetCountMerchantSummary_Default_ForQuery
As
Begin
SELECT Count(*)
  FROM MERCHANT_SUMMARY_DAILY
end

