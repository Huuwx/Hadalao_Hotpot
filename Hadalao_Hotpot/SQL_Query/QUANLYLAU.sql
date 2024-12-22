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
(MABAN VARCHAR(20) NOT NULL PRIMARY KEY,
TENBAN VARCHAR(20) NOT NULL,
TTBAN VARCHAR(20),
)

CREATE TABLE KHACH
(MAKH VARCHAR(20) NOT NULL PRIMARY KEY,
TENKH VARCHAR(20) NOT NULL,
SDT VARCHAR(10) NOT NULL,
TUOI INT,
)

CREATE TABLE BILL
(MAHD VARCHAR(20) NOT NULL PRIMARY KEY,
TGTT DATE,
TTBILL VARCHAR(10) NOT NULL,
)

CREATE TABLE BILLINFO
(MAHD VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES BILL(MAHD),
MABAN VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES BAN(MABAN),
MAKH VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES KHACH(MAKH),
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

--VŨ ĐĂNG HƯỞNG

--VŨ QUANG KHẢI