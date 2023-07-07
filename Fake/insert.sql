USE myShop
GO
DELETE FROM OrderItems
GO
DELETE FROM Orders
GO
DELETE FROM ProductVariantStores
GO
DELETE FROM Warehouses
GO
DELETE FROM ProductVariants
GO
DELETE FROM Images
GO
DELETE FROM Ratings
GO
DELETE FROM Products
GO
DELETE FROM Categories
GO
DELETE FROM colors
GO
DELETE FROM Sales
GO
DELETE FROM Comments
GO
DELETE FROM Users
GO
DELETE FROM Addresses
GO

SET IDENTITY_INSERT Colors ON
GO
INSERT INTO Colors (Id, Name)
SELECT Id, Name  FROM OPENROWSET  (
    BULK 'D:/color.json', 
    SINGLE_NCLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id			int				'$.Id', 
            Name		Nvarchar(MAX)	'$.Name' 
        ) AS [Colors]
GO
SET IDENTITY_INSERT Colors OFF
GO

SET IDENTITY_INSERT Categories ON
GO
INSERT INTO Categories (Id, Name, Url, Gender, Type)
SELECT Id, Name, Url, Gender, Type  FROM OPENROWSET  (
    BULK 'D:/categories.json', 
    SINGLE_NCLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id			int					'$.Id', 
            Name		Nvarchar(MAX)		'$.Name' ,
			Url			Nvarchar(MAX)		'$.URL',
			Gender  	Nvarchar(MAX)		'$.Gender',
			Type		Nvarchar(MAX)		'$.Type'
        ) AS [Categories]
GO
SET IDENTITY_INSERT Categories OFF
GO

SET IDENTITY_INSERT Products ON
GO
INSERT INTO Products (Id, Title, Description, Overview, Material, CategoryId, BrandId)
SELECT Id, Title, Description, Overview, Material, CategoryId, BrandId  FROM OPENROWSET  (
    BULK 'D:/product.json', 
    SINGLE_NCLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id				int				'$.Id', 
            Title			Nvarchar(MAX)	'$.title' ,
			Description		Nvarchar(MAX)	'$.description',
			Overview		Nvarchar(MAX)	'$.overview' ,
			Material		Nvarchar(MAX)	'$.material',
			CategoryId		int				'$.categoryId',
			BrandId			int				'$.brandId'
        ) AS [Products]
GO
SET IDENTITY_INSERT Products OFF
GO

CREATE TABLE tmp
(
   ProductId int,
   Rating float
);
GO

INSERT INTO tmp (ProductId, Rating)
SELECT ProductId, Rating  FROM OPENROWSET  (
    BULK 'D:/ratingProduct.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            ProductId		int		'$.Id', 
            Rating			float	'$.Rating'
        )  AS [Rating]
GO

UPDATE      Products
SET         Rating = t2.Rating
FROM        Products t1
INNER JOIN  tmp t2 
ON          t1.Id = t2.ProductId
GO

DROP TABLE tmp
GO

INSERT INTO Ratings (UserId, ProductId, Rating)
SELECT UserId, ProductId, Rating  FROM OPENROWSET  (
    BULK 'D:/ratingUser.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            UserId			int		'$.UserId',
			ProductId		int		'$.ProductId',
            Rating			float	'$.Rating'
        )  AS [Rating]
GO

SET IDENTITY_INSERT ProductVariants ON
GO
INSERT INTO ProductVariants (Id, ProductId, ColorId, Price, OriginalPrice, Quantity, ProductSize)
SELECT Id, ProductId, ColorId, Price, OriginalPrice, Quantity, ProductSize  FROM OPENROWSET  (
    BULK 'D:/variants.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
        ProductId		int					'$.ProductId', 
        ColorId			int					'$.ColorId' ,
		Price			decimal(18,2)		'$.price',
		OriginalPrice	decimal(18,2)		'$.originalPrice',
		Quantity		int					'$.quantity',
		ProductSize		Nvarchar(5)			'$.productSize',
		Id				int					'$.Id'
        ) AS [ProductVariants]
GO
SET IDENTITY_INSERT ProductVariants OFF
GO

SET IDENTITY_INSERT Images ON
GO
INSERT INTO Images (Id, Url, ColorId, ProductId)
SELECT Id, Url, ColorId,ProductId  FROM OPENROWSET  (
    BULK 'D:/images.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id			int					'$.Id', 
			Url			Nvarchar(MAX)		'$.URL' ,
			ColorId		int					'$.ColorId',
			ProductId	int					'$.ProductId'
        ) AS [Images]
GO
SET IDENTITY_INSERT Images OFF
GO

SET DATEFORMAT DMY;
GO
INSERT INTO Sales (Date, Year, Totals)
SELECT Date, Year, Totals  FROM OPENROWSET  (
    BULK 'D:/sales.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Date		Date	'$.Date', 
            Year		Real	'$.Year', 
			Totals		Real	'$.Totals' 
        ) AS [Sales]
GO

SET IDENTITY_INSERT Addresses ON
GO
INSERT INTO Addresses (Id, WardId, Street)
SELECT Id, WardId, Street  FROM OPENROWSET  (
    BULK 'D:/addresses.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id			int				'$.Id',
			WardId		int				'$.wardId',
            Street		Nvarchar(MAX)	'$.street'
        )  AS [Addresses]
GO
SET IDENTITY_INSERT Addresses OFF
GO

SET IDENTITY_INSERT Users ON
GO
INSERT INTO Users (Id, Name, Email, Role, Phone, Weight, Height, AddressId)
SELECT Id, Name, Email, Role, Phone, Weight, Height, AddressId FROM OPENROWSET  (
    BULK 'D:/users.json', 
    SINGLE_NCLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id				int				'$.id', 
            Name			Nvarchar(MAX)	'$.name', 
			Email			Nvarchar(MAX)	'$.email',
			Role			int				'$.role',
			Phone			Nvarchar(MAX)	'$.phone', 
			Weight			int				'$.weight', 
			Height			int				'$.height',
			AddressId		int				'$.addressId'
        ) AS [Users]
GO
SET IDENTITY_INSERT Users OFF
GO

SET IDENTITY_INSERT Orders ON
GO
INSERT INTO Orders (Id, UserId, Status, AddressId, TotalPrice)
SELECT Id, UserId, Status, AddressId, TotalPrice FROM OPENROWSET  (
    BULK 'D:/order.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id				int			'$.Id', 
            UserId			int			'$.UserId', 
			Status			int			'$.Status',
			AddressId		int			'$.AddressId',	
			TotalPrice		int			'$.TotalPrice'
        ) AS [Orders]
GO
SET IDENTITY_INSERT Orders OFF
GO

INSERT INTO OrderItems (OrderId, ProductId, ProductVariantId, Quantity, TotalPrice)
SELECT OrderId, ProductId, ProductVariantId, Quantity, TotalPrice FROM OPENROWSET  (
    BULK 'D:/orderItem.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            OrderId				int			'$.OrderId', 
			ProductId			int			'$.ProductId', 
            ProductVariantId	int			'$.ProductVariantId', 
			Quantity			int			'$.Quantity',
			TotalPrice			int			'$.TotalPrice'
        ) AS [OrderItems]
GO

-- Set default password is 'string'
UPDATE Users
SET PasswordHash = CONVERT(VARBINARY(max), '0x916345A0A2CE77E985CECB7CEA20298B7A443D66AD43C6CBB06A915376F204729D86EF18BA15561F1EB8ED1F8CC30D44E28BCDD4513D106A5782ECC1D29F8F8B', 1),
	PasswordSalt = CONVERT(VARBINARY(max), '0xF19B4CC587AB67449FC183AB984ABE4623EA0B572AE0EBAD592607F498CF60B909ED70A02B963BFFC63F336A9FF018A5BB73C217ECB88EC7F4643C309C20EC94931096031093F87D0244F6551F4ABE54DA0C581658A8642896BEF19240876114C3A9D0FCBFD98B74B5AA4415E0C2591156761563B8AF03C9B0C8E7E8DF238D60', 1)
GO

SET DATEFORMAT YMD;
GO

SET IDENTITY_INSERT Comments ON
GO
INSERT INTO Comments (Id, UserId, ProductVariantId, Rating, Content, CommentDate, ProductId, CreatedDate)
SELECT Id, UserId, ProductVariantId, Rating, Content, CommentDate, ProductId, CreatedDate FROM OPENROWSET  (
    BULK 'D:/comments.json', 
    SINGLE_NCLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
			Id					int				'$.Id', 
            UserId				int				'$.UserId', 
            ProductVariantId	int				'$.ProductVariantId', 
			Rating				Real			'$.Rating',
			Content				Nvarchar(MAX)	'$.Content',
			CommentDate			DateTime		'$.CommentDate',
			CreatedDate			DateTime		'$.CommentDate',
			ProductId			int				'$.ProductId'
        ) AS [Comments]
GO
SET IDENTITY_INSERT Comments OFF
GO

SET IDENTITY_INSERT Warehouses ON
GO
INSERT INTO Warehouses (Id, AddressId, Phone, Name)
SELECT Id, AddressId, Phone, Name FROM OPENROWSET  (
    BULK 'D:/warehouses.json', 
    SINGLE_NCLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
			Id			int				'$.id', 
			AddressId	int				'$.addressId',
			Phone		Nvarchar(MAX)	'$.phone',
			Name		Nvarchar(MAX)	'$.name'
        ) AS [Warehouses]
GO
SET IDENTITY_INSERT Warehouses OFF
GO

INSERT INTO ProductVariantStores (ProductVariantId, WarehouseId, BuyPrice, Quantity, DateInput, LotCode)
SELECT ProductVariantId, WarehouseId, BuyPrice, Quantity, DateInput, LotCode  FROM OPENROWSET  (
    BULK 'D:/store.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
        ProductVariantId	int					'$.ProductVariantId', 
        WarehouseId			int					'$.WarehouseId' ,
		BuyPrice			decimal(18,2)		'$.BuyPrice',
		Quantity			int					'$.Quantity',
		Stock				int					'$.Stock',
		DateInput			DateTime			'$.DateInput',
		LotCode				Varchar(MAX)		'$.LotCode'
        ) AS [ProductVariantStores]
GO

--Update comments
UPDATE comments
SET UserName = (select name from users where users.id=comments.UserId)
GO
--UPDATE comments
--SET ProductId = (select ProductId from productVariants where productVariants.Id=comments.ProductVariantId)
--GO
UPDATE comments
SET ProductTitle = (select products.Title from productVariants inner join products on productVariants.productId = products.id
		where productVariants.id=comments.ProductVariantId),
	ProductSize = (select ProductSize from productVariants 
		where productVariants.id=comments.ProductVariantId),
	ProductColor = (select colors.Name from productVariants inner join colors on productVariants.colorId = colors.id
		where productVariants.id=comments.ProductVariantId)
GO

--Update orders
UPDATE orders
SET Name = (select name from users where users.id=orders.UserId),
	Phone = (select phone from users where users.id=orders.UserId),
	IsPaid = 1
GO
UPDATE orders
SET OrderDate = (select createdDate from comments where orders.id=comments.Id),
	CreatedDate = (select createdDate from comments where orders.id=comments.Id)
GO

--Update orderItems
UPDATE orderItems
SET ProductSize = (select ProductSize from productVariants 
		where productVariants.id=orderItems.ProductVariantId),
	ProductColor = (select colors.Name from productVariants inner join colors on productVariants.colorId = colors.id
		where productVariants.id=orderItems.ProductVariantId),
	IsCommented = 1
GO