-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WarehouseDB')
CREATE DATABASE WarehouseDB;
GO

USE WarehouseDB
GO

--droping table
IF OBJECT_ID('tblProducts') IS NOT NULL 
DROP TABLE tblProducts;

--create table
CREATE TABLE tblProducts(
	ID INT PRIMARY KEY IDENTITY(1,1),
	ProductName VARCHAR(50) NOT NULL,
	ProductCode VARCHAR(10) NOT NULL,
	Quantity INT,
	Price INT NOT NULL,
	Stored VARCHAR(10)
	);
