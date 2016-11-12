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


CREATE proc [dbo].[sp_FindRetrivalElement] @Element varchar(50)
AS
DECLARE @tmp_money MONEY

BEGIN
	IF (ISNUMERIC(@Element) = 1)
	BEGIN
		SET @tmp_money = CONVERT(MONEY, @ELEMENT)
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout 
		FROM RETRIVAL a
		WHERE a.Amout = @tmp_money OR a.AccountNumber = @Element
		ORDER BY a.RetrivalCode
	END
	ELSE
	IF (ISDATE(@Element) = 1)
	BEGIN
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout 
		FROM RETRIVAL a
		WHERE a.ReportDate = @Element
		   OR a.TransactionDate = @Element
		ORDER BY a.RetrivalCode
	END
	ELSE
	BEGIN
		SELECT a.RetrivalCode, a.AccountNumber, a.MerchantCode, a.TransactionCode, a.TransactionDate, a.ReportDate, a.Amout 
		FROM RETRIVAL a
		WHERE  a.RetrivalCode = @Element 
			   OR a.AccountNumber = @Element 
			   OR a.MerchantCode = @Element
			   OR a.TransactionCode = @Element
			   OR a.Amout = @tmp_money
		ORDER BY a.RetrivalCode
	END

END
GO

