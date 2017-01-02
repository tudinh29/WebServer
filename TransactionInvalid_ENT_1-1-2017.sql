USE [SERVER]
GO

--Lấy danh sách transaction detail invalid của master
DROP PROCEDURE [dbo].[sp_GetTransaction_Detail_Invalid_Master] 
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0
----01/01/2017		Pham Van Ha					1.1

CREATE PROC [dbo].[sp_GetTransaction_Detail_Invalid_Master] @pageIndex int, @pageSize int
AS
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
	Offset @pageIndex * @pageSize row 
	fetch next @pageSize row only 
END
GO

--Đếm tổng transaction detail invalid của Master
DROP PROCEDURE [dbo].[sp_CountTransInvalid_Master]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----01/01/2017		Pham Van Ha					1.1
CREATE PROC [dbo].[sp_CountTransInvalid_Master]
AS
BEGIN
		SELECT Count(*) 
		FROM TRANSACTION_DETAIL_INVALID
END
GO

-- Lấy danh sách các Transaction_Invalid theo từng Agent 

----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0
----01/01/2017		Pham Van Ha					1.1

DROP PROCEDURE [dbo].[sp_GetTransaction_Detail_Invalid_Agent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_GetTransaction_Detail_Invalid_Agent] @AgentCode varchar(10), @pageIndex int, @pageSize int
As
BEGIN
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
	Offset @pageIndex * @pageSize row 
	fetch next @pageSize row only 
END
GO

--Đếm tổng transaction detail invalid của Agent
DROP PROCEDURE [dbo].[sp_CountTransInvalid_Agent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----01/01/2017		Pham Van Ha					1.1
CREATE PROC [dbo].[sp_CountTransInvalid_Agent] @AgentCode varchar(10)
AS
BEGIN
		SELECT Count(*) 
		FROM TRANSACTION_DETAIL_INVALID t join MERCHANT m on (m.MerchantCode= t.MerchantCode AND m.AgentCode= @AgentCode)
END
GO

-- -- Lấy danh sách các Transaction_Invalid theo từng Merchant

----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0
----01/01/2017		Pham Van Ha					1.1

DROP PROCEDURE [dbo].[sp_GetTransaction_Detail_Invalid_Merchant] 
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_GetTransaction_Detail_Invalid_Merchant] @MerchantCode varchar(10), @pageIndex int, @pageSize int
As
BEGIN
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
	Offset @pageIndex * @pageSize row 
	fetch next @pageSize row only 
END
GO

--Đếm tổng transaction detail invalid của Merchant
DROP PROCEDURE [dbo].[sp_CountTransInvalid_Merchant] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----01/01/2017		Pham Van Ha					1.1
CREATE PROC [dbo].[sp_CountTransInvalid_Merchant] @MerchantCode varchar(10)
AS
BEGIN
		SELECT Count(*) 
		FROM TRANSACTION_DETAIL_INVALID t join MERCHANT m on (m.MerchantCode= t.MerchantCode AND m.MerchantCode = @MerchantCode)
END
GO

-- tìm kiếm Transaction_Detail_Invalid( user là Master)
DROP PROCEDURE [dbo].[sp_FindTransInvalidElement_Master]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0
----01/01/2017		Pham Van Ha					1.1

CREATE PROC [dbo].[sp_FindTransInvalidElement_Master] @Element varchar(50), @pageIndex int, @pageSize int
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
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
END
GO

--Đếm tổng tìm kiếm transaction detail invalid của Master
DROP PROCEDURE [dbo].[sp_CountTransInvalidElements_Master]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----01/01/2017		Pham Van Ha					1.1
CREATE PROC [dbo].[sp_CountTransInvalidElements_Master] @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM TRANSACTION_DETAIL_INVALID
		WHERE (TransactionCode like '%'+@Element+'%' OR
		  MerchantCode like '%'+@Element+'%' OR
		  CardtypeCode like '%'+@Element+'%' OR
		  AccountNumber like '%'+@Element+'%' OR
		  FirstTwelveAccountNumber like '%'+@Element+'%' OR
		  TransactionTypeCode like '%'+@Element+'%' OR
		  AgentCode like '%'+@Element+'%') 
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
----01/01/2017		Pham Van Ha					1.1

CREATE PROC [dbo].[sp_FindTransInvalidElement_Agent] @Element varchar(50), @AgentCode varchar(10), @pageIndex int, @pageSize int
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
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
END
GO

--Đếm tổng tìm kiếm transaction detail invalid của Agent
DROP PROCEDURE [dbo].[sp_CountTransInvalidElements_Agent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----01/01/2017		Pham Van Ha					1.1
CREATE PROC [dbo].[sp_CountTransInvalidElements_Agent] @AgentCode varchar(10), @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM TRANSACTION_DETAIL_INVALID t join MERCHANT m on (m.MerchantCode= t.MerchantCode AND m.AgentCode= @AgentCode)
		WHERE (TransactionCode like '%'+@Element+'%' OR
		  t.MerchantCode like '%'+@Element+'%' OR
		  CardtypeCode like '%'+@Element+'%' OR
		  AccountNumber like '%'+@Element+'%' OR
		  FirstTwelveAccountNumber like '%'+@Element+'%' OR
		  TransactionTypeCode like '%'+@Element+'%' OR
		  t.AgentCode like '%'+@Element+'%')
END
GO

---- tìm kiếm Transaction_Detail_Invalid( user là Merchant)

DROP PROCEDURE [dbo].[sp_FindTransInvalidElement_Merchant]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----28/11/2016		Nguyen Thi Dieu         	1.0
----01/01/2017		Pham Van Ha					1.1

CREATE PROC [dbo].[sp_FindTransInvalidElement_Merchant] @Element varchar(50), @MerchantCode varchar(10), @pageIndex int, @pageSize int
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
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
END
GO

--Đếm tổng tìm kiếm transaction detail invalid của Merchant
DROP PROCEDURE [dbo].[sp_CountTransInvalidElements_Merchant] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----Ngay			NguoiChinhSua				Version-----
----01/01/2017		Pham Van Ha					1.1
CREATE PROC [dbo].[sp_CountTransInvalidElements_Merchant] @MerchantCode varchar(10), @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM TRANSACTION_DETAIL_INVALID t join MERCHANT m on (m.MerchantCode= t.MerchantCode AND m.MerchantCode = @MerchantCode)
		WHERE (TransactionCode like '%'+@Element+'%' OR
		  t.MerchantCode like '%'+@Element+'%' OR
		  CardtypeCode like '%'+@Element+'%' OR
		  AccountNumber like '%'+@Element+'%' OR
		  FirstTwelveAccountNumber like '%'+@Element+'%' OR
		  TransactionTypeCode like '%'+@Element+'%' OR
		  t.AgentCode like '%'+@Element+'%')
END
GO