CREATE SCHEMA PizzaStore
GO

CREATE TABLE PizzaStore.Users
(
	ID INT,
	FirstName NVARCHAR(128),
	LastName NVARCHAR(128),
	PrefLocation INT	
);

CREATE TABLE PizzaStore.Locations
(
	StoreNumber INT
);

CREATE TABLE PizzaStore.Orders
(
	OrderID INT,
	StoreNumber INT,
	TotalPizzas INT,	
	Onion BIT,
	Pepper BIT,
	Pineapple BIT,
	Ham BIT,
	Chicken BIT,
	Sausage BIT,
	BBQChicken BIT,
	Pepperoni BIT,
	Price MONEY,
	TimeOfOrder DATETIME
);

CREATE TABLE PizzaStore.Inventory
(
	InventoryID INT,
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
)



ALTER TABLE PizzaStore.Locations
ADD CONSTRAINT PK_Locations_StoreNumber PRIMARY KEY (StoreNumber)
GO

ALTER TABLE PizzaStore.Users
ADD CONSTRAINT FK_Users_PrefLocation FOREIGN KEY (PrefLocation) REFERENCES PizzaShop.Locations(StoreNumber)
GO

ALTER TABLE PizzaStore.Users
ADD CONSTRAINT PK_Users_ID PRIMARY KEY (ID);
GO

ALTER TABLE PizzaStore.Orders
ADD CONSTRAINT PK_Orders_OrderID PRIMARY KEY (OrderID)
GO

ALTER TABLE PizzaStore.Orders
ADD CONSTRAINT FK_Order_StoreNumber FOREIGN KEY (StoreNumber) REFERENCES PizzaShop.Location(StoreNumber)
GO





