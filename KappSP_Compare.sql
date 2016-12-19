USE [SERVER]
GO

----Ngay			NguoiTao				Version-----
----16/12/2016		Nguyen Thi Dieu         	1.0



-- Phan he MASTER

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_compare]    Script Date: 12/18/2016 7:48:00 PM ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
drop Procedure SP_GetReportData_Generality_compare
go
CREATE Procedure [dbo].[SP_GetReportData_Generality_compare] (@month1 int, @year1 int, @month2 int, @year2 int) As
Begin
	declare @currentDate date
	declare @firstDate date
	declare @currentDate2 date
	declare @firstDate2 date
	declare @startdate1 varchar(10)
	declare @enddate1 varchar(10)
	declare @startdate2 varchar(10)
	declare @enddate2 varchar(10)
	set @startdate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-01';
	set @startdate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-01'; 
	
	If (@month1 = 1 OR @month1= 3 OR @month1= 5 OR @month1 = 7 OR @month1 = 8 OR @month1 = 10 OR @month1 = 12)
		BEGIN
			set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-31'; 
		END
	ELSE
		IF (@month1 = 4 OR @month1 = 6 OR @month1 =9 OR @month1 = 11)
			set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-30'; 
		ELSE
			BEGIN
				IF(@year1 % 400 = 0)
					set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-29';
				ELSE
					set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-28';
			END

			If (@month2 = 1 OR @month2= 3 OR @month2= 5 OR @month2 = 7 OR @month2 = 8 OR @month2 = 10 OR @month2 = 12)
		BEGIN
			set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-31'; 
		END
	ELSE
		IF (@month2 = 4 OR @month2= 6 OR @month2 =9 OR @month2 = 11)
			set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-30'; 
		ELSE
			BEGIN
				IF(@year2 % 400 = 0)
					set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-29';
				ELSE
					set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-28';
			END

	print @enddate1
	print @enddate2
	--EXEC sp_monthToDate @month, @year, @date1, @date2
	set @currentDate = CAST(@enddate1 AS DATE)
	set @firstDate =  CAST(@startdate1 AS DATE)
	set @currentDate2 = CAST(@enddate2 AS DATE)
	set @firstDate2 =  CAST(@startdate2 AS DATE)
	
		(SELECT CONCAT(DAY(ReportDate), '/',MONTH(ReportDate)) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_DAILY 
		where ReportDate < @currentDate and ReportDate >= @firstDate
		group by ReportDate)
		UNION
		(
		SELECT CONCAT(DAY(ReportDate), '/',MONTH(ReportDate)) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_DAILY 
		where ReportDate < @currentDate2 and ReportDate >= @firstDate2
		group by ReportDate
		)
End

GO

DROP PROCEDURE [dbo].[SP_GetReportData_Generality_Quaterly_Compare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_Quaterly_Compare]    Script Date: 12/18/2016 7:50:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_Quaterly_Compare] (@quarter1 int, @year1 int, @quarter2 int, @year2 int) As
Begin


	--
	declare @t1 int
	declare @t2 int
	declare @t3 int
	declare @t4 int
	declare @t5 int
	declare @t6 int
	declare @t int
	
	set @t1= @quarter1*3
	set @t2= @quarter1*3 - 1
	set @t3= @quarter1*3 - 2
	set @t4= @quarter2*3
	set @t5= @quarter2*3 - 1
	set @t6= @quarter2*3 - 2
	set @t= abs(@quarter2 - @quarter1)*3
	--EXEC sp_monthToDate @month, @year, @date1, @date2

	if(@quarter1 < @quarter2)
	Begin
		(SELECT CONCAT(ReportMonth, '-', @quarter1,'/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY
		where ReportMonth in (@t1, @t2, @t3) AND ReportYear = @year1
		group by ReportMonth, ReportYear)
		UNION
		(
		SELECT CONCAT(ReportMonth - @t,'-', @quarter2 , '/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where ReportMonth in (@t4, @t5, @t6) AND ReportYear = @year2
		group by ReportMonth, ReportYear
		)
	End
	else
	Begin
		(SELECT CONCAT(ReportMonth, '-', @quarter2,'/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY
		
		where ReportMonth in (@t4, @t5, @t6) AND ReportYear = @year2
		group by ReportMonth, ReportYear)
		UNION
		(
		SELECT CONCAT(ReportMonth - @t,'-', @quarter1 , '/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where ReportMonth in (@t1, @t2, @t3) AND ReportYear = @year1
		group by ReportMonth, ReportYear
		)
	End
End

GO


DROP PROCEDURE [dbo].[SP_GetReportData_Generality_Yearly_Compare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_Yearly_Compare]    Script Date: 12/18/2016 7:51:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_Yearly_Compare] (@year1 int, @year2 int) As
Begin


	--EXEC sp_monthToDate @month, @year, @date1, @date2

	
		(SELECT CONCAT(ReportMonth, '/', ReportYear) as Name,ReportMonth, ReportYear,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where ReportYear = @year1
		group by ReportMonth,ReportYear
		)
		UNION
		(
		SELECT CONCAT(ReportMonth, '/', ReportYear) as Name,ReportMonth,ReportYear,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where ReportYear = @year2
		group by ReportMonth,ReportYear
		
		)
		Order by ReportMonth, ReportYear
		
End
GO


-- Phan he AGENT:
DROP PROCEDURE [dbo].[SP_GetReportData_Generality_compare_AGENT]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_compare_AGENT]    Script Date: 12/18/2016 7:51:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_compare_AGENT] (@month1 int, @year1 int, @month2 int, @year2 int, @agentCode varchar(10)) As
Begin
	declare @currentDate date
	declare @firstDate date
	declare @currentDate2 date
	declare @firstDate2 date
	declare @startdate1 varchar(10)
	declare @enddate1 varchar(10)
	declare @startdate2 varchar(10)
	declare @enddate2 varchar(10)
	set @startdate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-01';
	set @startdate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-01'; 
	
	If (@month1 = 1 OR @month1= 3 OR @month1= 5 OR @month1 = 7 OR @month1 = 8 OR @month1 = 10 OR @month1 = 12)
		BEGIN
			set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-31'; 
		END
	ELSE
		IF (@month1 = 4 OR @month1 = 6 OR @month1 =9 OR @month1 = 11)
			set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-30'; 
		ELSE
			BEGIN
				IF(@year1 % 400 = 0)
					set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-29';
				ELSE
					set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-28';
			END

			If (@month2 = 1 OR @month2= 3 OR @month2= 5 OR @month2 = 7 OR @month2 = 8 OR @month2 = 10 OR @month2 = 12)
		BEGIN
			set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-31'; 
		END
	ELSE
		IF (@month2 = 4 OR @month2= 6 OR @month2 =9 OR @month2 = 11)
			set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-30'; 
		ELSE
			BEGIN
				IF(@year2 % 400 = 0)
					set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-29';
				ELSE
					set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-28';
			END

	print @enddate1
	print @enddate2
	--EXEC sp_monthToDate @month, @year, @date1, @date2
	set @currentDate = CAST(@enddate1 AS DATE)
	set @firstDate =  CAST(@startdate1 AS DATE)
	set @currentDate2 = CAST(@enddate2 AS DATE)
	set @firstDate2 =  CAST(@startdate2 AS DATE)
	
		(SELECT CONCAT(DAY(ReportDate), '/',MONTH(ReportDate)) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_DAILY 
		where AgentCode= @agentCode AND ReportDate < @currentDate and ReportDate >= @firstDate 
		group by ReportDate)
		UNION
		(
		SELECT CONCAT(DAY(ReportDate), '/',MONTH(ReportDate)) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_DAILY 
		where AgentCode= @agentCode AND ReportDate < @currentDate2 and ReportDate >= @firstDate2
		group by ReportDate
		)
End

GO


DROP PROCEDURE [dbo].[SP_GetReportData_Generality_Quaterly_Compare_AGENT]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_Quaterly_Compare_AGENT]    Script Date: 12/18/2016 7:51:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_Quaterly_Compare_AGENT] (@quarter1 int, @year1 int, @quarter2 int, @year2 int, @agentCode varchar(10)) As
Begin


	--
	declare @t1 int
	declare @t2 int
	declare @t3 int
	declare @t4 int
	declare @t5 int
	declare @t6 int
	declare @t int
	
	set @t1= @quarter1*3
	set @t2= @quarter1*3 - 1
	set @t3= @quarter1*3 - 2
	set @t4= @quarter2*3
	set @t5= @quarter2*3 - 1
	set @t6= @quarter2*3 - 2
	set @t= abs(@quarter2 - @quarter1)*3
	--EXEC sp_monthToDate @month, @year, @date1, @date2

	if(@quarter1 < @quarter2)
	Begin
		(SELECT CONCAT(ReportMonth, '-', @quarter1,'/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY
		where AgentCode= @agentCode AND ReportMonth in (@t1, @t2, @t3) AND ReportYear = @year1
		group by ReportMonth, ReportYear)
		UNION
		(
		SELECT CONCAT(ReportMonth - @t,'-', @quarter2 , '/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where AgentCode= @agentCode AND ReportMonth in (@t4, @t5, @t6) AND ReportYear = @year2
		group by ReportMonth, ReportYear
		)
	End
	else
	Begin
		(SELECT CONCAT(ReportMonth, '-', @quarter2,'/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY
		
		where AgentCode= @agentCode AND ReportMonth in (@t4, @t5, @t6) AND ReportYear = @year2
		group by ReportMonth, ReportYear)
		UNION
		(
		SELECT CONCAT(ReportMonth - @t,'-', @quarter1 , '/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where AgentCode= @agentCode AND ReportMonth in (@t1, @t2, @t3) AND ReportYear = @year1
		group by ReportMonth, ReportYear
		)
	End
End


GO


DROP PROCEDURE [dbo].[SP_GetReportData_Generality_Yearly_Compare_AGENT]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_Yearly_Compare_AGENT]    Script Date: 12/18/2016 7:52:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_Yearly_Compare_AGENT] (@year1 int, @year2 int, @agentCode varchar(10)) As
Begin


	--EXEC sp_monthToDate @month, @year, @date1, @date2

	
		(SELECT CONCAT(ReportMonth, '/', ReportYear) as Name,ReportMonth, ReportYear,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where AgentCode= @agentCode AND ReportYear = @year1
		group by ReportMonth,ReportYear
		)
		UNION
		(
		SELECT CONCAT(ReportMonth, '/', ReportYear) as Name,ReportMonth,ReportYear,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where AgentCode= @agentCode AND ReportYear = @year2
		group by ReportMonth,ReportYear
		
		)
		Order by ReportMonth, ReportYear
		
End
GO


-- Phan he Merchant:
DROP PROCEDURE [dbo].[SP_GetReportData_Generality_compare_Merchant]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_compare_Merchant]    Script Date: 12/18/2016 7:52:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_compare_Merchant] (@month1 int, @year1 int, @month2 int, @year2 int, @merchantCode varchar(10)) As
Begin
	declare @currentDate date
	declare @firstDate date
	declare @currentDate2 date
	declare @firstDate2 date
	declare @startdate1 varchar(10)
	declare @enddate1 varchar(10)
	declare @startdate2 varchar(10)
	declare @enddate2 varchar(10)
	set @startdate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-01';
	set @startdate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-01'; 
	
	If (@month1 = 1 OR @month1= 3 OR @month1= 5 OR @month1 = 7 OR @month1 = 8 OR @month1 = 10 OR @month1 = 12)
		BEGIN
			set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-31'; 
		END
	ELSE
		IF (@month1 = 4 OR @month1 = 6 OR @month1 =9 OR @month1 = 11)
			set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-30'; 
		ELSE
			BEGIN
				IF(@year1 % 400 = 0)
					set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-29';
				ELSE
					set @enddate1 = CAST(@year1 AS varchar(4)) + '-' + CAST(@month1 AS varchar(4)) + '-28';
			END

			If (@month2 = 1 OR @month2= 3 OR @month2= 5 OR @month2 = 7 OR @month2 = 8 OR @month2 = 10 OR @month2 = 12)
		BEGIN
			set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-31'; 
		END
	ELSE
		IF (@month2 = 4 OR @month2= 6 OR @month2 =9 OR @month2 = 11)
			set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-30'; 
		ELSE
			BEGIN
				IF(@year2 % 400 = 0)
					set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-29';
				ELSE
					set @enddate2 = CAST(@year2 AS varchar(4)) + '-' + CAST(@month2 AS varchar(4)) + '-28';
			END

	print @enddate1
	print @enddate2
	--EXEC sp_monthToDate @month, @year, @date1, @date2
	set @currentDate = CAST(@enddate1 AS DATE)
	set @firstDate =  CAST(@startdate1 AS DATE)
	set @currentDate2 = CAST(@enddate2 AS DATE)
	set @firstDate2 =  CAST(@startdate2 AS DATE)
	
		(SELECT CONCAT(DAY(ReportDate), '/',MONTH(ReportDate)) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_DAILY 
		where MerchantCode = @merchantCode AND ReportDate < @currentDate and ReportDate >= @firstDate 
		group by ReportDate)
		UNION
		(
		SELECT CONCAT(DAY(ReportDate), '/',MONTH(ReportDate)) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_DAILY 
		where MerchantCode = @merchantCode AND ReportDate < @currentDate2 and ReportDate >= @firstDate2
		group by ReportDate
		)
End

GO


DROP PROCEDURE [dbo].[SP_GetReportData_Generality_Quaterly_Compare_Merchant]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_Quaterly_Compare_Merchant]    Script Date: 12/18/2016 7:52:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_Quaterly_Compare_Merchant] (@quarter1 int, @year1 int, @quarter2 int, @year2 int, @merchantCode varchar(10)) As
Begin


	--
	declare @t1 int
	declare @t2 int
	declare @t3 int
	declare @t4 int
	declare @t5 int
	declare @t6 int
	declare @t int
	
	set @t1= @quarter1*3
	set @t2= @quarter1*3 - 1
	set @t3= @quarter1*3 - 2
	set @t4= @quarter2*3
	set @t5= @quarter2*3 - 1
	set @t6= @quarter2*3 - 2
	set @t= abs(@quarter2 - @quarter1)*3
	--EXEC sp_monthToDate @month, @year, @date1, @date2

	if(@quarter1 < @quarter2)
	Begin
		(SELECT CONCAT(ReportMonth, '-', @quarter1,'/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY
		where MerchantCode = @merchantCode AND ReportMonth in (@t1, @t2, @t3) AND ReportYear = @year1
		group by ReportMonth, ReportYear)
		UNION
		(
		SELECT CONCAT(ReportMonth - @t,'-', @quarter2 , '/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where MerchantCode = @merchantCode AND ReportMonth in (@t4, @t5, @t6) AND ReportYear = @year2
		group by ReportMonth, ReportYear
		)
	End
	else
	Begin
		(SELECT CONCAT(ReportMonth, '-', @quarter2,'/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY
		
		where MerchantCode = @merchantCode AND ReportMonth in (@t4, @t5, @t6) AND ReportYear = @year2
		group by ReportMonth, ReportYear)
		UNION
		(
		SELECT CONCAT(ReportMonth - @t,'-', @quarter1 , '/', ReportYear) as Name,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where MerchantCode = @merchantCode AND ReportMonth in (@t1, @t2, @t3) AND ReportYear = @year1
		group by ReportMonth, ReportYear
		)
	End
End

GO

DROP PROCEDURE [dbo].[SP_GetReportData_Generality_Yearly_Compare_Merchant]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetReportData_Generality_Yearly_Compare_Merchant]    Script Date: 12/18/2016 7:53:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_GetReportData_Generality_Yearly_Compare_Merchant] (@year1 int, @year2 int, @merchantCode varchar(10)) As
Begin


	--EXEC sp_monthToDate @month, @year, @date1, @date2

	
		(SELECT CONCAT(ReportMonth, '/', ReportYear) as Name,ReportMonth, ReportYear,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where MerchantCode = @merchantCode AND ReportYear = @year1
		group by ReportMonth,ReportYear
		)
		UNION
		(
		SELECT CONCAT(ReportMonth, '/', ReportYear) as Name,ReportMonth,ReportYear,
		 SUM(SaleAmount) AS Value, 0 - SUM( ReturnAmount) AS  ReturnAmount,  CAST(SUM( TransactionCount) AS BIGINT) AS  TransactionCount
		FROM MERCHANT_SUMMARY_MONTHLY 
		where MerchantCode = @merchantCode AND ReportYear = @year2
		group by ReportMonth,ReportYear
		
		)
		Order by ReportMonth, ReportYear
		
End
GO





