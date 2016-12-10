-- Update 08/12/2016 --

Create Procedure SP_GetReportData_Monthly (@startMonth int, @startYear int, 
	@endMonth int, @endYear int) 
As
Begin
	declare @reportDate varchar(20)
	declare @currentDate int
	declare @firstDate int
	set @currentDate = @endMonth + (@endYear * 12)
	set @firstDate =  @startMonth + (@startYear * 12)

	set @reportDate = CONCAT(@startMonth, '/', @startYear, ' - ', @endMonth, '/', @endYear)
	
		SELECT @reportDate as ReportDate, 'A' as MerchantCode,
		 SUM(SaleAmount) AS SaleAmount, CAST(SUM( SaleCount) AS BIGINT) AS  SaleCount, SUM( ReturnAmount) AS  ReturnAmount, CAST(SUM( ReturnCount) AS BIGINT) AS  ReturnCount, 
		 SUM( NetAmount) AS  NetAmount, CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount, SUM( KeyedAmount) AS  KeyedAmount, CAST(SUM( KeyedCount) AS BIGINT) AS  KeyedCount, 
		 SUM( KeyedReturnAmount) AS  KeyedReturnAmount, CAST(SUM( KeyedReturnCount) AS BIGINT) AS  KeyedReturnCount, SUM( KeyedNetAmount) AS  KeyedNetAmount, 
		 CAST(SUM( KeyedTransactionCount) AS BIGINT) AS  KeyedTransactionCount, SUM( ForeignCardAmount) AS  ForeignCardAmount, CAST(SUM( ForeignCardCount) AS BIGINT) AS  ForeignCardCount,
		 SUM( ForeignCardReturnAmount) AS  ForeignCardReturnAmount, CAST(SUM( ForeignCardReturnCount) AS BIGINT) AS  ForeignCardReturnCount, SUM( ForeignCardNetAmount) AS  ForeignCardNetAmount, 
		 CAST(SUM( ForeignCardTransactionCount) AS BIGINT) AS  ForeignCardTransactionCount, SUM( DebitCardAmount) AS  DebitCardAmount, CAST(SUM(DebitCardCount) AS BIGINT) AS  DebitCardCount, 
		 SUM( DebitCardReturnAmount) AS  DebitCardReturnAmount, CAST(SUM( DebitCardReturnCount) AS BIGINT) AS  DebitCardReturnCount, SUM( DebitCardNetAmount) AS  DebitCardNetAmount, 
		 CAST(SUM(DebitCardTransactionCount) AS BIGINT) AS  DebitCardTransactionCount, SUM( VisaCardAmount) AS  VisaCardAmount, CAST(SUM( VisaCardCount) AS BIGINT) AS  VisaCardCount, 
		 SUM( VisaCardReturnAmount) AS  VisaCardReturnAmount, CAST(SUM( VisaCardReturnCount) AS BIGINT) AS  VisaCardReturnCount, SUM( VisaCardNetAmount) AS  VisaCardNetAmount, 
		 CAST(SUM( VisaCardTransactionCount) AS BIGINT) AS  VisaCardTransactionCount, SUM( DiscoverCardAmount) AS  DiscoverCardAmount, CAST(SUM( DiscoverCardCount) AS BIGINT) AS  DiscoverCardCount,
		 SUM( DiscoverCardReturnAmount) AS  DiscoverCardReturnAmount, CAST(SUM( DiscoverCardReturnCount) AS BIGINT) AS  DiscoverCardReturnCount, SUM( DiscoverCardNetAmount) AS  DiscoverCardNetAmount, 
		 CAST(SUM( DiscoverCardTransactionCount) AS BIGINT) AS  DiscoverCardTransactionCount, SUM( MASterCardAmount) AS  MASterCardAmount, CAST(SUM( MASterCardCount) AS BIGINT) AS  MASterCardCount, 
		 SUM( MASterCardReturnAmount) AS  MASterCardReturnAmount, CAST(SUM( MASterCardReturnCount) AS BIGINT) AS  MASterCardReturnCount, SUM( MASterCardNetAmount) AS  MASterCardNetAmount,
		 CAST(SUM( MASterCardTransactionCount) AS BIGINT) AS  MASterCardTransactionCount, SUM( AmericanExpressAmount) AS  AmericanExpressAmount, CAST(SUM( AmericanExpressCount) AS BIGINT) AS  AmericanExpressCount,
		 SUM( AmericanExpressReturnAmount) AS  AmericanExpressReturnAmount, CAST(SUM( AmericanExpressReturnCount) AS BIGINT) AS  AmericanExpressReturnCount, SUM( AmericanExpressNetAmount) AS  AmericanExpressNetAmount, 
		 CAST(SUM( AmericanExpressTransactionCount) AS BIGINT) AS  AmericanExpressTransactionCount, SUM( OtherCardAmount) AS  OtherCardAmount, CAST(SUM( OtherCardCount) AS BIGINT) AS  OtherCardCount, 
		 SUM( OtherCardReturnAmount) AS  OtherCardReturnAmount, CAST(SUM( OtherCardReturnCount) AS BIGINT) AS  OtherCardReturnCount, SUM( OtherCardNetAmount) AS  OtherCardNetAmount,
		 CAST(SUM( OtherCardTransactionCount) AS BIGINT) AS  OtherCardTransactionCount, RegionCode, MerchantType, 'A' as AgentCode
		FROM MERCHANT_SUMMARY_MONTHLY 
		where (ReportMonth + (ReportYear * 12)) <= @currentDate
			and (ReportMonth + (ReportYear * 12)) >= @firstDate
		group by  RegionCode,MerchantType
End

-------------------------------
Create Procedure SP_GetReportData_Quarterly (
	@startQuarter int, @startYear int, 
	@endQuarter int, @endYear int) 
As
Begin
	declare @reportDate varchar(20)
	declare @currentDate int
	declare @firstDate int
	set @currentDate = @endQuarter + (@startYear * 4)
	set @firstDate =  @startQuarter + (@endYear * 4)

	set @reportDate = CONCAT(@startQuarter, '/', @startYear, ' - ', @endQuarter, '/', @endYear)
	
		SELECT @reportDate as ReportDate, 'A' as MerchantCode,
		 SUM(SaleAmount) AS SaleAmount, CAST(SUM( SaleCount) AS BIGINT) AS  SaleCount, SUM( ReturnAmount) AS  ReturnAmount, CAST(SUM( ReturnCount) AS BIGINT) AS  ReturnCount, 
		 SUM( NetAmount) AS  NetAmount, CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount, SUM( KeyedAmount) AS  KeyedAmount, CAST(SUM( KeyedCount) AS BIGINT) AS  KeyedCount, 
		 SUM( KeyedReturnAmount) AS  KeyedReturnAmount, CAST(SUM( KeyedReturnCount) AS BIGINT) AS  KeyedReturnCount, SUM( KeyedNetAmount) AS  KeyedNetAmount, 
		 CAST(SUM( KeyedTransactionCount) AS BIGINT) AS  KeyedTransactionCount, SUM( ForeignCardAmount) AS  ForeignCardAmount, CAST(SUM( ForeignCardCount) AS BIGINT) AS  ForeignCardCount,
		 SUM( ForeignCardReturnAmount) AS  ForeignCardReturnAmount, CAST(SUM( ForeignCardReturnCount) AS BIGINT) AS  ForeignCardReturnCount, SUM( ForeignCardNetAmount) AS  ForeignCardNetAmount, 
		 CAST(SUM( ForeignCardTransactionCount) AS BIGINT) AS  ForeignCardTransactionCount, SUM( DebitCardAmount) AS  DebitCardAmount, CAST(SUM(DebitCardCount) AS BIGINT) AS  DebitCardCount, 
		 SUM( DebitCardReturnAmount) AS  DebitCardReturnAmount, CAST(SUM( DebitCardReturnCount) AS BIGINT) AS  DebitCardReturnCount, SUM( DebitCardNetAmount) AS  DebitCardNetAmount, 
		 CAST(SUM(DebitCardTransactionCount) AS BIGINT) AS  DebitCardTransactionCount, SUM( VisaCardAmount) AS  VisaCardAmount, CAST(SUM( VisaCardCount) AS BIGINT) AS  VisaCardCount, 
		 SUM( VisaCardReturnAmount) AS  VisaCardReturnAmount, CAST(SUM( VisaCardReturnCount) AS BIGINT) AS  VisaCardReturnCount, SUM( VisaCardNetAmount) AS  VisaCardNetAmount, 
		 CAST(SUM( VisaCardTransactionCount) AS BIGINT) AS  VisaCardTransactionCount, SUM( DiscoverCardAmount) AS  DiscoverCardAmount, CAST(SUM( DiscoverCardCount) AS BIGINT) AS  DiscoverCardCount,
		 SUM( DiscoverCardReturnAmount) AS  DiscoverCardReturnAmount, CAST(SUM( DiscoverCardReturnCount) AS BIGINT) AS  DiscoverCardReturnCount, SUM( DiscoverCardNetAmount) AS  DiscoverCardNetAmount, 
		 CAST(SUM( DiscoverCardTransactionCount) AS BIGINT) AS  DiscoverCardTransactionCount, SUM( MASterCardAmount) AS  MASterCardAmount, CAST(SUM( MASterCardCount) AS BIGINT) AS  MASterCardCount, 
		 SUM( MASterCardReturnAmount) AS  MASterCardReturnAmount, CAST(SUM( MASterCardReturnCount) AS BIGINT) AS  MASterCardReturnCount, SUM( MASterCardNetAmount) AS  MASterCardNetAmount,
		 CAST(SUM( MASterCardTransactionCount) AS BIGINT) AS  MASterCardTransactionCount, SUM( AmericanExpressAmount) AS  AmericanExpressAmount, CAST(SUM( AmericanExpressCount) AS BIGINT) AS  AmericanExpressCount,
		 SUM( AmericanExpressReturnAmount) AS  AmericanExpressReturnAmount, CAST(SUM( AmericanExpressReturnCount) AS BIGINT) AS  AmericanExpressReturnCount, SUM( AmericanExpressNetAmount) AS  AmericanExpressNetAmount, 
		 CAST(SUM( AmericanExpressTransactionCount) AS BIGINT) AS  AmericanExpressTransactionCount, SUM( OtherCardAmount) AS  OtherCardAmount, CAST(SUM( OtherCardCount) AS BIGINT) AS  OtherCardCount, 
		 SUM( OtherCardReturnAmount) AS  OtherCardReturnAmount, CAST(SUM( OtherCardReturnCount) AS BIGINT) AS  OtherCardReturnCount, SUM( OtherCardNetAmount) AS  OtherCardNetAmount,
		 CAST(SUM( OtherCardTransactionCount) AS BIGINT) AS  OtherCardTransactionCount, RegionCode, MerchantType, 'A' as AgentCode
		FROM MERCHANT_SUMMARY_QUARTERLY 
		where (ReportQuarter + (ReportYear * 4)) <= @currentDate
		and (ReportQuarter + (ReportYear * 4)) >= @firstDate
		group by  RegionCode,MerchantType
End
-------------------------------
Create Procedure SP_GetReportData_Yearly (
	@startYear int, @endYear int)
As
Begin
	declare @reportDate varchar(20)
	set @reportDate = CONCAT(@startYear, ' - ', @endYear)
	
		SELECT @reportDate as ReportDate, 'A' as MerchantCode,
		 SUM(SaleAmount) AS SaleAmount, CAST(SUM( SaleCount) AS BIGINT) AS  SaleCount, SUM( ReturnAmount) AS  ReturnAmount, CAST(SUM( ReturnCount) AS BIGINT) AS  ReturnCount, 
		 SUM( NetAmount) AS  NetAmount, CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount, SUM( KeyedAmount) AS  KeyedAmount, CAST(SUM( KeyedCount) AS BIGINT) AS  KeyedCount, 
		 SUM( KeyedReturnAmount) AS  KeyedReturnAmount, CAST(SUM( KeyedReturnCount) AS BIGINT) AS  KeyedReturnCount, SUM( KeyedNetAmount) AS  KeyedNetAmount, 
		 CAST(SUM( KeyedTransactionCount) AS BIGINT) AS  KeyedTransactionCount, SUM( ForeignCardAmount) AS  ForeignCardAmount, CAST(SUM( ForeignCardCount) AS BIGINT) AS  ForeignCardCount,
		 SUM( ForeignCardReturnAmount) AS  ForeignCardReturnAmount, CAST(SUM( ForeignCardReturnCount) AS BIGINT) AS  ForeignCardReturnCount, SUM( ForeignCardNetAmount) AS  ForeignCardNetAmount, 
		 CAST(SUM( ForeignCardTransactionCount) AS BIGINT) AS  ForeignCardTransactionCount, SUM( DebitCardAmount) AS  DebitCardAmount, CAST(SUM(DebitCardCount) AS BIGINT) AS  DebitCardCount, 
		 SUM( DebitCardReturnAmount) AS  DebitCardReturnAmount, CAST(SUM( DebitCardReturnCount) AS BIGINT) AS  DebitCardReturnCount, SUM( DebitCardNetAmount) AS  DebitCardNetAmount, 
		 CAST(SUM(DebitCardTransactionCount) AS BIGINT) AS  DebitCardTransactionCount, SUM( VisaCardAmount) AS  VisaCardAmount, CAST(SUM( VisaCardCount) AS BIGINT) AS  VisaCardCount, 
		 SUM( VisaCardReturnAmount) AS  VisaCardReturnAmount, CAST(SUM( VisaCardReturnCount) AS BIGINT) AS  VisaCardReturnCount, SUM( VisaCardNetAmount) AS  VisaCardNetAmount, 
		 CAST(SUM( VisaCardTransactionCount) AS BIGINT) AS  VisaCardTransactionCount, SUM( DiscoverCardAmount) AS  DiscoverCardAmount, CAST(SUM( DiscoverCardCount) AS BIGINT) AS  DiscoverCardCount,
		 SUM( DiscoverCardReturnAmount) AS  DiscoverCardReturnAmount, CAST(SUM( DiscoverCardReturnCount) AS BIGINT) AS  DiscoverCardReturnCount, SUM( DiscoverCardNetAmount) AS  DiscoverCardNetAmount, 
		 CAST(SUM( DiscoverCardTransactionCount) AS BIGINT) AS  DiscoverCardTransactionCount, SUM( MASterCardAmount) AS  MASterCardAmount, CAST(SUM( MASterCardCount) AS BIGINT) AS  MASterCardCount, 
		 SUM( MASterCardReturnAmount) AS  MASterCardReturnAmount, CAST(SUM( MASterCardReturnCount) AS BIGINT) AS  MASterCardReturnCount, SUM( MASterCardNetAmount) AS  MASterCardNetAmount,
		 CAST(SUM( MASterCardTransactionCount) AS BIGINT) AS  MASterCardTransactionCount, SUM( AmericanExpressAmount) AS  AmericanExpressAmount, CAST(SUM( AmericanExpressCount) AS BIGINT) AS  AmericanExpressCount,
		 SUM( AmericanExpressReturnAmount) AS  AmericanExpressReturnAmount, CAST(SUM( AmericanExpressReturnCount) AS BIGINT) AS  AmericanExpressReturnCount, SUM( AmericanExpressNetAmount) AS  AmericanExpressNetAmount, 
		 CAST(SUM( AmericanExpressTransactionCount) AS BIGINT) AS  AmericanExpressTransactionCount, SUM( OtherCardAmount) AS  OtherCardAmount, CAST(SUM( OtherCardCount) AS BIGINT) AS  OtherCardCount, 
		 SUM( OtherCardReturnAmount) AS  OtherCardReturnAmount, CAST(SUM( OtherCardReturnCount) AS BIGINT) AS  OtherCardReturnCount, SUM( OtherCardNetAmount) AS  OtherCardNetAmount,
		 CAST(SUM( OtherCardTransactionCount) AS BIGINT) AS  OtherCardTransactionCount, RegionCode, MerchantType, 'A' as AgentCode
		FROM MERCHANT_SUMMARY_YEARLY 
		where ReportYear <= @endYear
		and ReportYear >= @startYear
		group by  RegionCode,MerchantType
End
-------------------------------
Create Procedure SP_GetReportDataForLineChart_Monthly (
	@startMonth int, @startYear int, 
	@endMonth int, @endYear int) 
As
Begin
	declare @currentDate int
	declare @firstDate int
	set @currentDate = @endMonth + (@endYear * 12)
	set @firstDate =  @startMonth + (@startYear * 12)
	
	SELECT ConCat(ReportMonth, '/', ReportYear) as Name,
		SUM(SaleAmount) AS Value
	FROM MERCHANT_SUMMARY_MONTHLY 
	where (ReportMonth + (ReportYear * 12)) <= @currentDate
		and (ReportMonth + (ReportYear * 12)) >= @firstDate
	group by  ReportMonth, ReportYear
End
---------------------------------------
Create Procedure SP_GetReportDataForLineChart_Quarterly (
	@startQuarter int, @startYear int, 
	@endQuarter int, @endYear int) 
As
Begin
	declare @currentDate int
	declare @firstDate int
	set @currentDate = @endQuarter + (@startYear * 4)
	set @firstDate =  @startQuarter + (@endYear * 4)

	SELECT ConCat(ReportQuarter, '/', ReportYear) as Name,
		SUM(SaleAmount) AS Value
	FROM MERCHANT_SUMMARY_QUARTERLY 
	where (ReportQuarter + (ReportYear * 4)) <= @currentDate
		and (ReportQuarter + (ReportYear * 4)) >= @firstDate
	group by  ReportQuarter, ReportYear
End
----------------------------------------
Create Procedure SP_GetReportDataForLineChart_Yearly (
	@startYear int, @endYear int) 
As
Begin
	SELECT CONVERT(varchar(4), ReportYear) as Name,
		SUM(SaleAmount) AS Value
	FROM MERCHANT_SUMMARY_YEARLY 
	where ReportYear <= @endYear
		and ReportYear >= @startYear
	group by  ReportYear
End



Alter Procedure SP_GetReportDataForLineChart_Generality (@startDate varchar(10), @endDate varchar(10)) As
Begin
	declare @currentDate date
	declare @firstDate date
	set @currentDate = CAST(@endDate AS DATETIME)
	set @firstDate =  CAST(@startDate AS DATETIME)
	
		SELECT CONVERT(varchar(max),ReportDate,103) as Name,
		 SUM(SaleAmount) AS Value
		FROM MERCHANT_SUMMARY_DAILY 
		where ReportDate < @currentDate and ReportDate >= @firstDate
		group by ReportDate
End
go
Alter Procedure SP_GetReportData_Generality (@startDate varchar(10), @endDate varchar(10)) As
Begin
	declare @currentDate date
	declare @firstDate date
	set @currentDate = CAST(@endDate AS DATETIME)
	set @firstDate =  CAST(@startDate AS DATETIME)
	
		SELECT CONVERT(varchar(max),@currentDate,103) as ReportDate, 'A' as MerchantCode,
		 SUM(SaleAmount) AS SaleAmount, CAST(SUM( SaleCount) AS BIGINT) AS  SaleCount, SUM( ReturnAmount) AS  ReturnAmount, CAST(SUM( ReturnCount) AS BIGINT) AS  ReturnCount, 
		 SUM( NetAmount) AS  NetAmount, CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount, SUM( KeyedAmount) AS  KeyedAmount, CAST(SUM( KeyedCount) AS BIGINT) AS  KeyedCount, 
		 SUM( KeyedReturnAmount) AS  KeyedReturnAmount, CAST(SUM( KeyedReturnCount) AS BIGINT) AS  KeyedReturnCount, SUM( KeyedNetAmount) AS  KeyedNetAmount, 
		 CAST(SUM( KeyedTransactionCount) AS BIGINT) AS  KeyedTransactionCount, SUM( ForeignCardAmount) AS  ForeignCardAmount, CAST(SUM( ForeignCardCount) AS BIGINT) AS  ForeignCardCount,
		 SUM( ForeignCardReturnAmount) AS  ForeignCardReturnAmount, CAST(SUM( ForeignCardReturnCount) AS BIGINT) AS  ForeignCardReturnCount, SUM( ForeignCardNetAmount) AS  ForeignCardNetAmount, 
		 CAST(SUM( ForeignCardTransactionCount) AS BIGINT) AS  ForeignCardTransactionCount, SUM( DebitCardAmount) AS  DebitCardAmount, CAST(SUM(DebitCardCount) AS BIGINT) AS  DebitCardCount, 
		 SUM( DebitCardReturnAmount) AS  DebitCardReturnAmount, CAST(SUM( DebitCardReturnCount) AS BIGINT) AS  DebitCardReturnCount, SUM( DebitCardNetAmount) AS  DebitCardNetAmount, 
		 CAST(SUM(DebitCardTransactionCount) AS BIGINT) AS  DebitCardTransactionCount, SUM( VisaCardAmount) AS  VisaCardAmount, CAST(SUM( VisaCardCount) AS BIGINT) AS  VisaCardCount, 
		 SUM( VisaCardReturnAmount) AS  VisaCardReturnAmount, CAST(SUM( VisaCardReturnCount) AS BIGINT) AS  VisaCardReturnCount, SUM( VisaCardNetAmount) AS  VisaCardNetAmount, 
		 CAST(SUM( VisaCardTransactionCount) AS BIGINT) AS  VisaCardTransactionCount, SUM( DiscoverCardAmount) AS  DiscoverCardAmount, CAST(SUM( DiscoverCardCount) AS BIGINT) AS  DiscoverCardCount,
		 SUM( DiscoverCardReturnAmount) AS  DiscoverCardReturnAmount, CAST(SUM( DiscoverCardReturnCount) AS BIGINT) AS  DiscoverCardReturnCount, SUM( DiscoverCardNetAmount) AS  DiscoverCardNetAmount, 
		 CAST(SUM( DiscoverCardTransactionCount) AS BIGINT) AS  DiscoverCardTransactionCount, SUM( MASterCardAmount) AS  MASterCardAmount, CAST(SUM( MASterCardCount) AS BIGINT) AS  MASterCardCount, 
		 SUM( MASterCardReturnAmount) AS  MASterCardReturnAmount, CAST(SUM( MASterCardReturnCount) AS BIGINT) AS  MASterCardReturnCount, SUM( MASterCardNetAmount) AS  MASterCardNetAmount,
		 CAST(SUM( MASterCardTransactionCount) AS BIGINT) AS  MASterCardTransactionCount, SUM( AmericanExpressAmount) AS  AmericanExpressAmount, CAST(SUM( AmericanExpressCount) AS BIGINT) AS  AmericanExpressCount,
		 SUM( AmericanExpressReturnAmount) AS  AmericanExpressReturnAmount, CAST(SUM( AmericanExpressReturnCount) AS BIGINT) AS  AmericanExpressReturnCount, SUM( AmericanExpressNetAmount) AS  AmericanExpressNetAmount, 
		 CAST(SUM( AmericanExpressTransactionCount) AS BIGINT) AS  AmericanExpressTransactionCount, SUM( OtherCardAmount) AS  OtherCardAmount, CAST(SUM( OtherCardCount) AS BIGINT) AS  OtherCardCount, 
		 SUM( OtherCardReturnAmount) AS  OtherCardReturnAmount, CAST(SUM( OtherCardReturnCount) AS BIGINT) AS  OtherCardReturnCount, SUM( OtherCardNetAmount) AS  OtherCardNetAmount,
		 CAST(SUM( OtherCardTransactionCount) AS BIGINT) AS  OtherCardTransactionCount, RegionCode, MerchantType, 'A' as AgentCode
		FROM MERCHANT_SUMMARY_DAILY 
		where ReportDate < @currentDate and ReportDate >= @firstDate
		group by RegionCode,MerchantType
End

go
-- End Update 08/12/2016 --
alter Procedure SP_GetMerchantSummary_Default
As
Begin
SELECT  top 10000 [ReportDate]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
	  ,[ReturnCount]
      ,[NetAmount]
	  ,[TransactionCount]
	  ,[KeyedAmount]
  FROM [SERVER].[dbo].[MERCHANT_SUMMARY_DAILY]
end
go

alter Procedure SP_FindMerchantSummaryElement @Element varchar(50)
As
Begin
SELECT  top 10000 [ReportDate]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
	  ,[ReturnCount]
      ,[NetAmount]
	  ,[TransactionCount]
	  ,[KeyedAmount]
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
	ORDER BY ReportDate
end
go


alter Procedure SP_FindMerchantSummaryElementByAgent (@Element varchar(50),@AgentCode varchar(10))
As
Begin
SELECT  top 10000 [ReportDate]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
	  ,[ReturnCount]
      ,[NetAmount]
	  ,[TransactionCount]
	  ,[KeyedAmount]
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
	ORDER BY ReportDate
end