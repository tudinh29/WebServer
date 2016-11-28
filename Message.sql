-------------------------------MESSAGE-------------------------------------
/*
Version          Changer                Date               Detail         
1.0              Phạm Ngọc Thiện        07/11/2016         Đọc danh sách tin nhắn
*/
create proc sp_ReadMessage
	@MaCode varchar(10),
	@UserType char(1)
as
begin
	select *
	from MESSAGE M
	where (M.ReceiverType = @UserType and (M.Receiver = @MaCode)) or (M.ReceiverType = @UserType and (M.Receiver IS NULL))
	order by M.DateSend desc
end
go
/*
Version          Changer                Date               Detail         
1.0              Phạm Ngọc Thiện        07/11/2016         Cập nhật lại isread trong bản message từ false sang true
*/
create proc sp_UpdateIsRead
	@ID varchar(10)
as
begin
	Update MESSAGE
	set IsRead = 1
	where ID = @ID
end
go

/* 
Version			Changer				Date			Detail
1.0				Phạm Ngọc Thiện		26/10/2016		Thêm vào bản message	
1.1				Phạm Ngọc Thiện		28/11/2016		Thêm isread là false vào														
*/

CREATE PROC sp_InsertMessage
	@sender VARCHAR(10),
	@senderType CHAR(1),
	@receive VARCHAR(10),
	@receiveType CHAR(1),
	@message TEXT
AS
BEGIN
	BEGIN TRY
	DECLARE @dateSend DATETIME
		SET @dateSend = GETDATE()
		INSERT INTO MESSAGE(Sender, SenderType, Receiver, ReceiverType, Message, DateSend, IsRead) VALUES (@sender, @senderType, @receive, @receiveType, @message, @dateSend,0)
	END TRY
	BEGIN CATCH
		RAISERROR (N'Loi',16,1)
		RETURN
	END CATCH
END
go

/* 
Version			Changer				Date			Detail
1.0				Phạm Ngọc Thiện		07/11/2016		Đọc danh sách các tin nhắn đã gửi đi													
*/

create proc sp_MessageSent
	@sender varchar(10)
as
begin
	select *
	from MESSAGE M
	where M.Sender = @sender
	order by M.DateSend desc	
end

go

/* 
Version			Changer				Date			Detail
1.0				Phạm Ngọc Thiện		07/11/2016		Tìm tin nhắn theo ID													
*/
create proc sp_GetMessageID
	@ID int
as
begin
	select *
	from MESSAGE M
	where M.ID = @ID
end
go
/* 
Version			Changer				Date			Detail
1.0				Hà Xuân Duy		19/11/2016			Đếm số lượng tin nhăn chưa đọc													
*/
CREATE PROC sp_CountUnreadMessenge @MaCode VARCHAR(10), @UserType CHAR(1)
AS
BEGIN
	SELECT COUNT(*) AS Number
	FROM MESSAGE M
	WHERE ((M.ReceiverType = @UserType and (M.Receiver = @MaCode)) or (M.ReceiverType = @UserType and (M.Receiver IS NULL))) AND M.IsRead = 0 AND M.Receiver IS NOT NULL;
END

--------------------------------------------------------------------------------------