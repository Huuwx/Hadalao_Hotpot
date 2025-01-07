CREATE DATABASE QUANLYLAU1
USE QUANLYLAU1

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
SOLAN INT,
TINHTRANG NVARCHAR(200)  --KHACH QUEN || KHACH MOI
)
drop table KHACH
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

INSERT INTO NVQUAN
VALUES 
('NV001','Huong1','0383682951','DI LAM',2003),
('NV002','Huong2','0383682952','NGHI LAM',2002),
('NV003','Huong3','0383682953','DI LAM',2006),
('NV004','Huong4','0383682954','NGHI LAM',2001),
('NV005','Huong5','0383682955','DI LAM',2000),
('NV006','Huong6','0383682956','DI LAM',2002),
('NV007','Huong7','0383682957','NGHI LAM',2004),
('NV008','Huong8','0383682958','DI LAM',2001),
('NV009','Huong9','0383682959','NGHI LAM',2002);
select *from KHACH

drop trigger trg_PreventDuplicatePhone
drop trigger trg_PreventDelete
CREATE TRIGGER trg_PreventDuplicatePhone
ON KHACH
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM KHACH k
        JOIN INSERTED i ON k.SDT = i.SDT
    )
    BEGIN
        RAISERROR ('Số điện thoại đã tồn tại. Không thể thêm khách hàng mới.', 16, 1);
		rollback tran
    END
    ELSE
    BEGIN
        INSERT INTO KHACH (MAKH, TENKH, SDT, TUOI,SOLAN,TINHTRANG)
        SELECT MAKH, TENKH, SDT, TUOI,1,'KHACH MOI'
        FROM INSERTED;
		PRINT 'Thêm thành công!';
    END
END;
drop trigger trg_PreventDelete
CREATE TRIGGER trg_PreventDelete
ON KHACH
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM DELETED
        WHERE SOLAN >=5
    )
    BEGIN
        RAISERROR ('Không thể xóa khách hàng NAY.', 16, 1);
    END
    ELSE
    BEGIN
        DELETE FROM KHACH
        WHERE MAKH IN (SELECT MAKH FROM DELETED);
    END
END;
--view
CREATE VIEW View_KhachDuoi18 AS
SELECT 
    MAKH, 
    TENKH, 
    SDT, 
    TUOI,
	SOLAN,
	TINHTRANG
FROM 
    KHACH
WHERE 
    TUOI < 18;

DROP VIEW View_KhachDuoi18
CREATE VIEW View_KhachTren18 AS
SELECT 
    MAKH, 
    TENKH, 
    SDT, 
    TUOI,
	SOLAN,
	TINHTRANG
FROM 
    KHACH
WHERE 
    TUOI >= 18;
DROP VIEW View_KhachTren18
select *from View_KhachTren18

CREATE VIEW View_TimKiemKhachHang AS
SELECT 
    MAKH,
    TENKH,
    SDT,
    TUOI,
	SOLAN,
	TINHTRANG
FROM KHACH;
DROP VIEW View_TimKiemKhachHang

--proc

CREATE FUNCTION GetCustomerStatus (@SOLAN INT)
RETURNS NVARCHAR(200)
AS
BEGIN
    RETURN CASE 
               WHEN @SOLAN >= 2 THEN 'KHÁCH QUEN'
               ELSE 'KHÁCH MỜI'
           END;
END;

-- Stored procedure to add or update customer information
CREATE PROCEDURE AddKhachHangWithCondition
    @MAKH VARCHAR(20),
    @TENKH NVARCHAR(100),
    @SDT VARCHAR(10),
    @TUOI INT
AS
BEGIN
    -- Check if age is valid
    IF @TUOI <= 0
    BEGIN
        RAISERROR ('Tuổi phải lớn hơn 0.', 16, 1);
        RETURN;
    END;

    -- Check if customer already exists
    IF EXISTS (SELECT 1 FROM KHACH WHERE MAKH = @MAKH)
    BEGIN
        -- If customer exists, increment visit count and update status
        UPDATE KHACH
        SET 
            SOLAN = SOLAN + 1,
            TINHTRANG = dbo.GetCustomerStatus(SOLAN + 1)
        WHERE MAKH = @MAKH;

        RETURN;
    END;

    -- If customer does not exist, add new customer
    INSERT INTO KHACH (MAKH, TENKH, SDT, TUOI, SOLAN, TINHTRANG)
    VALUES (@MAKH, @TENKH, @SDT, @TUOI, 1, dbo.GetCustomerStatus(1));
END;
drop proc AddKhachHangWithCondition



---
CREATE FUNCTION dbo.CalculateLoyaltyPoints (@SOLAN INT)
RETURNS INT
AS
BEGIN
    DECLARE @Points INT;

    IF @SOLAN >= 5
        SET @Points = 15;
    ELSE
        SET @Points = 10;

    RETURN @Points; -- RETURN ở cuối hàm
END;

--proc
ALTER TABLE KHACH ADD DIEM INT DEFAULT 0;
ALTER TABLE KHACH ADD Discount INT DEFAULT 0;
select *from KHACH
CREATE PROCEDURE dbo.UpdateCustomerPointsById
    @MAKH VARCHAR(20)  -- Thêm tham số để chỉ cập nhật khách hàng theo mã
AS
BEGIN
    SET NOCOUNT ON;
    -- Khai báo con trỏ
    DECLARE customer_cursor CURSOR FOR
        SELECT MAKH, SOLAN  -- Chọn mã khách hàng và số lần ghé thăm
        FROM KHACH
        WHERE MAKH = @MAKH; -- Lọc theo mã khách hàng
    -- Khai báo biến để lưu dữ liệu con trỏ
    DECLARE @CurrentMAKH VARCHAR(20);
    DECLARE @CurrentSOLAN INT;
    -- Mở con trỏ
    OPEN customer_cursor;
    -- Lấy dữ liệu đầu tiên
    FETCH NEXT FROM customer_cursor INTO @CurrentMAKH, @CurrentSOLAN;
    -- Vòng lặp qua các khách hàng
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Kiểm tra và cập nhật điểm cho khách hàng
        IF EXISTS (SELECT 1 FROM KHACH WHERE MAKH = @CurrentMAKH)
        BEGIN
            -- Cập nhật điểm cho khách hàng dựa trên số lần ghé thăm
            UPDATE KHACH
            SET DIEM = DIEM + dbo.CalculateLoyaltyPoints(@CurrentSOLAN)
            WHERE MAKH = @CurrentMAKH;

            PRINT 'Cập nhật điểm cho khách hàng ' + @CurrentMAKH + ' thành công.';
        END
        ELSE
        BEGIN
            PRINT 'Mã khách hàng ' + @CurrentMAKH + ' không tồn tại.';
        END;
        -- Lấy dữ liệu tiếp theo từ con trỏ
        FETCH NEXT FROM customer_cursor INTO @CurrentMAKH, @CurrentSOLAN;
    END;
    -- Đóng con trỏ và giải phóng tài nguyên
    CLOSE customer_cursor;
    DEALLOCATE customer_cursor;
END;
drop proc dbo.UpdateCustomerPointsById
EXEC dbo.UpdateAllCustomerPoints;

--Dùng con trỏ để liệt kê ra danh sách những khách hàng có số lần vào thăm nhiều nhất
declare SOLANMAX cursor scroll
for
select TENKH,SOLAN from KHACH
open SOLANMAX

declare @tenkh nvarchar(200), @solan int, @max int;
set @max = 0;
fetch first from SOLANMAX into @tenkh, @solan;
while (@@FETCH_STATUS = 0)
begin
	if(@solan > @max)
		set @max = @solan
	print 'Khach '+@tenkh +'ghe' + cast(@solan as char(5))
	fetch next from SOLANMAX into @tenkh, @solan
end
print 'Số lần qua nhiều nhất' + cast(@max as char(5))
print 'Khach ghe qua nhieu nhat'
fetch first from SOLANMAX into @tenkh, @solan
while (@@FETCH_STATUS = 0)
begin	
	if(@solan = @max)
		print @tenkh + ','
		fetch next from SOLANMAX into @tenkh, @solan
end
close SOLANMAX;
deallocate SOLANMAX;

