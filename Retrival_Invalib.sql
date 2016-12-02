USE [DB]
GO
CREATE PROC [dbo].[sp_GetAllRetrivalInvalid]
as
begin
	select *
	from RETRIVAL_INVALID a
	order by a.RetrivalCode
end
Alter proc [dbo].[sp_FindRetrivalInvalid] @Element varchar(50)
AS
BEGIN
	
		SELECT a.RetrivalCode,ISNULL( a.AccountNumber,0), a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, ISNULL( a.Amout,0), ISNULL(A.ErrorMessage,'Error')
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