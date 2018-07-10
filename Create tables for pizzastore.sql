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
	StoreNumber INT IDENTITY NOT NULL
);

--DROP TABLE PizzaStore.Orders
CREATE TABLE PizzaStore.Orders
(
	OrderID INT IDENTITY NOT NULL,
	UserID INT NOT NULL,
	StoreNumber INT NOT NULL,
	TotalPizzas INT NOT NULL,	
	Price MONEY,
	TimeOfOrder DATETIME
);

--DROP TABLE PizzaStore.PizzaPie
CREATE TABLE PizzaStore.PizzaPie
(
	ID INT NOT NULL,
	OrderID INT NOT NULL,
	Onion BIT,
	Pepper BIT,
	Pineapple BIT,
	Ham BIT,
	Chicken BIT,
	Sausage BIT,
	BBQChicken BIT,
	Pepperoni BIT,
);


--DROP TABLE PizzaStore.Inventory
CREATE TABLE PizzaStore.Inventory
(
	ID INT NOT NULL,
	InventoryID INT IDENTITY NOT NULL,
	Dough INT,
	Cheese INT,
	Sauce INT,
	Onion INT,
	Pepper INT,
	Pineapple INT,
	Ham INT,
	Chicken INT,
	Sausage INT,
	BBQChicken INT,
	Pepperoni INT
);



ALTER TABLE PizzaStore.Inventory
ADD CONSTRAINT PK_Inventory_ID PRIMARY KEY (ID)
GO

ALTER TABLE PizzaStore.PizzaPie
ADD CONSTRAINT PK_PizzaPie_ID PRIMARY KEY (ID)
GO

ALTER TABLE PizzaStore.Orders
ADD CONSTRAINT FK_Orders_UserID FOREIGN KEY (UserID) REFERENCES PizzaStore.Users(ID)
GO

ALTER TABLE PizzaStore.PizzaPie
ADD CONSTRAINT FK_PizzaPie_OrderID FOREIGN KEY (OrderID) REFERENCES PizzaStore.Orders(OrderID)
GO

ALTER TABLE PizzaStore.Locations
ADD CONSTRAINT PK_Locations_StoreNumber PRIMARY KEY (StoreNumber)
GO

ALTER TABLE PizzaStore.Inventory
ADD CONSTRAINT FK_Inventory_InventoryID FOREIGN KEY (InventoryID) REFERENCES PizzaStore.Locations(StoreNumber)
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

ALTER TABLE PizzaStore.Orders
ADD CONSTRAINT FK_Order_StoreNumber FOREIGN KEY (StoreNumber) REFERENCES PizzaStore.Locations(StoreNumber)
GO





