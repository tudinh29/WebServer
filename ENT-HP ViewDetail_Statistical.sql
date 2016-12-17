USE [SERVER]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetMerchantSummaryForAgent_Default_MerchantCode]    Script Date: 12/17/2016 10:57:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure SP_GetMerchantSummaryForMerchantCode @MerchantCode varchar(10)
As
Begin
SELECT [ReportDate]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
	  ,[ReturnCount]
      ,[NetAmount]
	  ,[TransactionCount]
	  ,[KeyedAmount]
  FROM MERCHANT_SUMMARY_DAILY
  WHERE   MerchantCode= @MerchantCode
End

GO


CREATE Procedure SP_GetMerchantSummaryForAgent_Default_MerchantCode @AgentCode varchar(10),@MerchantCode varchar(10),@ReportDate Date
As
Begin
SELECT [ReportDate]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
	  ,[ReturnCount]
      ,[NetAmount]
	  ,[TransactionCount]
	  ,[KeyedAmount]
  FROM MERCHANT_SUMMARY_DAILY
  WHERE  AgentCode = @AgentCode AND MerchantCode= @MerchantCode And ReportDate=@ReportDate
End

GO


