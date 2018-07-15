--CREATE SCHEMA PizzaStore
--GO

--DROP TABLE PizzaStore.Users
CREATE TABLE PizzaStore.Users
(
	ID INT IDENTITY NOT NULL,
	FirstName NVARCHAR(128) NOT NULL,
	LastName NVARCHAR(128) NOT NULL,
	PrefLocation INT NOT NULL
);

--DROP TABLE PizzaStore.Locations
CREATE TABLE PizzaStore.Locations
(
	StoreNumber INT IDENTITY NOT NULL,
	Dough INT NOT NULL,
	Cheese INT NOT NULL,
	Sauce INT NOT NULL,
	Onion INT NOT NULL,
	Pepper INT NOT NULL,
	Pineapple INT NOT NULL,
	Ham INT NOT NULL,
	Chicken INT NOT NULL,
	Sausage INT NOT NULL,
	BBQChicken INT NOT NULL,
	Pepperoni INT NOT NULL
);

--DROP TABLE PizzaStore.Orders
CREATE TABLE PizzaStore.Orders
(
	OrderID INT IDENTITY NOT NULL,
	UserID INT NOT NULL,
	StoreNumber INT NOT NULL,
	TotalPizzas INT NOT NULL,	
	Price MONEY NOT NULL,
	TimeOfOrder DATETIME NOT NULL
);

--DROP TABLE PizzaStore.PizzaPie
CREATE TABLE PizzaStore.PizzaPie
(
	ID INT IDENTITY NOT NULL,
	OrderID INT NOT NULL,
	Onion BIT NOT NULL,
	Pepper BIT NOT NULL,
	Pineapple BIT NOT NULL,
	Ham BIT NOT NULL,
	Chicken BIT NOT NULL,
	Sausage BIT NOT NULL,
	BBQChicken BIT NOT NULL,
	Pepperoni BIT NOT NULL,
);


--DROP TABLE PizzaStore.Inventory
--CREATE TABLE PizzaStore.Inventory
--(
--	ID INT NOT NULL,
--	InventoryID INT IDENTITY NOT NULL,
--	Dough INT NOT NULL,
--	Cheese INT NOT NULL,
--	Sauce INT NOT NULL,
--	Onion INT NOT NULL,
--	Pepper INT NOT NULL,
--	Pineapple INT NOT NULL,
--	Ham INT NOT NULL,
--	Chicken INT NOT NULL,
--	Sausage INT NOT NULL,
--	BBQChicken INT NOT NULL,
--	Pepperoni INT NOT NULL
--);



--ALTER TABLE PizzaStore.Inventory
--ADD CONSTRAINT PK_Inventory_ID PRIMARY KEY (ID)
--GO

ALTER TABLE PizzaStore.Inventory
ADD CONSTRAINT FK_Inventory_InventoryID FOREIGN KEY (InventoryID) REFERENCES PizzaStore.Locations(StoreNumber)
GO


ALTER TABLE PizzaStore.Orders
ADD CONSTRAINT FK_Order_StoreNumber FOREIGN KEY (StoreNumber) REFERENCES PizzaStore.Locations(StoreNumber)
GO

ALTER TABLE PizzaStore.Orders
ADD CONSTRAINT FK_Orders_UserID FOREIGN KEY (UserID) REFERENCES PizzaStore.Users(ID)
GO

ALTER TABLE PizzaStore.PizzaPie
ADD CONSTRAINT FK_PizzaPie_OrderID FOREIGN KEY (OrderID) REFERENCES PizzaStore.Orders(OrderID)
GO


ALTER TABLE PizzaStore.Users
ADD CONSTRAINT FK_Users_PrefLocation FOREIGN KEY (PrefLocation) REFERENCES PizzaStore.Locations(StoreNumber)
GO

ALTER TABLE PizzaStore.Users
ADD CONSTRAINT PK_Users_ID PRIMARY KEY (ID);
GO

ALTER TABLE PizzaStore.Orders
ADD CONSTRAINT PK_Orders_OrderID PRIMARY KEY (OrderID)
GO

ALTER TABLE PizzaStore.PizzaPie
ADD CONSTRAINT PK_PizzaPie_ID PRIMARY KEY (ID)
GO

ALTER TABLE PizzaStore.Locations
ADD CONSTRAINT PK_Locations_StoreNumber PRIMARY KEY (StoreNumber)
GO

INSERT INTO PizzaStore.Users
VALUES('Joseph','Isble',2)

INSERT INTO PizzaStore.Locations
VALUES(1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000);

INSERT INTO PizzaStore.Locations
VALUES(1,1,1,1,1,1,1,1,1,1,1);

INSERT INTO PizzaStore.Locations
VALUES(50,100,20,500,234,454,12,566,135,135,516);

INSERT INTO PizzaStore.Locations
VALUES(5,123,633,789,346,262,16,153,156,151,774);

SELECT * FROM PizzaStore.Locations;

SELECT * FROM PizzaStore.Users;

SELECT * FROM PizzaStore.Orders;

SELECT * FROM PizzaStore.PizzaPie

DELETE FROM PizzaStore.Users
WHERE PizzaStore.Users.ID = 5

DELETE FROM PizzaStore.PizzaPie
WHERE PizzaStore.PizzaPie.ID =5;