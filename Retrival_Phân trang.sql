--Alter procedure sp_FindRetrivalElement
ALTER proc [dbo].[sp_FindRetrivalElement] @Element varchar(50), @pageIndex int, @pageSize int
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

CREATE proc [dbo].[sp_CountRetrivalElement] @Element varchar(50)
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

--Alter proc find all retrival
ALTER proc [dbo].[sp_FindAllRetrival]
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

CREATE PROC [dbo].[sp_CountRetrival]
AS
BEGIN
	SELECT Count(*)
	FROM RETRIVAL
END
