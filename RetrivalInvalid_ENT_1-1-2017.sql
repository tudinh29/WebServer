----Ngay			NguoiChinhSua				Version-----
----01/01/2017		Pham Van Ha					1.1
USE SERVER

DROP PROCEDURE [dbo].[sp_FindRetrivalInvalidElement] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindRetrivalInvalidElement] @Element varchar(50), @pageIndex int, @pageSize int
AS
BEGIN
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout , a.ErrorMessage
		FROM RETRIVAL_INVALID a
		WHERE  a.RetrivalCode like '%'+@Element+'%'
			   OR a.AccountNumber like '%'+@Element+'%'
			   OR a.MerchantCode like '%'+@Element+'%'
			   OR a.TransactionCode like '%'+@Element+'%'
			   OR a.Amout like '%'+@Element+'%'	   
			   OR a.ReportDate like '%'+@Element+'%'
				OR a.TransactionDate like '%'+@Element+'%'
				OR a.ErrorMessage like '%' +@Element+'%'
		ORDER BY a.ReportDate
		Offset @pageIndex*@pageSize row 
		fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountRetrivalInvalidElement] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountRetrivalInvalidElement] @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM RETRIVAL_INVALID a
		WHERE  a.RetrivalCode like '%'+@Element+'%'
			   OR a.AccountNumber like '%'+@Element+'%'
			   OR a.MerchantCode like '%'+@Element+'%'
			   OR a.TransactionCode like '%'+@Element+'%'
			   OR a.Amout like '%'+@Element+'%'	   
			   OR a.ReportDate like '%'+@Element+'%'
				OR a.TransactionDate like '%'+@Element+'%'
				OR a.ErrorMessage like '%'+@Element+'%'
END
GO

DROP PROCEDURE [dbo].[sp_FindAllRetrivalInvalid]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllRetrivalInvalid]
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from RETRIVAL_INVALID 
	order by RetrivalCode asc
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountRetrivalInvalid]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountRetrivalInvalid]
AS
BEGIN
	SELECT Count(*)
	FROM RETRIVAL_INVALID
END
GO

