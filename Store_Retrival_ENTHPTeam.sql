Create proc sp_FindAllRetrival
as
begin
	select * 
	from RETRIVAL a
	order by a.RetrivalCode
	
end
go 

Create proc sp_GetRetrival
@RetrivalCode VARCHAR(10)
as
begin
	select *
	from RETRIVAL a
	where RetrivalCode = @RetrivalCode
end
go


create proc [dbo].[sp_FindRetrivalElement] @Element varchar(50)
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
END
GO

