CREATE DATABASE QUANLYLAU
USE QUANLYLAU

CREATE TABLE NVQUAN
(
MANV VARCHAR(20) NOT NULL PRIMARY KEY,
TENNV VARCHAR(20) NOT NULL,
SDT VARCHAR(10) NOT NULL,
TINHTRANG VARCHAR(20) NOT NULL, --NGHI || DI LAM--
NAMSINH INT,
)

CREATE TABLE food
(
    food_id INT IDENTITY(1,1) PRIMARY KEY,
    food_name nvarchar(100) not null,
	food_price decimal not null,
	food_availability nvarchar(20) not null
);



CREATE TABLE BAN
(MABAN varchar(20) PRIMARY KEY,
TENBAN VARCHAR(20) NOT NULL,
TTBAN VARCHAR(20),
)

drop table BAN

insert into BAN values('Bàn 1', 'ban 1', 'ok');

CREATE TABLE KHACH
(MAKH VARCHAR(20) NOT NULL PRIMARY KEY,
TENKH NVARCHAR(20) NOT NULL,
SDT VARCHAR(10) NOT NULL,
TUOI INT,
)

drop table KHACH

CREATE TABLE bill
(
    bill_id INT IDENTITY(1, 1) PRIMARY KEY,
    payment_time DATE,
    MABAN varchar(20) FOREIGN KEY REFERENCES BAN(MABAN),
    MAKH VARCHAR(20) FOREIGN KEY REFERENCES KHACH(MAKH),
    Total DECIMAL(10,2)	,
	bill_status nvarchar(50)
);
drop table bill
CREATE TABLE bill_info
(	
    bill_id INT,                                    
    food_id INT,                                     
    quantity INT,                                    
    FOREIGN KEY (bill_id) REFERENCES bill(bill_id),   
    FOREIGN KEY (food_id) REFERENCES food(food_id)   
);
select *from bill_info

drop table bill_info

drop table food

drop table KHACH

drop table BAN

drop table bill

drop table bill_info


INSERT INTO bill (payment_time, MABAN, customer_name,total) VALUES (GETDATE(), @tableName, @emp_id, @customerName, @total);SELECT SCOPE_IDENTITY() AS LastInsertedID;



INSERT INTO food (food_id, food_name, food_price, food_availability)
VALUES 
(11, 'Burger', 5, 'Available'),
(12, 'Salad', 8, 'Available'),
(13, 'Pasta', 12, 'Available'),
(14, 'Sandwich', 6, 'Available'),
(15, 'Soup', 4, 'Available'),
(16, 'Steak', 15, 'Available'),
(17, 'Sushi', 18, 'Available'),	
(18, 'Fries', 3, 'Available'),
(19, 'Ice Cream', 4, 'Available');

drop table food

select * from bill_info

--CHU MẠNH HỮU - 2251172368

--Trigger

--Trigger khong cho xoa mon available

--trigger cho insert food_availability auto la available

--trigger cho insert food id tu nhay

CREATE TRIGGER trg_CheckNegativePrice
ON FOOD
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (SELECT * FROM INSERTED WHERE food_price <= 0)
    BEGIN
        Print N'Giá món ăn không thể âm!';
        --ROLLBACK TRANSACTION;
    END
	ELSE
	BEGIN
		INSERT INTO FOOD (food_id, food_name, food_price, food_availability)
		SELECT food_id, food_name, food_price, 'Available' from inserted
	END
END;

drop trigger trg_CheckNegativePrice

create trigger trg_CheckDelete
on food
for delete
as
begin
	insert into food select food_id, food_name, food_price, food_availability from deleted
	where food_availability = 'Available'
end

drop trigger trg_CheckDelete
--Function

-- thay tinh gia trung binh food bang hien bang cac food unavailable

CREATE FUNCTION fn_TotalFoodByAvailability(@availability NVARCHAR(50))
RETURNS INT
AS
BEGIN
    RETURN (SELECT COUNT(*) FROM FOOD WHERE food_availability = @availability);
END;

drop function fn_TotalFoodByAvailability

Create function fn_UnavailableFood()
Returns table
as
	return (SELECT food_id, food_name, food_price, food_availability FROM FOOD
			Where food_availability = 'Unavailable')

drop function fn_UnavailableFood

--Proc

CREATE PROC pr_DeleteFoodById
    @food_id INT
AS
BEGIN
    DELETE FROM FOOD WHERE food_id = @food_id;
END;

drop proc pr_DeleteFoodById

CREATE PROC pr_SearchByName
@foodName NVARCHAR(50)
AS
BEGIN
    SELECT * FROM FOOD WHERE food_name = @foodName;
END;

drop proc pr_SearchByName

create proc pr_ThemMonAn
@food_name nvarchar(50), @food_price decimal
as begin
	declare @id int
	select @id = Count(food_id) from food 
	if(@id >= 1)
	begin
		Insert into food(food_id ,food_name, food_price, food_availability) values
		(@id + 1, @food_name, @food_price, 'Available')
	end
	else
	begin
		Insert into food(food_id ,food_name, food_price, food_availability) values
		(1, @food_name, @food_price, 'Available')
	end
	end

drop proc pr_ThemMonAn

--View
CREATE VIEW vw_FoodDetails
AS
SELECT food_id, food_name, food_price, food_availability FROM FOOD;

drop view vw_FoodDetails

CREATE VIEW vw_AvailableFood
AS
SELECT food_id, food_name, food_price, food_availability FROM FOOD
Where food_availability = 'Available'

drop view vw_AvailableFood
--Cursor

-- max price tra ve table, hien thi ten va gia cua mon dat nhat 

-- Tao trigger cap nhat lai id mon an cho food khi xoa

create function fn_MaxPriceByCursor()
RETURNS @ResultTable TABLE
(
	food_id int,
    food_name NVARCHAR(100),
    food_price DECIMAL(10, 2),
	food_availability nvarchar(20)
)
as
begin
	DECLARE cur_MaxPrice CURSOR SCROLL FOR
	SELECT food_name, food_price FROM FOOD;

	OPEN cur_MaxPrice;

	DECLARE @food_name NVARCHAR(100), @food_price DECIMAL(10,2), @maxprice DECIMAL(10,2);
	FETCH FIRST FROM cur_MaxPrice INTO @food_name, @food_price;
	set @maxprice = @food_price

	WHILE @@FETCH_STATUS = 0
	BEGIN
		if(@food_price > @maxprice)
		begin
			set @maxprice = @food_price
		end
		FETCH NEXT FROM cur_MaxPrice INTO @food_name, @food_price;
	END;

	CLOSE cur_MaxPrice;
	DEALLOCATE cur_MaxPrice;
	INSERT INTO @ResultTable (food_id, food_name, food_price, food_availability)
    select food_id, food_name, food_price, food_availability from food where food_price = @maxprice

	return;
end

drop function fn_MaxPriceByCursor

create trigger trg_UpdateIdAfterDelete
on food
for delete
as
begin
	if((select food_availability from deleted) = 'Unavailable')
	begin
		DECLARE @new_id INT;
		set @new_id = 1;
		DECLARE @current_id INT;

		-- Khai báo con trỏ để lấy danh sách các ID còn lại theo thứ tự tăng dần
		DECLARE cur CURSOR SCROLL
		FOR 
		SELECT food_id FROM food ORDER BY food_id;

		-- Mở con trỏ
		OPEN cur;

		-- Lặp qua con trỏ
		FETCH FIRST FROM cur INTO @current_id; -- Lấy bản ghi đầu tiên
		WHILE (@@FETCH_STATUS = 0)
		Begin
			-- Cập nhật ID của món ăn
			UPDATE food
			SET food_id = @new_id
			WHERE food_id = @current_id;

			-- Tăng giá trị ID mới
			SET @new_id = @new_id + 1;

			-- Lấy bản ghi tiếp theo
			FETCH NEXT FROM cur INTO @current_id;
		END

		-- Đóng con trỏ
		CLOSE cur;
		DEALLOCATE cur;
	end
end

drop trigger trg_UpdateIdAfterDelete

select * from food

--VŨ ĐĂNG HƯỞNG

--VŨ QUANG KHẢI

--1.1function tổng thu nhập của Haladao
create FUNCTION fn_TongThuNhap()
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @total DECIMAL(10, 2) = 0
    DECLARE @bill_total DECIMAL(10, 2)
    
    DECLARE cursor_bill CURSOR FOR
    SELECT total FROM bill WHERE bill_status = N'Đã thanh toán'
    
    OPEN cursor_bill
    FETCH NEXT FROM cursor_bill INTO @bill_total
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @total = @total + ISNULL(@bill_total, 0)
        FETCH NEXT FROM cursor_bill INTO @bill_total
    END
    
    CLOSE cursor_bill
    DEALLOCATE cursor_bill
    
    RETURN @total
END;
SELECT dbo.fn_TongThuNhap()

--1.2function thanh toán hóa đơn
create FUNCTION fn_Tongbillthanhtoan (@bill_id INT)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @total DECIMAL(10, 2) = 0;
    DECLARE @food_price DECIMAL(10, 2);
    DECLARE @quantity INT;

    DECLARE bill_cursor CURSOR FOR
        SELECT food.food_price, bill_info.quantity
        FROM bill_info
        INNER JOIN food ON food.food_id = bill_info.food_id
        WHERE bill_info.bill_id = @bill_id;

    OPEN bill_cursor;

    -- Lấy từng dòng dữ liệu và tính tổng tiền
    FETCH NEXT FROM bill_cursor INTO @food_price, @quantity;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @total = @total + (@food_price * @quantity);
        FETCH NEXT FROM bill_cursor INTO @food_price, @quantity;
    END


    CLOSE bill_cursor;
    DEALLOCATE bill_cursor;


    RETURN @total;
END;

select fn_Tongbillthanhtoan(1);

select *from bill_info
create trigger 

--3.1 view tất cả hóa đơn đã thanh toán
select *from bill
create view vw_Billall AS
select *from bill
SELECT * FROM vw_Billall;
drop view vw_Billall
select *from bill
select *from vw_ga
select DISTINCT  bill.bill_id, bill.payment_time, bill.MABAN, bill.MAKH, bill.total , bill.bill_status FROM bill
create view vw_Bill_dathanhtoan AS
select *from bill
where bill.bill_status = N'Đã thanh toán'
SELECT * FROM vw_Bill_dathanhtoan;

create view vw_Bill_chuathanhtoan AS
select *from bill
where bill.bill_status = N'Chưa thanh toán'
SELECT * FROM vw_Bill_chuathanhtoan;



select *from bill_info


select *from bill where bill_status = N'Đã thanh toán'
--3.2 view hóa đơn chi tiết
create VIEW vw_CustomerBills
AS
SELECT food.food_name , bill_info.quantity , (bill_info.quantity*food_price) as Price from bill_info
INNER JOIN food ON bill_info.food_id = food.food_id
SELECT * FROM vw_CustomerBills

drop view vw_BillDetails

select * from bill where bill_id = 4;
EXEC pr_AddBill '2025-01-02', 'B1', 'KH001';
 
 
--2.1 procedure thêm total
 CREATE PROCEDURE pr_UpdateBillTotal
    @bill_id INT
AS
BEGIN
    DECLARE @total DECIMAL(10, 2);
    SET @total = dbo.fn_Tongbillthanhtoan(@bill_id);
    UPDATE bill
    SET total = @total
    WHERE bill_id = @bill_id;
END;
EXEC pr_UpdateBillTotal @bill_id = 4; 
select *from bill
select *from bill_info
--2.2 procedure thêm đồ ăn vào trong hóa đơn
create PROCEDURE pr_AddFoodToBill
    @bill_id INT,
    @food_id INT,
    @quantity INT
AS
BEGIN
    INSERT INTO bill_info (bill_id, food_id, quantity)
    VALUES (@bill_id, @food_id, @quantity);
    DECLARE @total DECIMAL(10, 2)
    SET @total = dbo.fn_Tongbillthanhtoan(@bill_id)
    UPDATE bill
    SET total = @total
    WHERE bill_id = @bill_id;
END;
drop 
exec pr_AddFoodToBill @bill_id =1 ,@food_id =3 , @quantity =4
--2.3 proc
CREATE PROCEDURE pr_GetTongThuNhap
AS
BEGIN
    DECLARE @tongThuNhap DECIMAL(10, 2);
    SET @tongThuNhap = dbo.fn_TongThuNhap();
    SELECT @tongThuNhap AS TongThuNhap;
END;
EXEC pr_GetTongThuNhap;

--4.2trigger update thay doi bill_ìno
CREATE TRIGGER trg_UpdateTotal
ON bill_info
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @bill_id INT;
    SELECT @bill_id = bill_id FROM inserted;
    DECLARE @total DECIMAL(10, 2);
    SET @total = dbo.fn_Tongbillthanhtoan(@bill_id);
    UPDATE bill
    SET total = @total
    WHERE bill_id = @bill_id;
END;

create trigger trig_quantity
on bill_info
for insert , update
as
begin
	if (select quantity from inserted ) < 1
	begin
	rollback transaction
	print'khong hop le'
	end
	end
drop trigger trig_quantity


	select * from bill
	delete from bill

create TRIGGER trig_deletebill
ON bill
FOR DELETE
AS
BEGIN
    DECLARE @bill_id INT;
    SELECT @bill_id = bill_id FROM DELETED;

    -- Kiểm tra nếu hóa đơn đã được thanh toán, không cho phép xóa
    IF (SELECT bill_status FROM DELETED) = N'Đã thanh toán'
    BEGIN
        PRINT 'Không thể xóa hóa đơn đã thanh toán';
        ROLLBACK TRANSACTION; -- Ngừng thao tác nếu hóa đơn đã thanh toán
    END
    ELSE
    BEGIN
        -- Xóa dữ liệu liên quan trong bảng bill_info và bill
        DELETE FROM bill_info WHERE bill_id = @bill_id;
        DELETE FROM bill WHERE bill_id = @bill_id;
    END
END;


select *from bill


	select *from bill

	drop trigger trig_deletebill
	create trigger trig_bill_status
	on bill
	for insert
	as
	begin
	UPDATE bill
    SET bill.bill_status = N'Chưa thanh toán'
	FROM bill
	INNER JOIN inserted  ON bill.bill_id = inserted.bill_id;
    end


	select *from bill
drop trigger trg_UpdateTotal

CREATE TRIGGER trg_UpdateTongThuNhap
ON bill
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
    DECLARE @tongThuNhap DECIMAL(10, 2);
    SET @tongThuNhap = dbo.fn_TongThuNhap();
END;


-- Chèn vào bảng NVQUAN
INSERT INTO NVQUAN (MANV, TENNV, SDT, TINHTRANG, NAMSINH)
VALUES 
('NV001', 'Nguyễn Văn A', '0123456789', 'DI LAM', 1990),
('NV002', 'Trần Thị B', '0987654321', 'NGHI', 1992),
('NV003', 'Lê Văn C', '0912345678', 'DI LAM', 1991);

-- Chèn vào bảng KHACH
INSERT INTO KHACH (MAKH, TENKH, SDT, TUOI)
VALUES 
('KH001', N'Nguyễn Thị D', '0934567890', 30),
('KH002', N'Vũ Minh E', '0901234567', 45),
('KH003', N'Phạm Hồng F', '0988765432', 25);
select MAKH from KHACH where TENKH = N'Phạm Hồng F'
-- Chèn vào bảng food
INSERT INTO food (food_name, food_price, food_availability)
VALUES 
('Burger', 5, 'Available'),
('Salad', 8, 'Available'),
('Pasta', 12, 'Available'),
('Sandwich', 6, 'Available'),
('Soup', 4, 'Available'),
('Steak', 15, 'Available'),
('Sushi', 18, 'Available'),
('Fries', 3, 'Available'),
('Ice Cream', 4, 'Available');

-- Chèn vào bảng BAN
INSERT INTO BAN (MABAN, TENBAN, TTBAN)
VALUES 
('B1', 'Ban 1', 'ok'),
('B2', 'Ban 2', 'ok'),
('B3', 'Ban 3', 'ok'),
('B4', 'Ban 4', 'ok'),
('B5', 'Ban 5', 'ok'),
('B6', 'Ban 6', 'ok'),
('B7', 'Ban 7', 'ok'),
('B8', 'Ban 8', 'ok'),
('B9', 'Ban 9', 'ok'),
('B10', 'Ban 10', 'ok'),
('B11', 'Ban 11', 'ok'),
('B12', 'Ban 12', 'ok'),
('B13', 'Ban 13', 'ok'),
('B14', 'Ban 14', 'ok'),
('B15', 'Ban 15', 'ok'),
('B16', 'Ban 16', 'ok');
DELETE FROM BAN;

-- Chèn vào bảng bill
INSERT INTO bill (payment_time, MABAN, MAKH , bill_status)
VALUES 
(GETDATE(), 'B1', 'KH001' ,N'Đã thanh toán'),
(GETDATE(), 'B2', 'KH002',N'Đã thanh toán'),
(GETDATE(), 'B3', 'KH003',N'Đã thanh toán');
drop table bill
use QUANLYLAU
INSERT INTO bill (payment_time, MABAN, MAKH)
VALUES 
('2020-01-01', 'B2', 'KH002' );

DELETE FROM bill;
-- Chèn vào bảng bill_info
INSERT INTO bill_info (bill_id, food_id, quantity)
VALUES 
(1, 1, 2),  -- Bill 1: 2 Burgers
(1, 2, 1),  -- Bill 1: 1 Salad
(2, 3, 3),  -- Bill 2: 3 Pastas 36
(2, 4, 2),  -- Bill 2: 2 Sandwiches 12
(3, 5, 1),  -- Bill 3: 1 Soup
(3, 6, 2),  -- Bill 3: 2 Steaks
(3, 7, 1);  -- Bill 3: 1 Sushi

INSERT INTO bill (payment_time, MABAN, MAKH , bill_status)
VALUES 
(GETDATE(), 'B16', 'KH001' ,N'Đã thanh toán')
select *from bill
INSERT INTO bill_info (bill_id, food_id, quantity)
VALUES 
(4, 1, 100)
VALUES 
(1, 1, -1);


select * from bill_info where bill_id = 18

select * from bill_info
SELECT dbo.fn_Tongbillthanhtoan(1) as total;

drop table NVQUAN

drop table KHACH
drop table food

drop table BAN

drop table bill

drop table bill_info

select *from bill

delete from bill where bill_id = 1
DELETE FROM bill_info WHERE bill_id = 22
select *from bill
select *from bill_info 

CREATE TRIGGER trg_UpdateTongThuNhap
ON bill
AFTER INSERT, DELETE
AS
BEGIN
    DECLARE @tongThuNhap DECIMAL(10, 2);

    SET @tongThuNhap = dbo.fn_TongThuNhap();
  
    
END;
drop trigger trg_UpdateTongThuNhap


select *from bill


SELECT * FROM bill WHERE bill_id = '3';

INSERT INTO bill_info (bill_id, food_id, quantity)
VALUES (1, 101, 2);

select *from bill
select *from vw_CustomerBills

SELECT * FROM vw_Billall

create view vw_bill as
select *from bill

drop view vw_bill