DROP PROCEDURE [dbo].[sp_FindRetrivalElement_ForQuery] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindRetrivalElement_ForQuery] @Element varchar(50), @pageIndex int, @pageSize int
AS
BEGIN
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout 
		FROM RETRIVAL a
		WHERE  a.RetrivalCode like '%'+@Element+'%'
			   OR a.AccountNumber like '%'+@Element+'%'
			   OR a.MerchantCode like '%'+@Element+'%'
			   OR a.TransactionCode like '%'+@Element+'%'
			   OR a.Amout like '%'+@Element+'%'	   
			   OR a.ReportDate like '%'+@Element+'%'
				OR a.TransactionDate like '%'+@Element+'%'
		ORDER BY a.ReportDate
		Offset @pageIndex*@pageSize row 
		fetch next @pageSize row only 
END
GO

DROP PROCEDURE [dbo].[sp_CountRetrivalElement] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountRetrivalElement] @Element varchar(50)
AS
BEGIN
		SELECT Count(*) 
		FROM RETRIVAL a
		WHERE  a.RetrivalCode like '%'+@Element+'%'
			   OR a.AccountNumber like '%'+@Element+'%'
			   OR a.MerchantCode like '%'+@Element+'%'
			   OR a.TransactionCode like '%'+@Element+'%'
			   OR a.Amout like '%'+@Element+'%'	   
			   OR a.ReportDate like '%'+@Element+'%'
				OR a.TransactionDate like '%'+@Element+'%'
END
GO

DROP PROCEDURE [dbo].[sp_FindAllRetrival_ForQuery]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_FindAllRetrival_ForQuery]
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from RETRIVAL 
	order by RetrivalCode asc
	Offset @pageIndex*@pageSize row 
	fetch next @pageSize row only 
end
GO

DROP PROCEDURE [dbo].[sp_CountRetrival]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CountRetrival]
AS
BEGIN
	SELECT Count(*)
	FROM RETRIVAL
END
GO
