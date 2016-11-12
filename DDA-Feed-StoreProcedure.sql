

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
Create Procedure SP_GetAllStatistic
As
Begin
SELECT [ReportDate]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[ReturnAmount]
      ,[RegionCode]
      ,[MerchantType]
      ,[AgentCode]
  FROM [SERVER].[dbo].[MERCHANT_SUMMARY_DAILY]
End