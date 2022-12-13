USE myShop
GO
DELETE FROM ProductVariants
GO
DELETE FROM Images
GO
DELETE FROM Products
GO
DELETE FROM Categories
GO
DELETE FROM colors
GO
DELETE FROM Sales
GO

SET IDENTITY_INSERT Colors ON
GO
INSERT INTO Colors (Id, Name)
SELECT Id, Name  FROM OPENROWSET  (
    BULK 'D:/color.json', 
    SINGLE_CLOB) AS [Json]    
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
    SINGLE_CLOB) AS [Json]    
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
INSERT INTO Products (Id, Title, Description, CategoryId, BrandId)
SELECT Id, Title, Description, CategoryId, BrandId  FROM OPENROWSET  (
    BULK 'D:/product.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id				int				'$.Id', 
            Title			Nvarchar(MAX)	'$.title' ,
			Description		Nvarchar(MAX)	'$.description',
			CategoryId		int				'$.categoryId',
			BrandId			int				'$.brandId'
        ) AS [Products]
GO
SET IDENTITY_INSERT Products OFF
GO

INSERT INTO ProductVariants (ProductId, ColorId, Price, OriginalPrice, Quantity, ProductSize)
SELECT ProductId, ColorId, Price, OriginalPrice, Quantity, ProductSize  FROM OPENROWSET  (
    BULK 'D:/variants.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
        ProductId		int					'$.ProductId', 
        ColorId			int					'$.ColorId' ,
		Price			decimal(18,2)		'$.price',
		OriginalPrice	decimal(18,2)		'$.originalPrice',
		Quantity		int					'$.quantity',
		ProductSize		Nvarchar(5)			'$.productSize'			
        ) AS [ProductVariants]
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
