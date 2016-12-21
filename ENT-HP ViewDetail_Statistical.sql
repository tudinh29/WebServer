USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMerchantSummaryForMaster]    Script Date: 21/12/2016 12:24:57 SA ******/
DROP PROCEDURE [dbo].[SP_GetMerchantSummaryForMaster]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_GetMerchantSummaryForMaster] @MerchantCode varchar(10), @ReportDate datetime
As
Begin
SELECT *
  FROM MERCHANT_SUMMARY_DAILY
  WHERE MerchantCode= @MerchantCode AND ReportDate=@ReportDate
End
GO

USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMerchantSummaryForMerchant_Default]    Script Date: 21/12/2016 12:25:12 SA ******/
DROP PROCEDURE [dbo].[SP_GetMerchantSummaryForMerchant_Default]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_GetMerchantSummaryForMerchant_Default] @MerchantCode varchar(10)
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
  WHERE  MerchantCode = @MerchantCode
End
GO

USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMerchantSummaryForMerchant_Default_ForQuery]    Script Date: 21/12/2016 12:25:37 SA ******/
DROP PROCEDURE [dbo].[SP_GetMerchantSummaryForMerchant_Default_ForQuery]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_GetMerchantSummaryForMerchant_Default_ForQuery]
 @MerchantCode varchar(10),
 @pageIndex int,
 @pageSize int
As
Begin
SELECT *
  FROM MERCHANT_SUMMARY_DAILY
  WHERE  MerchantCode = @MerchantCode
  ORDER BY ReportDate desc
  Offset (@pageIndex - 1)*@pageSize row 
  fetch next @pageSize row only 
End
GO

USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMerchantSummaryForMerchantCode]    Script Date: 21/12/2016 12:25:47 SA ******/
DROP PROCEDURE [dbo].[SP_GetMerchantSummaryForMerchantCode]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_GetMerchantSummaryForMerchantCode] @MerchantCode varchar(10)
As
Begin
SELECT *
  FROM MERCHANT_SUMMARY_DAILY
  WHERE MerchantCode= @MerchantCode
End
GO

USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMerchantTypeName]    Script Date: 21/12/2016 12:25:59 SA ******/
DROP PROCEDURE [dbo].[SP_GetMerchantTypeName]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[SP_GetMerchantTypeName]
@MerchantType VARCHAR(10)
AS
BEGIN
	SELECT *
	FROM MERCHANT_TYPE  
	WHERE MerchantType = @MerchantType
END
GO

USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRegionName]    Script Date: 21/12/2016 12:26:12 SA ******/
DROP PROCEDURE [dbo].[SP_GetRegionName]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[SP_GetRegionName]
@RegionCode VARCHAR(10)
AS
BEGIN
	SELECT *
	FROM REGION
	WHERE RegionCode = @RegionCode
END
GO

USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMerchantSummaryForAgent_Default_MerchantCode]    Script Date: 21/12/2016 12:26:58 SA ******/
DROP PROCEDURE [dbo].[SP_GetMerchantSummaryForAgent_Default_MerchantCode]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_GetMerchantSummaryForAgent_Default_MerchantCode] @AgentCode varchar(10),@MerchantCode varchar(10),@ReportDate Date
As
Begin
SELECT *
  FROM MERCHANT_SUMMARY_DAILY
  WHERE  AgentCode = @AgentCode AND MerchantCode= @MerchantCode And ReportDate=@ReportDate
End
GO

USE [SERVER]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCountMerchantSummaryForMerchant_Default_ForQuery]    Script Date: 21/12/2016 12:28:18 SA ******/
DROP PROCEDURE [dbo].[SP_GetCountMerchantSummaryForMerchant_Default_ForQuery]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_GetCountMerchantSummaryForMerchant_Default_ForQuery]
 @MerchantCode varchar(10)
As
Begin
SELECT Count(*)
  FROM MERCHANT_SUMMARY_DAILY
  WHERE  MerchantCode = @MerchantCode
End