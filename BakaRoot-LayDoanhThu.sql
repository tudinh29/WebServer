USE SERVER
GO
create proc sp_LayDoanhThuMerchant 
	@MerchantCode varchar(120)
AS
	declare @sum money
	DECLARE @average money
	DECLARE @sum_lastmonth money
	DECLARE @average_lastmonth money 
	declare @startday date
	declare @endday date
	DECLARE @lastmonth_firstday date
	DECLARE @lastmonth_lastday date
	DECLARE @merchant varchar(10)
	DECLARE @index int
	DECLARE @t TABLE(MerchantCode varchar(20), DoanhThu money, TrungBinh money, TangTruong nvarchar(20))

	set @endday = GETDATE()
	set @startday = DATEADD(dd, -(DAY(@endday)) + 1, @endday)
	set @lastmonth_lastday = DATEADD(dd, -1, @startday)
	set @lastmonth_firstday = DATEADD(dd, -(DAY(@lastmonth_lastday)) + 1, @lastmonth_lastday)

	DECLARE @length int
	SET @length = LEN(@MerchantCode)
	DECLARE @i int
	SET @i = 1
	while @length > 0
	Begin
		SET @index = CHARINDEX(',', @MerchantCode, @i)

		IF @index = 0
		BEGIN
			SET @merchant = SUBSTRING(@MerchantCode, @i, @length)
			SET @index = @length
		END
		ELSE
		BEGIN
			SET @merchant = SUBSTRING(@MerchantCode, @i, @index - 1)
		END
		
		SET @MerchantCode = RIGHT(@MerchantCode, @length - @index)
		SET @length = LEN(@MerchantCode)
		set @sum = (SELECT SUM(NetAmount)
						FROM MERCHANT_SUMMARY_DAILY
						WHERE MerchantCode = @merchant AND ReportDate between @startday and @endday)
		set @average = @sum / DATEDIFF(d, @startday, @endday)
		set @sum_lastmonth = (SELECT SUM(NetAmount)
								FROM MERCHANT_SUMMARY_DAILY
								WHERE MerchantCode = @merchant AND ReportDate between @lastmonth_firstday and @lastmonth_lastday)
		set @average_lastmonth = @sum_lastmonth / DATEDIFF(d, @lastmonth_firstday, @lastmonth_lastday)
		insert into @t values (@merchant, @sum, @average, CASE 
															WHEN (@average - @average_lastmonth) < 0 THEN N'Giảm'
															WHEN (@average - @average_lastmonth) = 0 THEN N'Ổn định'
															WHEN (@average - @average_lastmonth) > 0 THEN N'Tăng'
															ELSE N'Chưa có so sánh'
															END)
	end
	select * from @t
GO