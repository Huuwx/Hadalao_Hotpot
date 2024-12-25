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
	food_price int not null,
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

select * from bill_info

--CHU MẠNH HỮU - 2251172368

--Trigger
CREATE TRIGGER trg_CheckNegativePrice
ON FOOD
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM INSERTED WHERE food_price < 0)
    BEGIN
        Print N'Giá món ăn không thể âm!';
        ROLLBACK TRANSACTION;
    END
END;

drop trigger trg_CheckNegativePrice

CREATE TRIGGER trg_CheckSizeOfName
ON FOOD
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM INSERTED WHERE Len(food_name) > 20)
    BEGIN
        Print N'Tên món ăn quá dài! (Quá 20 kí tự)';
        ROLLBACK TRANSACTION;
    END
	ELSE
	BEGIN
		INSERT INTO FOOD (food_name, food_price, food_availability)
		SELECT food_name, food_price, food_availability from inserted
	END
END;

drop trigger trg_CheckSizeOfName
--Function
CREATE FUNCTION fn_TotalFoodByAvailability(@availability NVARCHAR(50))
RETURNS INT
AS
BEGIN
    RETURN (SELECT COUNT(*) FROM FOOD WHERE food_availability = @availability);
END;

drop function fn_TotalFoodByAvailability

CREATE FUNCTION fn_AverageFoodPrice()
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN (SELECT AVG(food_price) FROM FOOD);
END;

drop function fn_AverageFoodPrice

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
create function fn_MaxPriceByCursor()
returns DECIMAL(10,2)
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
	return @maxprice
end

drop function fn_MaxPriceByCursor

--VŨ ĐĂNG HƯỞNG

--VŨ QUANG KHẢI