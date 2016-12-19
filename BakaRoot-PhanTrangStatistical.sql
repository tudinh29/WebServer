----------------------YEARLY----------------------------
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryYearlyElement_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryYearlyElement_ForQuery] 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_YEARLY] 
			WHERE [ReportYear] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%'
					  OR [AgentCode] like '%'+@Element+'%'
			ORDER BY [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryYearlyElement] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryYearlyElement] @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_YEARLY 
		WHERE [ReportYear] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%'
					  OR [AgentCode] like '%'+@Element+'%'
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryYearly_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryYearly_ForQuery]
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_YEARLY 
	order by ReportYear asc
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryYearly]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryYearly]
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_YEARLY
END
GO

DROP PROCEDURE [dbo].[sp_GetMerchantSummaryYearly]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_GetMerchantSummaryYearly]
	@ReportYear varchar(4),
	@MerchantCode varchar(50)
AS
BEGIN
	SELECT [ReportYear]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
      ,[ReturnCount]
      ,[NetAmount]
      ,[TransactionCount]
      ,[KeyedAmount]
      ,[KeyedCount]
      ,[KeyedReturnAmount]
      ,[KeyedReturnCount]
      ,[KeyedNetAmount]
      ,[KeyedTransactionCount]
      ,[ForeignCardAmount]
      ,[ForeignCardCount]
      ,[ForeignCardReturnAmount]
      ,[ForeignCardReturnCount]
      ,[ForeignCardNetAmount]
      ,[ForeignCardTransactionCount]
      ,[DebitCardAmount]
      ,[DebitCardCount]
      ,[DebitCardReturnAmount]
      ,[DebitCardReturnCount]
      ,[DebitCardNetAmount]
      ,[DebitCardTransactionCount]
      ,[VisaCardAmount]
      ,[VisaCardCount]
      ,[VisaCardReturnAmount]
      ,[VisaCardReturnCount]
      ,[VisaCardNetAmount]
      ,[VisaCardTransactionCount]
      ,[DiscoverCardAmount]
      ,[DiscoverCardCount]
      ,[DiscoverCardReturnAmount]
      ,[DiscoverCardReturnCount]
      ,[DiscoverCardNetAmount]
      ,[DiscoverCardTransactionCount]
      ,[MasterCardAmount]
      ,[MasterCardCount]
      ,[MasterCardReturnAmount]
      ,[MasterCardReturnCount]
      ,[MasterCardNetAmount]
      ,[MasterCardTransactionCount]
      ,[AmericanExpressAmount]
      ,[AmericanExpressCount]
      ,[AmericanExpressReturnAmount]
      ,[AmericanExpressReturnCount]
      ,[AmericanExpressNetAmount]
      ,[AmericanExpressTransactionCount]
      ,[OtherCardAmount]
      ,[OtherCardCount]
      ,[OtherCardReturnAmount]
      ,[OtherCardReturnCount]
      ,[OtherCardNetAmount]
      ,[OtherCardTransactionCount]
      ,[RegionCode]
      ,[MerchantType]
      ,[AgentCode]
  FROM [dbo].[MERCHANT_SUMMARY_YEARLY]
  WHERE MerchantCode = @MerchantCode AND ReportYear = @ReportYear
END
GO

--AGENT--
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryYearlyElement_Agent_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryYearlyElement_Agent_ForQuery]
	@AgentCode varchar(50), 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_YEARLY] 
			WHERE [AgentCode] = @AgentCode
					  AND ([ReportYear] = @Element
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
			ORDER BY [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryYearlyElement_Agent] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryYearlyElement_Agent] 
	@AgentCode varchar(50),
	@Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_YEARLY 
		WHERE [AgentCode] = @AgentCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryYearly_Agent_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryYearly_Agent_ForQuery]
	@AgentCode varchar(50),
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_YEARLY
	WHERE AgentCode = @AgentCode 
	order by ReportYear asc
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryYearly_Agent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryYearly_Agent]
	@AgentCode varchar(50)
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_YEARLY
	WHERE AgentCode = @AgentCode
END
GO
---MERCHANT---
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryYearlyElement_Merchant_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryYearlyElement_Merchant_ForQuery]
	@MerchantCode varchar(50), 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_YEARLY] 
			WHERE [MerchantCode] = @MerchantCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
			ORDER BY [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryYearlyElement_Merchant] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryYearlyElement_Merchant] 
	@MerchantCode varchar(50),
	@Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_YEARLY 
		WHERE [MerchantCode] = @MerchantCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryYearly_Merchant_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryYearly_Merchant_ForQuery]
	@MerchantCode varchar(50),
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_YEARLY
	WHERE MerchantCode = @MerchantCode
	order by ReportYear asc
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryYearly_Merchant]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryYearly_Merchant]
	@MerchantCode varchar(50)
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_YEARLY
	WHERE MerchantCode = @MerchantCode
END
GO


-------------------MONTHLY--------------------------
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryMonthlyElement_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryMonthlyElement_ForQuery] 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportMonth]
			  ,[ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_MONTHLY] 
			WHERE [ReportYear] like '%'+@Element+'%'
					  OR [ReportMonth] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%'
					  OR [AgentCode] like '%'+@Element+'%'
			ORDER BY [ReportMonth], [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryMonthlyElement] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryMonthlyElement] @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_MONTHLY 
		WHERE [ReportYear] like '%'+@Element+'%'
					  OR [ReportMonth] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%'
					  OR [AgentCode] like '%'+@Element+'%'
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryMonthly_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryMonthly_ForQuery]
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_MONTHLY 
	order by [ReportMonth], [ReportYear]
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryMonthly]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryMonthly]
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_MONTHLY
END
GO

DROP PROCEDURE [dbo].[sp_GetMerchantSummaryMonthly]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_GetMerchantSummaryMonthly]
	@ReportMonth int,
	@ReportYear varchar(4),
	@MerchantCode varchar(50)
AS
BEGIN
	SELECT [ReportMonth]
	  ,[ReportYear]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
      ,[ReturnCount]
      ,[NetAmount]
      ,[TransactionCount]
      ,[KeyedAmount]
      ,[KeyedCount]
      ,[KeyedReturnAmount]
      ,[KeyedReturnCount]
      ,[KeyedNetAmount]
      ,[KeyedTransactionCount]
      ,[ForeignCardAmount]
      ,[ForeignCardCount]
      ,[ForeignCardReturnAmount]
      ,[ForeignCardReturnCount]
      ,[ForeignCardNetAmount]
      ,[ForeignCardTransactionCount]
      ,[DebitCardAmount]
      ,[DebitCardCount]
      ,[DebitCardReturnAmount]
      ,[DebitCardReturnCount]
      ,[DebitCardNetAmount]
      ,[DebitCardTransactionCount]
      ,[VisaCardAmount]
      ,[VisaCardCount]
      ,[VisaCardReturnAmount]
      ,[VisaCardReturnCount]
      ,[VisaCardNetAmount]
      ,[VisaCardTransactionCount]
      ,[DiscoverCardAmount]
      ,[DiscoverCardCount]
      ,[DiscoverCardReturnAmount]
      ,[DiscoverCardReturnCount]
      ,[DiscoverCardNetAmount]
      ,[DiscoverCardTransactionCount]
      ,[MasterCardAmount]
      ,[MasterCardCount]
      ,[MasterCardReturnAmount]
      ,[MasterCardReturnCount]
      ,[MasterCardNetAmount]
      ,[MasterCardTransactionCount]
      ,[AmericanExpressAmount]
      ,[AmericanExpressCount]
      ,[AmericanExpressReturnAmount]
      ,[AmericanExpressReturnCount]
      ,[AmericanExpressNetAmount]
      ,[AmericanExpressTransactionCount]
      ,[OtherCardAmount]
      ,[OtherCardCount]
      ,[OtherCardReturnAmount]
      ,[OtherCardReturnCount]
      ,[OtherCardNetAmount]
      ,[OtherCardTransactionCount]
      ,[RegionCode]
      ,[MerchantType]
      ,[AgentCode]
  FROM [dbo].[MERCHANT_SUMMARY_MONTHLY]
  WHERE MerchantCode = @MerchantCode AND ReportMonth = @ReportMonth AND ReportYear = @ReportYear
END
GO

--AGENT--
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryMonthlyElement_Agent_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryMonthlyElement_Agent_ForQuery]
	@AgentCode varchar(50), 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportMonth]
			  ,[ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_MONTHLY] 
			WHERE [AgentCode] = @AgentCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportMonth] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
			ORDER BY [ReportMonth], [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryMonthlyElement_Agent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryMonthlyElement_Agent] 
	@AgentCode varchar(50),
	@Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_MONTHLY 
		WHERE [AgentCode] = @AgentCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportMonth] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryMonthly_Agent_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryMonthly_Agent_ForQuery]
	@AgentCode varchar(50),
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_MONTHLY
	WHERE AgentCode = @AgentCode 
	order by [ReportMonth], [ReportYear]
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryMonthly_Agent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryMonthly_Agent]
	@AgentCode varchar(50)
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_MONTHLY
	WHERE AgentCode = @AgentCode
END
GO
---MERCHANT---
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryMonthlyElement_Merchant_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryMonthlyElement_Merchant_ForQuery]
	@MerchantCode varchar(50), 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportMonth]
			  ,[ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_MONTHLY] 
			WHERE [MerchantCode] = @MerchantCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportMonth] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
			ORDER BY [ReportMonth], [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryMonthlyElement_Merchant] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryMonthlyElement_Merchant] 
	@MerchantCode varchar(50),
	@Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_MONTHLY 
		WHERE [MerchantCode] = @MerchantCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportMonth] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryMonthly_Merchant_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryMonthly_Merchant_ForQuery]
	@MerchantCode varchar(50),
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_MONTHLY
	WHERE MerchantCode = @MerchantCode
	order by [ReportMonth], [ReportYear]
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryMonthly_Merchant]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryMonthly_Merchant]
	@MerchantCode varchar(50)
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_MONTHLY
	WHERE MerchantCode = @MerchantCode
END
GO

-------QUARTER--------
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryQuarterlyElement_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryQuarterlyElement_ForQuery] 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportQuarter]
			  ,[ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_QUARTERLY] 
			WHERE [ReportYear] like '%'+@Element+'%'
					  OR [ReportQuarter] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%'
					  OR [AgentCode] like '%'+@Element+'%'
			ORDER BY [ReportQuarter], [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryQuarterlyElement] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryQuarterlyElement] @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_QUARTERLY 
		WHERE [ReportYear] like '%'+@Element+'%'
					  OR [ReportQuarter] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%'
					  OR [AgentCode] like '%'+@Element+'%'
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryQuarterly_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryQuarterly_ForQuery]
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_QUARTERLY 
	order by [ReportQuarter], [ReportYear]
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryQuarterly]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryQuarterly]
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_QUARTERLY
END
GO

DROP PROCEDURE [dbo].[sp_GetMerchantSummaryQuarterly]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_GetMerchantSummaryQuarterly]
	@ReportQuarter int,
	@ReportYear varchar(4),
	@MerchantCode varchar(50)
AS
BEGIN
	SELECT [ReportQuarter]
	  ,[ReportYear]
      ,[MerchantCode]
      ,[SaleAmount]
      ,[SaleCount]
      ,[ReturnAmount]
      ,[ReturnCount]
      ,[NetAmount]
      ,[TransactionCount]
      ,[KeyedAmount]
      ,[KeyedCount]
      ,[KeyedReturnAmount]
      ,[KeyedReturnCount]
      ,[KeyedNetAmount]
      ,[KeyedTransactionCount]
      ,[ForeignCardAmount]
      ,[ForeignCardCount]
      ,[ForeignCardReturnAmount]
      ,[ForeignCardReturnCount]
      ,[ForeignCardNetAmount]
      ,[ForeignCardTransactionCount]
      ,[DebitCardAmount]
      ,[DebitCardCount]
      ,[DebitCardReturnAmount]
      ,[DebitCardReturnCount]
      ,[DebitCardNetAmount]
      ,[DebitCardTransactionCount]
      ,[VisaCardAmount]
      ,[VisaCardCount]
      ,[VisaCardReturnAmount]
      ,[VisaCardReturnCount]
      ,[VisaCardNetAmount]
      ,[VisaCardTransactionCount]
      ,[DiscoverCardAmount]
      ,[DiscoverCardCount]
      ,[DiscoverCardReturnAmount]
      ,[DiscoverCardReturnCount]
      ,[DiscoverCardNetAmount]
      ,[DiscoverCardTransactionCount]
      ,[MasterCardAmount]
      ,[MasterCardCount]
      ,[MasterCardReturnAmount]
      ,[MasterCardReturnCount]
      ,[MasterCardNetAmount]
      ,[MasterCardTransactionCount]
      ,[AmericanExpressAmount]
      ,[AmericanExpressCount]
      ,[AmericanExpressReturnAmount]
      ,[AmericanExpressReturnCount]
      ,[AmericanExpressNetAmount]
      ,[AmericanExpressTransactionCount]
      ,[OtherCardAmount]
      ,[OtherCardCount]
      ,[OtherCardReturnAmount]
      ,[OtherCardReturnCount]
      ,[OtherCardNetAmount]
      ,[OtherCardTransactionCount]
      ,[RegionCode]
      ,[MerchantType]
      ,[AgentCode]
  FROM [dbo].[MERCHANT_SUMMARY_QUARTERLY]
  WHERE MerchantCode = @MerchantCode AND ReportQuarter = @ReportQuarter AND ReportYear = @ReportYear 
END
GO
--AGENT--
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryQuarterlyElement_Agent_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryQuarterlyElement_Agent_ForQuery]
	@AgentCode varchar(50), 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportQuarter]
			  ,[ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_QUARTERLY] 
			WHERE [AgentCode] = @AgentCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportQuarter] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
			ORDER BY [ReportQuarter], [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryQuarterlyElement_Agent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryQuarterlyElement_Agent] 
	@AgentCode varchar(50),
	@Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_QUARTERLY 
		WHERE [AgentCode] = @AgentCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportQuarter] like '%'+@Element+'%'
					  OR [MerchantCode] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryQuarterly_Agent_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryQuarterly_Agent_ForQuery]
	@AgentCode varchar(50),
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_QUARTERLY
	WHERE AgentCode = @AgentCode 
	order by [ReportQuarter], [ReportYear]
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryQuarterly_Agent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryQuarterly_Agent]
	@AgentCode varchar(50)
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_QUARTERLY
	WHERE AgentCode = @AgentCode
END
GO
---MERCHANT---
DROP PROCEDURE [dbo].[sp_FindMerchantSummaryQuarterlyElement_Merchant_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindMerchantSummaryQuarterlyElement_Merchant_ForQuery]
	@MerchantCode varchar(50), 
	@Element varchar(50), 
	@pageIndex int, 
	@pageSize int
AS
BEGIN
		SELECT [ReportQuarter]
			  ,[ReportYear]
			  ,[MerchantCode]
			  ,[SaleAmount]
			  ,[SaleCount]
			  ,[ReturnAmount]
			  ,[ReturnCount]
			  ,[NetAmount]
			  ,[TransactionCount]
			  ,[KeyedAmount]
			  ,[KeyedCount]
			  ,[KeyedReturnAmount]
			  ,[KeyedReturnCount]
			  ,[KeyedNetAmount]
			  ,[KeyedTransactionCount]
			  ,[ForeignCardAmount]
			  ,[ForeignCardCount]
			  ,[ForeignCardReturnAmount]
			  ,[ForeignCardReturnCount]
			  ,[ForeignCardNetAmount]
			  ,[ForeignCardTransactionCount]
			  ,[DebitCardAmount]
			  ,[DebitCardCount]
			  ,[DebitCardReturnAmount]
			  ,[DebitCardReturnCount]
			  ,[DebitCardNetAmount]
			  ,[DebitCardTransactionCount]
			  ,[VisaCardAmount]
			  ,[VisaCardCount]
			  ,[VisaCardReturnAmount]
			  ,[VisaCardReturnCount]
			  ,[VisaCardNetAmount]
			  ,[VisaCardTransactionCount]
			  ,[DiscoverCardAmount]
			  ,[DiscoverCardCount]
			  ,[DiscoverCardReturnAmount]
			  ,[DiscoverCardReturnCount]
			  ,[DiscoverCardNetAmount]
			  ,[DiscoverCardTransactionCount]
			  ,[MasterCardAmount]
			  ,[MasterCardCount]
			  ,[MasterCardReturnAmount]
			  ,[MasterCardReturnCount]
			  ,[MasterCardNetAmount]
			  ,[MasterCardTransactionCount]
			  ,[AmericanExpressAmount]
			  ,[AmericanExpressCount]
			  ,[AmericanExpressReturnAmount]
			  ,[AmericanExpressReturnCount]
			  ,[AmericanExpressNetAmount]
			  ,[AmericanExpressTransactionCount]
			  ,[OtherCardAmount]
			  ,[OtherCardCount]
			  ,[OtherCardReturnAmount]
			  ,[OtherCardReturnCount]
			  ,[OtherCardNetAmount]
			  ,[OtherCardTransactionCount]
			  ,[RegionCode]
			  ,[MerchantType]
			  ,[AgentCode]
		  FROM [dbo].[MERCHANT_SUMMARY_QUARTERLY] 
			WHERE [MerchantCode] = @MerchantCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportQuarter] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
			ORDER BY [ReportQuarter], [ReportYear]
			Offset @pageIndex*@pageSize row 
			fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryQuarterlyElement_Merchant] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryQuarterlyElement_Merchant] 
	@MerchantCode varchar(50),
	@Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM MERCHANT_SUMMARY_QUARTERLY 
		WHERE [MerchantCode] = @MerchantCode
					  AND ([ReportYear] like '%'+@Element+'%'
					  OR [ReportQuarter] like '%'+@Element+'%'
					  OR [SaleAmount] like '%'+@Element+'%'
					  OR [SaleCount] like '%'+@Element+'%'
					  OR [ReturnAmount] like '%'+@Element+'%'
					  OR [ReturnCount] like '%'+@Element+'%'
					  OR [NetAmount] like '%'+@Element+'%'
					  OR [TransactionCount] like '%'+@Element+'%'
					  OR [KeyedAmount] like '%'+@Element+'%'
					  OR [KeyedCount] like '%'+@Element+'%'
					  OR [KeyedReturnAmount] like '%'+@Element+'%'
					  OR [KeyedReturnCount] like '%'+@Element+'%'
					  OR [KeyedNetAmount] like '%'+@Element+'%'
					  OR [KeyedTransactionCount] like '%'+@Element+'%'
					  OR [ForeignCardAmount] like '%'+@Element+'%'
					  OR [ForeignCardCount] like '%'+@Element+'%'
					  OR [ForeignCardReturnAmount] like '%'+@Element+'%'
					  OR [ForeignCardReturnCount] like '%'+@Element+'%'
					  OR [ForeignCardNetAmount] like '%'+@Element+'%'
					  OR [ForeignCardTransactionCount] like '%'+@Element+'%'
					  OR [DebitCardAmount] like '%'+@Element+'%'
					  OR [DebitCardCount] like '%'+@Element+'%'
					  OR [DebitCardReturnAmount] like '%'+@Element+'%'
					  OR [DebitCardReturnCount] like '%'+@Element+'%'
					  OR [DebitCardNetAmount] like '%'+@Element+'%'
					  OR [DebitCardTransactionCount] like '%'+@Element+'%'
					  OR [VisaCardAmount] like '%'+@Element+'%'
					  OR [VisaCardCount] like '%'+@Element+'%'
					  OR [VisaCardReturnAmount] like '%'+@Element+'%'
					  OR [VisaCardReturnCount] like '%'+@Element+'%'
					  OR [VisaCardNetAmount] like '%'+@Element+'%'
					  OR [VisaCardTransactionCount] like '%'+@Element+'%'
					  OR [DiscoverCardAmount] like '%'+@Element+'%'
					  OR [DiscoverCardCount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnAmount] like '%'+@Element+'%'
					  OR [DiscoverCardReturnCount] like '%'+@Element+'%'
					  OR [DiscoverCardNetAmount] like '%'+@Element+'%'
					  OR [DiscoverCardTransactionCount] like '%'+@Element+'%'
					  OR [MasterCardAmount] like '%'+@Element+'%'
					  OR [MasterCardCount] like '%'+@Element+'%'
					  OR [MasterCardReturnAmount] like '%'+@Element+'%'
					  OR [MasterCardReturnCount] like '%'+@Element+'%'
					  OR [MasterCardNetAmount] like '%'+@Element+'%'
					  OR [MasterCardTransactionCount] like '%'+@Element+'%'
					  OR [AmericanExpressAmount] like '%'+@Element+'%'
					  OR [AmericanExpressCount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnAmount] like '%'+@Element+'%'
					  OR [AmericanExpressReturnCount] like '%'+@Element+'%'
					  OR [AmericanExpressNetAmount] like '%'+@Element+'%'
					  OR [AmericanExpressTransactionCount] like '%'+@Element+'%'
					  OR [OtherCardAmount] like '%'+@Element+'%'
					  OR [OtherCardCount] like '%'+@Element+'%'
					  OR [OtherCardReturnAmount] like '%'+@Element+'%'
					  OR [OtherCardReturnCount] like '%'+@Element+'%'
					  OR [OtherCardNetAmount] like '%'+@Element+'%'
					  OR [OtherCardTransactionCount] like '%'+@Element+'%'
					  OR [RegionCode] like '%'+@Element+'%'
					  OR [MerchantType] like '%'+@Element+'%')
END
GO

DROP PROCEDURE [dbo].[sp_FindAllMerchantSummaryQuarterly_Merchant_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllMerchantSummaryQuarterly_Merchant_ForQuery]
	@MerchantCode varchar(50),
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from MERCHANT_SUMMARY_QUARTERLY
	WHERE MerchantCode = @MerchantCode
	order by [ReportQuarter], [ReportYear]
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountMerchantSummaryQuarterly_Merchant]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountMerchantSummaryQuarterly_Merchant]
	@MerchantCode varchar(50)
AS
BEGIN
	SELECT Count(*)
	FROM MERCHANT_SUMMARY_QUARTERLY
	WHERE MerchantCode = @MerchantCode
END
GO
