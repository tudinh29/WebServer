--Version		Người chỉnh sửa			Ngày		Nội dung chỉnh sửa
--1.0			Nguyễn Phạm Hoàng Diễm	31/12/2016	Tạo store procedure
--1.1			Nguyễn Phạm Hoàng Diễm	1/1/2017	Chỉnh sửa giá trị null thành 0
USE SERVER
GO
DROP PROCEDURE [dbo].[sp_LayDoanhThuMerchant] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc sp_LayDoanhThuMerchant 
	@MerchantCode varchar(120)
AS
	DECLARE @sum money
	DECLARE @average money
	DECLARE @sum_lastmonth money
	DECLARE @average_lastmonth money 
	DECLARE @startday date
	DECLARE @endday date
	DECLARE @lastmonth_firstday date
	DECLARE @lastmonth_lastday date
	DECLARE @merchant varchar(10)
	DECLARE @index int
	DECLARE @t TABLE(MerchantCode varchar(20), DoanhThu money, TrungBinh money, TangTruong decimal(18,1))

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
		SET @sum = COALESCE (@sum, 0)
		IF @startday = @endday
		BEGIN
			SET @average = @sum
		END
		ELSE
		BEGIN
			set @average = @sum / DATEDIFF(d, @startday, @endday)
		END

		set @sum_lastmonth = (SELECT SUM(NetAmount)
								FROM MERCHANT_SUMMARY_DAILY
								WHERE MerchantCode = @merchant AND ReportDate between @lastmonth_firstday and @lastmonth_lastday)
		SET @sum_lastmonth = COALESCE (@sum_lastmonth, 0)
		IF @lastmonth_firstday = @lastmonth_lastday
		BEGIN
			SET @average_lastmonth = @sum_lastmonth
		END
		ELSE
		BEGIN
			set @average_lastmonth = @sum_lastmonth / DATEDIFF(d, @lastmonth_firstday, @lastmonth_lastday)
		END

		insert into @t values (@merchant, @sum, @average, CASE
		                                                    WHEN @average_lastmonth = 0 THEN 0
                                                            ELSE (@average - @average_lastmonth)/@average_lastmonth*100
														END)	
	end
	select * from @t
	
GO

--Version		Người chỉnh sửa			Ngày		Nội dung chỉnh sửa
--1.0			Nguyễn Phạm Hoàng Diễm	31/12/2016	Tạo store procedure
--1.1			Nguyễn Phạm Hoàng Diễm	1/1/2017	Chỉnh sửa giá trị null thành 0
DROP PROCEDURE [dbo].[sp_LayDoanhThuAgent] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc sp_LayDoanhThuAgent 
	@AgentCode varchar(120)
AS
	declare @sum money
	DECLARE @average money
	DECLARE @sum_lastmonth money
	DECLARE @average_lastmonth money 
	declare @startday date
	declare @endday date
	DECLARE @lastmonth_firstday date
	DECLARE @lastmonth_lastday date
	DECLARE @Agent varchar(10)
	DECLARE @index int
	DECLARE @t TABLE(AgentCode varchar(20), DoanhThu money, TrungBinh money, TangTruong decimal(18,1))

	set @endday = GETDATE()
	set @startday = DATEADD(dd, -(DAY(@endday)) + 1, @endday)
	set @lastmonth_lastday = DATEADD(dd, -1, @startday)
	set @lastmonth_firstday = DATEADD(dd, -(DAY(@lastmonth_lastday)) + 1, @lastmonth_lastday)

	DECLARE @length int
	SET @length = LEN(@AgentCode)
	DECLARE @i int
	SET @i = 1
	while @length > 0
	Begin
		SET @index = CHARINDEX(',', @AgentCode, @i)

		IF @index = 0
		BEGIN
			SET @Agent = SUBSTRING(@AgentCode, @i, @length)
			SET @index = @length
		END
		ELSE
		BEGIN
			SET @Agent = SUBSTRING(@AgentCode, @i, @index - 1)
		END
		
		SET @AgentCode = RIGHT(@AgentCode, @length - @index)
		SET @length = LEN(@AgentCode)
		set @sum = (SELECT SUM(NetAmount)
						FROM MERCHANT_SUMMARY_DAILY
						WHERE AgentCode = @Agent AND ReportDate between @startday and @endday)
		SET @sum = COALESCE (@sum, 0)
		IF @startday = @endday
		BEGIN
			SET @average = @sum
		END
		ELSE
		BEGIN
			set @average = @sum / DATEDIFF(d, @startday, @endday)
		END
		
		set @sum_lastmonth = (SELECT SUM(NetAmount)
								FROM MERCHANT_SUMMARY_DAILY
								WHERE AgentCode = @Agent AND ReportDate between @lastmonth_firstday and @lastmonth_lastday)
		SET @sum_lastmonth = COALESCE (@sum_lastmonth, 0)
		IF @lastmonth_firstday = @lastmonth_lastday
		BEGIN
			SET @average_lastmonth = @sum_lastmonth
		END
		ELSE
		BEGIN
			set @average_lastmonth = @sum_lastmonth / DATEDIFF(d, @lastmonth_firstday, @lastmonth_lastday)
		END
		insert into @t values (@Agent, @sum, @average, CASE
		                                                    WHEN @average_lastmonth = 0 THEN 0
                                                            ELSE (@average - @average_lastmonth)/@average_lastmonth*100

														END)											
	end
	select * from @t
GO

--Version		Người chỉnh sửa			Ngày		Nội dung chỉnh sửa
--1.0			Nguyễn Phạm Hoàng Diễm	31/12/2016	Tạo store procedure
drop proc sp_FindAllAgent_ForQuery
go
CREATE PROC sp_FindAllAgent_ForQuery
	@pageIndex int,
	@pageSize int
as
begin
	select * 
	from Agent
	order by AgentCode asc
	Offset (@pageIndex - 1)*@pageSize row 
	fetch next @pageSize row only 
end
GO

--Version		Người chỉnh sửa			Ngày		Nội dung chỉnh sửa
--1.0			Nguyễn Phạm Hoàng Diễm	31/12/2016	Tạo store procedure
drop proc sp_CountAgent
go
CREATE PROC sp_CountAgent
AS
BEGIN
	SELECT Count(*)
	FROM Agent
END
GO

--Version		Người chỉnh sửa			Ngày		Nội dung chỉnh sửa
--1.0			Nguyễn Phạm Hoàng Diễm	31/12/2016	Tạo store procedure
drop proc sp_FindAgentElement_ForQuery
go
create proc [dbo].[sp_FindAgentElement_ForQuery] @Element varchar(50), @pageIndex int, @pageSize int
AS

BEGIN
	SELECT [AgentCode]
			  ,[AgentName]
			  ,[AgentStatus]
			  ,[Owner]
			  ,[Address1]
			  ,[Address2]
			  ,[Address3]
			  ,[CityCode]
			  ,[Zip]
			  ,[Phone]
			  ,[Fax]
			  ,[Email]
			  ,[ApprovalDate]
			  ,[CloseDate]
			  ,[FirstActiveDate]
			  ,[LastActiveDate]
			  ,[CityName]
			  ,[RegionCode]
			  ,[RegionName]
	FROM [dbo].[Agent] a
	WHERE  a.AgentCode like '%'+@Element+'%' 
			OR a.AgentName like '%'+@Element+'%' 
			OR a.Owner like '%'+@Element+'%'
			OR a.AgentStatus like '%'+@Element+'%'
			OR a.Address1 like '%'+@Element+'%'
			OR a.Address2 like '%'+@Element+'%'
			OR a.Address3 like '%'+@Element+'%'
			OR a.CityCode like '%'+@Element+'%'
			OR a.Phone like '%'+@Element+'%'
			OR a.Fax like '%'+@Element+'%'
			OR a.Email like '%'+@Element+'%'
			OR a.Zip like '%'+@Element+'%'
			OR a.ApprovalDate like '%'+@Element+'%'
			OR a.CloseDate like '%'+@Element+'%'
			OR a.FirstActiveDate like '%'+@Element+'%'
			OR a.LastActiveDate like '%'+@Element+'%'
			OR a.CityName like '%'+@Element+'%'
			OR a.RegionCode like '%'+@Element+'%'
			OR a.RegionName like '%'+@Element+'%'
	ORDER BY a.AgentCode
	Offset(@pageIndex - 1)*@pageSize row 
	fetch next @pageSize row only 
END
GO

--Version		Người chỉnh sửa			Ngày		Nội dung chỉnh sửa
--1.0			Nguyễn Phạm Hoàng Diễm	31/12/2016	Tạo store procedure
drop proc sp_CountAgentElement_ForQuery 
go
create proc [dbo].[sp_CountAgentElement_ForQuery] @Element varchar(50)
AS

BEGIN
	SELECT COUNT(*)
	FROM [dbo].[Agent] a
	WHERE  a.AgentCode like '%'+@Element+'%' 
			OR a.AgentName like '%'+@Element+'%' 
			OR a.Owner like '%'+@Element+'%'
			OR a.AgentStatus like '%'+@Element+'%'
			OR a.Address1 like '%'+@Element+'%'
			OR a.Address2 like '%'+@Element+'%'
			OR a.Address3 like '%'+@Element+'%'
			OR a.CityCode like '%'+@Element+'%'
			OR a.Phone like '%'+@Element+'%'
			OR a.Fax like '%'+@Element+'%'
			OR a.Email like '%'+@Element+'%'
			OR a.Zip like '%'+@Element+'%'
			OR a.ApprovalDate like '%'+@Element+'%'
			OR a.CloseDate like '%'+@Element+'%'
			OR a.FirstActiveDate like '%'+@Element+'%'
			OR a.LastActiveDate like '%'+@Element+'%'
			OR a.CityName like '%'+@Element+'%'
			OR a.RegionCode like '%'+@Element+'%'
			OR a.RegionName like '%'+@Element+'%'
END
GO
--Autofill textbox
--Version		Người chỉnh sửa			Ngày		Nội dung chỉnh sửa
--1.0			Nguyễn Lê Hoàng An		31/12/2016	Tạo store procedure
drop proc sp_GetViewListMerchant
go
CREATE proc sp_GetViewListMerchant
	@AgentCode varchar(10),
	@KeyWord varchar(10)
AS

BEGIN
	SELECT m.MerchantCode
	FROM AGENT a, MERCHANT m
	WHERE a.AgentCode = @AgentCode
		AND m.AgentCode != @AgentCode
		AND m.RegionCode = a.RegionCode
		AND m.MerchantCode like '%'+@KeyWord+'%'
END
GO

