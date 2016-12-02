Create Procedure SP_GetReportDataForLineChart_Generality (@startDate varchar(10), @endDate varchar(10)) As
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

Create Procedure SP_GetReportData_Generality (@startDate varchar(10), @endDate varchar(10)) As
Begin
	declare @currentDate date
	declare @firstDate date
	set @currentDate = CAST(@endDate AS DATETIME)
	set @firstDate =  CAST(@startDate AS DATETIME)
	
		SELECT @currentDate as ReportDate, 'A' as MerchantCode,
		 SUM(SaleAmount) AS SaleAmount, SUM( SaleCount) AS  SaleCount, SUM( ReturnAmount) AS  ReturnAmount, SUM( ReturnCount) AS  ReturnCount, 
		 SUM( NetAmount) AS  NetAmount, SUM( TransactionCount) AS  TransactionCount, SUM( KeyedAmount) AS  KeyedAmount, SUM( KeyedCount) AS  KeyedCount, 
		 SUM( KeyedReturnAmount) AS  KeyedReturnAmount, SUM( KeyedReturnCount) AS  KeyedReturnCount, SUM( KeyedNetAmount) AS  KeyedNetAmount, 
		 SUM( KeyedTransactionCount) AS  KeyedTransactionCount, SUM( ForeignCardAmount) AS  ForeignCardAmount, SUM( ForeignCardCount) AS  ForeignCardCount,
		 SUM( ForeignCardReturnAmount) AS  ForeignCardReturnAmount, SUM( ForeignCardReturnCount) AS  ForeignCardReturnCount, SUM( ForeignCardNetAmount) AS  ForeignCardNetAmount, 
		 SUM( ForeignCardTransactionCount) AS  ForeignCardTransactionCount, SUM( DebitCardAmount) AS  DebitCardAmount, SUM(DebitCardCount) AS  DebitCardCount, 
		 SUM( DebitCardReturnAmount) AS  DebitCardReturnAmount, SUM( DebitCardReturnCount) AS  DebitCardReturnCount, SUM( DebitCardNetAmount) AS  DebitCardNetAmount, 
		 SUM(DebitCardTransactionCount) AS  DebitCardTransactionCount, SUM( VisaCardAmount) AS  VisaCardAmount, SUM( VisaCardCount) AS  VisaCardCount, 
		 SUM( VisaCardReturnAmount) AS  VisaCardReturnAmount, SUM( VisaCardReturnCount) AS  VisaCardReturnCount, SUM( VisaCardNetAmount) AS  VisaCardNetAmount, 
		 SUM( VisaCardTransactionCount) AS  VisaCardTransactionCount, SUM( DiscoverCardAmount) AS  DiscoverCardAmount, SUM( DiscoverCardCount) AS  DiscoverCardCount,
		 SUM( DiscoverCardReturnAmount) AS  DiscoverCardReturnAmount, SUM( DiscoverCardReturnCount) AS  DiscoverCardReturnCount, SUM( DiscoverCardNetAmount) AS  DiscoverCardNetAmount, 
		 SUM( DiscoverCardTransactionCount) AS  DiscoverCardTransactionCount, SUM( MASterCardAmount) AS  MASterCardAmount, SUM( MASterCardCount) AS  MASterCardCount, 
		 SUM( MASterCardReturnAmount) AS  MASterCardReturnAmount, SUM( MASterCardReturnCount) AS  MASterCardReturnCount, SUM( MASterCardNetAmount) AS  MASterCardNetAmount,
		 SUM( MASterCardTransactionCount) AS  MASterCardTransactionCount, SUM( AmericanExpressAmount) AS  AmericanExpressAmount, SUM( AmericanExpressCount) AS  AmericanExpressCount,
		 SUM( AmericanExpressReturnAmount) AS  AmericanExpressReturnAmount, SUM( AmericanExpressReturnCount) AS  AmericanExpressReturnCount, SUM( AmericanExpressNetAmount) AS  AmericanExpressNetAmount, 
		 SUM( AmericanExpressTransactionCount) AS  AmericanExpressTransactionCount, SUM( OtherCardAmount) AS  OtherCardAmount, SUM( OtherCardCount) AS  OtherCardCount, 
		 SUM( OtherCardReturnAmount) AS  OtherCardReturnAmount, SUM( OtherCardReturnCount) AS  OtherCardReturnCount, SUM( OtherCardNetAmount) AS  OtherCardNetAmount,
		 SUM( OtherCardTransactionCount) AS  OtherCardTransactionCount, RegionCode, MerchantType, 'A' as AgentCode
		FROM MERCHANT_SUMMARY_DAILY 
		where ReportDate < @currentDate and ReportDate >= @firstDate
		group by RegionCode,MerchantType
End