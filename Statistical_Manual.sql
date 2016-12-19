exec SP_FindMerchantSummaryElementByAgent_ForQuery 'MC57431970','AG39207356',1,10

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



exec SP_GetMerchantSummaryForAgent_Default_ForQuery 'AG00026090',1,10

create Procedure SP_GetCountMerchantSummaryForAgent_Default_ForQuery
 @AgentCode varchar(10)
As
Begin
SELECT Count(*)
  FROM MERCHANT_SUMMARY_DAILY
  WHERE  AgentCode = @AgentCode
End
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

exec SP_GetMerchantSummary_Default_ForQuery 1,10
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

create Procedure SP_GetCountMerchantSummary_Default_ForQuery
As
Begin
SELECT Count(*)
  FROM MERCHANT_SUMMARY_DAILY
end

