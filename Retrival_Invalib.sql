USE [SERVER]
GO
drop PROC [dbo].[sp_GetAllRetrivalInvalid]
go
create PROC [dbo].[sp_GetAllRetrivalInvalid]
as
begin
	SELECT a.RetrivalCode,ISNULL( a.AccountNumber,0) as AccountNumber, a.MerchantCode, a.TransactionCode, ISNULL(a.TransactionDate,'1900-1-1') AS TransactionDate, a.ReportDate, ISNULL( a.Amout,0) as Amout, ISNULL(A.ErrorMessage,'Error') as ErrorMessage
	from RETRIVAL_INVALID a
	order by a.RetrivalCode
end
go
drop proc [dbo].[sp_FindRetrivalInvalid]
go
create proc [dbo].[sp_FindRetrivalInvalid] @Element varchar(50)
AS
BEGIN
	
		SELECT a.RetrivalCode,ISNULL( a.AccountNumber,0) as AccountNumber, a.MerchantCode, a.TransactionCode, ISNULL(a.TransactionDate,'1900-1-1')AS TransactionDate, a.ReportDate, ISNULL( a.Amout,0) as Amout, ISNULL(A.ErrorMessage,'Error') as ErrorMessage
		FROM RETRIVAL_INVALID a
		WHERE  a.RetrivalCode like '%'+@Element+'%'
			   OR a.AccountNumber like '%'+@Element+'%'
			   OR a.MerchantCode like '%'+@Element+'%'
			   OR a.TransactionCode like '%'+@Element+'%'
			   OR a.Amout like '%'+@Element+'%'	   
			   OR a.ReportDate like '%'+@Element+'%'
				OR a.TransactionDate like '%'+@Element+'%'
		ORDER BY a.RetrivalCode
END