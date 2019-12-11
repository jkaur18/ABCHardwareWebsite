Create Database ABCHardware

Drop Database ABCHardware

Use ABCHardware
go


Create Table Sale
(
	SaleNumber		Int identity(123456789,1)Primary key Not Null,
	CustomerID		Int Foreign Key references Customer(CustomerId) Not null,
	SalesPersonID	Int Foreign key references SalesPerson(SalesPersonID) Not Null,
	SaleDate		Date Not Null,
	Subtotal		Money,
	GST				Money,
	SaleTotal		Money
)

Create Table SalesDetail
(
	SaleNumber		Int Foreign Key references Sale(Salenumber) Not Null,
	ItemCode		Varchar(10) Foreign Key references Item(ItemCode) Not Null,
		Constraint	PK_SalesDetail Primary Key(SaleNumber,ItemCode),
	Quantity		Int Not NUll,
	ItemTotal		Money 
)

Create Table Item
(
	ItemCode		Varchar(10) Primary Key Not Null,
	[Description]	Varchar(200),
	UnitPrice		Money Not Null,
	QtyOnHand		int not null,
	Deleted			bit Default 0
)

Alter table Item Add Deleted bit Default 0
Alter table Item Drop Column Deleted
Select * from Item
Alter table Item
Drop Constraint df_deleted 
ALTER TABLE Item
ADD CONSTRAINT df_deleted
DEFAULT 0 for Deleted
with values


Create Table Customer
(
	CustomerID		Int identity(200,1) Primary Key Not Null,
	CustomerName	Varchar(50) Not Null,
	[Address]		Varchar(80),
	City			Varchar(30),
	Province		Varchar(8) Default 'Alberta',
	PostalCode		Varchar(7) Not Null Constraint CK_PCode check (PostalCode like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]'),
)

Create Table SalesPerson
(
	SalesPersonID	Int identity(300,1) Primary Key Not Null,
	SalesPersonName	Varchar(50) Not Null
)

Set Identity_Insert Sale On
Set Identity_Insert Customer Off
Set Identity_Insert SalesPerson off

Insert Into Sale ( CustomerID, SalesPersonID,  SaleDate, Subtotal, GST, SaleTotal)
	Values 
	(
		'200',
		'300',
		'01/16/2004',
		'115',
		'8.05',
		'123.05'
	)

Insert Into Customer(CustomerName, Address, City, Province, PostalCode)
	Values
	(		
		'John Smith',
		'12345 67 Street',
		'Edmonton',
		'Alberta',
		'T6T6T6'
	)

Insert Into SalesPerson (SalesPersonName)
	Values
	(		
		'Jenny Brooks'
	)

Insert Into Item (ItemCode, Description, UnitPrice,QtyOnHand)
	Values
	(
		'P87455',
		'Torcue Wrench',
		'75.00',
		20
	)

Insert Into SalesDetail (SaleNumber,ItemCode,Quantity,ItemTotal)
	Values
	(
		123456790,
		'P87455',
		'1',
		75.00
	)
	   	 


	Select Sale.SaleNumber,SalesPerson.SalesPersonName, Sale.SaleDate, Customer.CustomerName, Customer.Address, Customer.City, Customer.Province, Customer.PostalCode,
		Item.ItemCode, Item.Description, Item.UnitPrice, SalesDetail.Quantity, (Item.UnitPrice*SalesDetail.Quantity) as ItemTotal, 
		(
			Select sum(Item.UnitPrice*SalesDetail.Quantity)
			from SalesDetail
			inner join Item
			on SalesDetail.ItemCode = Item.ItemCode
			where SalesDetail.SaleNumber= '123456789'
		)as SubTotal,
		(
			Select cast((Sale.SubTotal*0.07) as decimal(10,2)) 
			from sale
			where sale.SaleNumber = '123456789'
		)as GST,
		(
			Select Subtotal+GST			
		)as SaleTotal
	From Customer 
	left Outer join
	Sale
	on Customer.CustomerID = Sale.CustomerID
	left outer join
	SalesDetail 
	on Sale.SaleNumber = SalesDetail.SaleNumber
	left outer join
	Item 
	on SalesDetail.ItemCode = Item.ItemCode
	left outer join
	SalesPerson 
	on Sale.SalesPersonID = SalesPerson.SalesPersonID


go
	Create or alter Procedure LookupSale(@SaleNumber int = Null)
AS
	Begin
	
	
	Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @SaleNumber IS NULL
		RAISERROR ('Lookup Sale - Required parameter: @SaleNumber',16,1)
	Else
		Begin
		Select Sale.SaleNumber,SalesPerson.SalesPersonName, Sale.SaleDate, Customer.CustomerName, Customer.Address, Customer.City, Customer.Province, Customer.PostalCode,
		Item.ItemCode, Item.Description, Item.UnitPrice, SalesDetail.Quantity, (Item.UnitPrice*SalesDetail.Quantity) as ItemTotal, 
		(
			Select sum(Item.UnitPrice*SalesDetail.Quantity)
			from SalesDetail
			inner join Item
			on SalesDetail.ItemCode = Item.ItemCode
			where SalesDetail.SaleNumber= @SaleNumber
		)as SubTotal,
		(
			Select cast((Sale.SubTotal*0.07) as decimal(10,2)) 
			from sale
			where sale.SaleNumber = @SaleNumber
		)as GST,
		(
			(Select sum(Item.UnitPrice*SalesDetail.Quantity) 
			from SalesDetail
			inner join Item
			on SalesDetail.ItemCode = Item.ItemCode
			where SalesDetail.SaleNumber= @SaleNumber )
			+
			(Select cast((Sale.SubTotal*0.07) as decimal(10,2)) 
			from sale
			where sale.SaleNumber = @SaleNumber)  
		)as SaleTotal
	From Customer 
	left Outer join
	Sale
	on Customer.CustomerID = Sale.CustomerID
	left outer join
	SalesDetail 
	on Sale.SaleNumber = SalesDetail.SaleNumber
	left outer join
	Item 
	on SalesDetail.ItemCode = Item.ItemCode
	left outer join
	SalesPerson 
	on Sale.SalesPersonID = SalesPerson.SalesPersonID
			If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('LookupSale - Select Error from Sale database',16,1)
		End
		Return @ReturnCode	
end

Execute LookupSale 123456789

Drop Procedure LookupSale

go
Create or Alter Procedure AddItem(@itemcode varchar(10), @description varchar(200), @unitprice money,@qtyoh int )
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @itemcode IS NULL
		RAISERROR ('Add Item - Required parameter: @itemcode',16,1)
	Else IF @description IS NULL
		RAISERROR ('Add Item - Required parameter: @description',16,1)
	Else If @unitprice IS NULL
		RAISERROR ('Add Item - Required parameter: @unitprice',16,1)
	Else If @qtyoh IS NULL
		RAISERROR ('Add Item - Required parameter: @qtyoh',16,1)
	Else	
	Begin
		insert into Item (ItemCode,Description,UnitPrice,QtyOnHand)
		Values (@itemcode,@description,@unitprice,@qtyoh)

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Add Item - Insert Error from Item database',16,1)		
		Return @ReturnCode	
	End
Exec AddItem 'S78695','Duct Tape', '6.97'

go
Create or Alter Procedure GetItem(@itemcode varchar(10))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @itemcode IS NULL
		RAISERROR ('Lookup Item - Required parameter: @itemcode',16,1)	
	Else	
	Begin
		Select ItemCode, Description, UnitPrice, Deleted from Item
			Where ItemCode = @itemcode

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Lookup Item - Lookup Error from Item database',16,1)		
		Return @ReturnCode	
	End
exec GetItem 'P75455'


go
Create or Alter Procedure UpdateItem(@itemcode varchar(10), @description varchar(200), @unitprice money )
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @itemcode IS NULL
		RAISERROR ('Update Item - Required parameter: @itemcode',16,1)
	Else IF @description IS NULL
		RAISERROR ('Update Item - Required parameter: @description',16,1)
	Else If @unitprice IS NULL
		RAISERROR ('Update Item - Required parameter: @unitprice',16,1)
	Else	
	Begin
		Update Item
		Set 
			Description = @description,
			UnitPrice = @unitprice
		Where ItemCode = @itemcode

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Update Item - Update Error from Item database',16,1)		
		Return @ReturnCode	
	End

Exec UpdateItem 'S78695', 'Duct Tape', '8.97'

go
Create or Alter Procedure DeleteItem(@itemcode varchar(10))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @itemcode IS NULL
		RAISERROR ('Delete Item - Required parameter: @itemcode',16,1)	
	Else	
	Begin
		Update Item
		Set 
			Deleted = 1
		Where ItemCode = @itemcode

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Delete Item - Delete Error from Item database',16,1)		
		Return @ReturnCode	
	End
exec DeleteItem 'P77455'
Select * from Item


go
Create or Alter Procedure AddCustomer(@customername varchar(50), @address Varchar(80), @city varchar(30), @province varchar(8), @postalcode varchar(7))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @customername IS NULL
		RAISERROR ('Add Customer - Required parameter: @customername',16,1)
	Else	
	Begin
		insert into Customer
		Values (@customername,@address, @city, @province, @postalcode)

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Add Customer - Insert Error from Customer database',16,1)		
		Return @ReturnCode	
	End
exec AddCustomer 'Dana Ardnt', '100 Meadowlark', 'Edmonton', 'AB', 'T5G2M6'

go
Create or Alter Procedure UpdateCustomer(@customerid int,@customername varchar(50), @address Varchar(80), @city varchar(30), @province varchar(8), @postalcode varchar(7))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @customerid IS NULL
		RAISERROR ('Update Customer - Required parameter: @customerid',16,1)
	Else	
	Begin
		Update Customer
		Set CustomerName = @customername,
			Address = @address,
			City = @city,
			Province = @province,
			PostalCode = @postalcode
		Where CustomerID = @customerid

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Update Customer - Update Error from Customer database',16,1)		
		Return @ReturnCode	
	End

exec UpdateCustomer '201', 'Nina Prinak', '100 Meadowlark', 'Edmonton', 'AB', 'T5G2M6' 

go
Create or Alter Procedure DeleteCustomer(@customerid int)
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @customerid IS NULL
		RAISERROR ('Delete Customer - Required parameter: @customerid',16,1)	
	Else	
	Begin
		Delete from Customer
		Where CustomerID = @customerid

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Delete Customer - Delete Error from Customer database',16,1)		
		Return @ReturnCode	
	End
Exec DeleteCustomer 201

go
Create or Alter Procedure GetCustomer(@customerid int)
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @customerid IS NULL
		RAISERROR ('Lookup Customer - Required parameter: @customerid',16,1)	
	Else	
	Begin
		Select CustomerID,CustomerName, Address, City, Province, PostalCode from Customer
			Where CustomerID = @customerid

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
				RaisError('Lookup Customer - Lookup Error from Customer database',16,1)		
		Return @ReturnCode	
	End

exec sp_columns Customer
Select * from Sale
Select * from SalesDetail
Select * from Item
Select * from Customer
Select * from SalesPerson

Drop Table Sale
Drop Table SalesDetail
Drop Table Item
Drop Table Customer
Drop Table SalesPerson




ALTER TABLE ITEM
add QtyOnHand int
go

create or alter procedure GetItems
as
begin
	Select * from item
end
go

exec GetItems
go

update item
set QtyOnHand = 2
where QtyOnHand is null
