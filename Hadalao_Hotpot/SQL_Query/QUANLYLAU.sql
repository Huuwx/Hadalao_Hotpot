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
    food_id INT PRIMARY KEY,
    food_name nvarchar(100) not null,
	food_price decimal not null,
	food_availability nvarchar(20) not null
);

CREATE TABLE BAN
(MABAN nvarchar(20) PRIMARY KEY,
TENBAN VARCHAR(20) NOT NULL,
TTBAN VARCHAR(20),
)

insert into BAN values('Bàn 1', 'ban 1', 'ok');

CREATE TABLE KHACH
(MAKH VARCHAR(20) NOT NULL PRIMARY KEY,
TENKH VARCHAR(20) NOT NULL,
SDT VARCHAR(10) NOT NULL,
TUOI INT,
)

create TABLE bill
(MAHD int identity(1, 1) PRIMARY KEY,
payment_time date,
table_code nvarchar(20) FOREIGN KEY REFERENCES BAN(MABAN),
customer_name nvarchar(50),
Total decimal
)

drop table BAN

drop table bill

drop table bill_info

select * from bill_info


INSERT INTO bill (payment_time, table_code, customer_name,total) VALUES (GETDATE(), @tableName, @emp_id, @customerName, @total);SELECT SCOPE_IDENTITY() AS LastInsertedID;

CREATE TABLE bill_info
(bill_id int FOREIGN KEY REFERENCES BILL(MAHD),
food_name nvarchar(50),
quantity int,
)

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

--CREATE FUNCTION fn_AverageFoodPrice()
--RETURNS DECIMAL(10, 2)
--AS
--BEGIN
--    RETURN (SELECT AVG(food_price) FROM FOOD);
--END;

--drop function fn_AverageFoodPrice

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

--Them view hien thi mon an duoc ban nhieu nhat

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