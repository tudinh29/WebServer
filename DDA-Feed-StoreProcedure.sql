

Create Procedure SP_MerchantTypeStatistic
As
Begin
	Select (Select Description 
			From MERCHANT_TYPE mt 
			Where msd.MerchantType = mt.MerchantType) Name, 
			Cast(Count(*) as decimal) Value
	From MERCHANT_SUMMARY_DAILY msd
	Group By msd.MerchantType
End
go

Create Procedure SP_MerchantRegionStatistic
As
Begin
	Select (Select r.RegionName 
			From REGION r
			Where r.RegionCode =  msd.RegionCode) Name, 

			Cast(count(RegionCode) as Decimal) Value
	From MERCHANT_SUMMARY_DAILY msd
	Group by msd.RegionCode
End
go
Create Procedure SP_MechantDailyRevenue
As
Begin
	Select (CONVERT(VARCHAR(10),ReportDate,103)) Name, SUM(SaleAmount)- SUM(ReturnAmount) Value
	From MERCHANT_SUMMARY_DAILY msd
	Group By ReportDate
End
go
exec SP_CardTypeStatistic
Create Procedure SP_CardTypeStatistic
As
Begin
  Select (Select ct.Description 
		From CARD_TYPE ct 
		Where ct.CardTypeCode = c.CardTypeCode) Name, 
		Cast(Count(c.CardTypeCode) as decimal) Value
  From Card c
  Group By c.CardTypeCode
End
go

create Procedure SP_GetAllStatistic
As
Begin
	declare @date date
	set @date = getDate()
		SELECT '' as ReportDate, 'A' as MerchantCode,
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
		group by RegionCode,MerchantType
End