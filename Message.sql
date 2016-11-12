-------------------------------MESSAGE-------------------------------------

create proc sp_ReadMessage
	@MaCode varchar(10),
	@UserType char(1)
as
begin
	select *
	from MESSAGE M
	where (M.ReceiverType = @UserType and (M.Receiver = @MaCode)) or (M.ReceiverType = @UserType and (M.Receiver IS NULL))
	order by M.IsRead asc, M.DateSend desc
end
go

create proc sp_UpdateIsRead
	@ID varchar(10)
as
begin
	Update MESSAGE
	set IsRead = 1
	where ID = @ID
end
go

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
		INSERT INTO MESSAGE(Sender, SenderType, Receiver, ReceiverType, Message, DateSend) VALUES (@sender, @senderType, @receive, @receiveType, @message, @dateSend)
	END TRY
	BEGIN CATCH
		RAISERROR (N'Loi',16,1)
		RETURN
	END CATCH
END
go

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
create proc sp_GetMessageID
	@ID int
as
begin
	select *
	from MESSAGE M
	where M.ID = @ID
end
--------------------------------------------------------------------------------------