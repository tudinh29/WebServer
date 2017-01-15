-- Update 20/12/2016 - Fix not suplied code parameter.
-- Update 18/12/2016 - adding filter by username
drop Procedure SP_GetReportData_Monthly 
go
Create Procedure SP_GetReportData_Monthly (@startMonth int, @startYear int, 
	@endMonth int, @endYear int, @code varchar(20)) 
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
		FROM (Select * From MERCHANT_SUMMARY_MONTHLY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_MONTHLY
		where (ReportMonth + (ReportYear * 12)) <= @currentDate
			and (ReportMonth + (ReportYear * 12)) >= @firstDate
		group by  RegionCode,MerchantType
End
go
go
drop Procedure SP_GetReportData_Quarterly 
go
-------------------------------
Create Procedure SP_GetReportData_Quarterly (
	@startQuarter int, @startYear int, 
	@endQuarter int, @endYear int, @code varchar(20)) 
As
Begin
	declare @reportDate varchar(20)
	declare @currentDate int
	declare @firstDate int
	set @currentDate = @endQuarter + (@endYear * 4)
	set @firstDate =  @startQuarter + (@startYear * 4)

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
		FROM (Select * From MERCHANT_SUMMARY_QUARTERLY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_QUARTERLY
		where (ReportQuarter + (ReportYear * 4)) <= @currentDate
		and (ReportQuarter + (ReportYear * 4)) >= @firstDate
		group by  RegionCode,MerchantType
End
go
drop Procedure SP_GetReportData_Yearly 
go
-------------------------------
Create Procedure SP_GetReportData_Yearly (
	@startYear int, @endYear int, @code varchar(20))
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
		FROM (Select * From MERCHANT_SUMMARY_YEARLY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_YEARLY
		where ReportYear <= @endYear
		and ReportYear >= @startYear
		group by  RegionCode,MerchantType
End
go
drop Procedure SP_GetReportDataForLineChart_Monthly 
go
-------------------------------
Create Procedure SP_GetReportDataForLineChart_Monthly (
	@startMonth int, @startYear int, 
	@endMonth int, @endYear int, @code varchar(20)) 
As
Begin
	declare @currentDate int
	declare @firstDate int
	set @currentDate = @endMonth + (@endYear * 12)
	set @firstDate =  @startMonth + (@startYear * 12)
	
	SELECT ConCat(ReportMonth, '/', ReportYear) as Name,
		SUM(SaleAmount) AS Value, SUM(ReturnAmount) as ReturnAmount, CAST(SUM(TransactionCount)as BIGINT) as TransactionCount
	FROM (Select * From MERCHANT_SUMMARY_MONTHLY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_MONTHLY
	where (ReportMonth + (ReportYear * 12)) <= @currentDate
		and (ReportMonth + (ReportYear * 12)) >= @firstDate
	group by  ReportMonth, ReportYear
	Order by ReportYear,ReportMonth
End
go
drop Procedure SP_GetReportDataForLineChart_Quarterly 
go
---------------------------------------
Create Procedure SP_GetReportDataForLineChart_Quarterly (
	@startQuarter int, @startYear int, 
	@endQuarter int, @endYear int, @code varchar(20)) 
As
Begin
	declare @currentDate int
	declare @firstDate int
	set @currentDate = @endQuarter + (@endYear * 4)
	set @firstDate =  @startQuarter + (@startYear * 4)

	SELECT ConCat(ReportQuarter, '/', ReportYear) as Name,
		SUM(SaleAmount) AS Value, SUM(ReturnAmount) as ReturnAmount, CAST(SUM(TransactionCount)as BIGINT) as TransactionCount
	FROM (Select * From MERCHANT_SUMMARY_QUARTERLY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_QUARTERLY 
	where (ReportQuarter + (ReportYear * 4)) <= @currentDate
		and (ReportQuarter + (ReportYear * 4)) >= @firstDate
	group by  ReportQuarter, ReportYear
	Order by ReportYear,ReportQuarter
End
go
drop Procedure SP_GetReportDataForLineChart_Yearly 
go
----------------------------------------
Create Procedure SP_GetReportDataForLineChart_Yearly (
	@startYear int, @endYear int, @code varchar(20)) 
As
Begin
	SELECT CONVERT(varchar(4), ReportYear) as Name,
		SUM(SaleAmount) AS Value, SUM(ReturnAmount) as ReturnAmount, CAST(SUM(TransactionCount)as BIGINT) as TransactionCount
	FROM (Select * From MERCHANT_SUMMARY_YEARLY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_YEARLY 
	where ReportYear <= @endYear
		and ReportYear >= @startYear
	group by  ReportYear
	Order by ReportYear
End
-- end update 18/12/2016

-- Update 08/12/2016 --
/*drop Procedure SP_GetReportData_Monthly 
go
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
go
go
drop Procedure SP_GetReportData_Quarterly 
go
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
go
drop Procedure SP_GetReportData_Yearly 
go
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
go
drop Procedure SP_GetReportDataForLineChart_Monthly 
go
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
		SUM(SaleAmount) AS Value, SUM(ReturnAmount) as ReturnAmount, CAST(SUM(TransactionCount)as BIGINT) as TransactionCount
	FROM MERCHANT_SUMMARY_MONTHLY
	where (ReportMonth + (ReportYear * 12)) <= @currentDate
		and (ReportMonth + (ReportYear * 12)) >= @firstDate
	group by  ReportMonth, ReportYear
	Order by ReportYear,ReportMonth
End
go
drop Procedure SP_GetReportDataForLineChart_Quarterly 
go
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
		SUM(SaleAmount) AS Value, SUM(ReturnAmount) as ReturnAmount, CAST(SUM(TransactionCount)as BIGINT) as TransactionCount
	FROM MERCHANT_SUMMARY_QUARTERLY 
	where (ReportQuarter + (ReportYear * 4)) <= @currentDate
		and (ReportQuarter + (ReportYear * 4)) >= @firstDate
	group by  ReportQuarter, ReportYear
	Order by ReportYear,ReportQuarter
End
go
drop Procedure SP_GetReportDataForLineChart_Yearly 
go
----------------------------------------
Create Procedure SP_GetReportDataForLineChart_Yearly (
	@startYear int, @endYear int) 
As
Begin
	SELECT CONVERT(varchar(4), ReportYear) as Name,
		SUM(SaleAmount) AS Value, SUM(ReturnAmount) as ReturnAmount, CAST(SUM(TransactionCount)as BIGINT) as TransactionCount
	FROM MERCHANT_SUMMARY_YEARLY 
	where ReportYear <= @endYear
		and ReportYear >= @startYear
	group by  ReportYear
	Order by ReportYear
End
*/

-- Update 05/01/2017 - adding code to SP
go
drop Procedure SP_GetReportDataForLineChart_Generality
go
create Procedure SP_GetReportDataForLineChart_Generality (@startDate varchar(10), @endDate varchar(10), @code varchar(20)) As
Begin
	declare @currentDate date
	declare @firstDate date
	set @currentDate = CAST(@endDate AS DATETIME)
	set @firstDate =  CAST(@startDate AS DATETIME)
	
		SELECT CONVERT(varchar(max),ReportDate,103) as Name,
		SUM(SaleAmount) AS Value, SUM(ReturnAmount) as ReturnAmount, CAST(SUM(TransactionCount)as BIGINT) as TransactionCount
		FROM (Select * From MERCHANT_SUMMARY_DAILY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_DAILY 
		where ReportDate < @currentDate and ReportDate >= @firstDate
		group by ReportDate
		Order by ReportDate
End
go
drop Procedure SP_GetReportData_Generality
go
create Procedure SP_GetReportData_Generality (@startDate varchar(10), @endDate varchar(10), @code varchar(20)) As
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
		FROM (Select * From MERCHANT_SUMMARY_DAILY Where (MerchantCode = @code or AgentCode = @code) or @code = '' or @code is null) MERCHANT_SUMMARY_DAILY 
		where ReportDate < @currentDate and ReportDate >= @firstDate
		group by RegionCode,MerchantType
End

go
-- End Update 08/12/2016 --
drop Procedure SP_GetMerchantSummary_Default
go
create Procedure SP_GetMerchantSummary_Default
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
drop Procedure SP_FindMerchantSummaryElement
go
create Procedure SP_FindMerchantSummaryElement @Element varchar(50)
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

drop Procedure SP_FindMerchantSummaryElementByAgent
go
create Procedure SP_FindMerchantSummaryElementByAgent (@Element varchar(50),@AgentCode varchar(10))
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