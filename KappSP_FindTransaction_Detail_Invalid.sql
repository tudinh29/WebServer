USE [SERVER]
GO


DROP PROCEDURE [dbo].[sp_FindAllTransaction_Detail_Invalid]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0

CREATE PROC [dbo].[sp_FindAllTransaction_Detail_Invalid]-- lấy toàn bộ danh sách các Transaction_Detail_Invalid
As
Begin
	SELECT t.[TransactionCode]
      ,t.[ReportDate]
      ,t.[MerchantCode]
      ,t.[CardtypeCode]
      ,t.[TransactionAmount]
      ,t.[TransactionDate]
      ,t.[AccountNumber]
      ,t.[TransactionTypeCode]
      ,t.[AgentCode]
      ,t.[ErrorMessage]
	from TRANSACTION_DETAIL_INVALID t
	order by t.ReportDate
END
GO

-- tìm kiếm Transaction_Detail_Invalid( user là Master)
DROP PROCEDURE [dbo].[sp_FindTransInvalidElement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0
CREATE PROC [dbo].[sp_FindTransInvalidElement] @Element varchar(50)
As
Begin
	SELECT [TransactionCode]
      ,[ReportDate]
      ,[MerchantCode]
      ,[CardtypeCode]
      ,[TransactionAmount]
      ,[TransactionDate]
      ,[AccountNumber]
      ,[TransactionTypeCode]
      ,[AgentCode]
      ,[ErrorMessage]
	FROM TRANSACTION_DETAIL_INVALID
	WHERE TransactionCode like '%'+@Element+'%' OR
		  MerchantCode like '%'+@Element+'%' OR
		  CardtypeCode like '%'+@Element+'%' OR
		  AccountNumber like '%'+@Element+'%' OR
		  FirstTwelveAccountNumber like '%'+@Element+'%' OR
		  TransactionTypeCode like '%'+@Element+'%' OR
		  MerchantCode like '%'+@Element+'%' OR
		  AgentCode like '%'+@Element+'%' 
	ORDER BY ReportDate
END

GO


---- tìm kiếm Transaction_Detail_Invalid( user là Agent)
DROP PROCEDURE [dbo].[sp_FindTransInvalidElement_Agent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0

CREATE PROC [dbo].[sp_FindTransInvalidElement_Agent] @Element varchar(50), @AgentCode varchar(10)
As
Begin
	SELECT t.[TransactionCode]
      ,t.[ReportDate]
      ,t.[MerchantCode]
      ,t.[CardtypeCode]
      ,t.[TransactionAmount]
      ,t.[TransactionDate]
      ,t.[AccountNumber]
      ,t.[TransactionTypeCode]
      ,t.[AgentCode]
      ,t.[ErrorMessage]
	FROM TRANSACTION_DETAIL_INVALID t join MERCHANT m on (m.MerchantCode= t.MerchantCode AND m.AgentCode= @AgentCode)  
	WHERE (t.TransactionCode like '%'+@Element+'%' OR
		  t.MerchantCode like '%'+@Element+'%' OR
		  t.CardtypeCode like '%'+@Element+'%' OR
		  t.AccountNumber like '%'+@Element+'%' OR
		  t.FirstTwelveAccountNumber like '%'+@Element+'%' OR
		  t.TransactionTypeCode like '%'+@Element+'%' OR
		  t.AgentCode like '%'+@Element+'%') 
	ORDER BY t.ReportDate
END

GO
---- tìm kiếm Transaction_Detail_Invalid( user là Merchant)

----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0

DROP PROCEDURE [dbo].[sp_FindTransInvalidElement_Merchant]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_FindTransInvalidElement_Merchant] @Element varchar(50), @MerchantCode varchar(10)
As
Begin
	SELECT [TransactionCode]
      ,[ReportDate]
      ,[MerchantCode]
      ,[CardtypeCode]
      ,[TransactionAmount]
      ,[TransactionDate]
      ,[AccountNumber]
      ,[TransactionTypeCode]
      ,[AgentCode]
      ,[ErrorMessage]
	FROM TRANSACTION_DETAIL_INVALID
	WHERE (TransactionCode like '%'+@Element+'%' OR
		  MerchantCode like '%'+@Element+'%' OR
		  CardtypeCode like '%'+@Element+'%' OR
		  AccountNumber like '%'+@Element+'%' OR
		  FirstTwelveAccountNumber like '%'+@Element+'%' OR
		  TransactionTypeCode like '%'+@Element+'%' OR
		  AgentCode like '%'+@Element+'%') 
		  AND MerchantCode = @MerchantCode
	ORDER BY ReportDate
END
GO
-- Lấy danh sách các Transaction_Invalid theo từng Agent 

----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0

DROP PROCEDURE [dbo].[sp_TransInvalid_Agent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_TransInvalid_Agent] @AgentCode varchar(10)
As
Begin
	SELECT t.[TransactionCode]
      ,t.[ReportDate]
      ,t.[MerchantCode]
      ,t.[CardtypeCode]
      ,t.[TransactionAmount]
      ,t.[TransactionDate]
      ,t.[AccountNumber]
      ,t.[TransactionTypeCode]
      ,t.[AgentCode]
      ,t.[ErrorMessage]
	FROM TRANSACTION_DETAIL_INVALID t join MERCHANT m on (m.MerchantCode= t.MerchantCode AND m.AgentCode= @AgentCode)  
	ORDER BY ReportDate
End
GO

-- -- Lấy danh sách các Transaction_Invalid theo từng Merchant

----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0

DROP PROCEDURE [dbo].[sp_TransInvalid_Merchant] 
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_TransInvalid_Merchant] @MerchantCode varchar(10)
As
Begin
	SELECT [TransactionCode]
      ,[ReportDate]
      ,[MerchantCode]
      ,[CardtypeCode]
      ,[TransactionAmount]
      ,[TransactionDate]
      ,[AccountNumber]
      ,[TransactionTypeCode]
      ,[AgentCode]
      ,[ErrorMessage]
	FROM TRANSACTION_DETAIL_INVALID
	WHERE MerchantCode= @MerchantCode
	ORDER BY ReportDate
End












