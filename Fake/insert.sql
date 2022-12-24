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
DELETE FROM Comments
GO
DELETE FROM Users
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

CREATE TABLE tmp
(
   Id int,
   Rating float
);
GO

INSERT INTO tmp (Id, Rating)
SELECT Id, Rating  FROM OPENROWSET  (
    BULK 'D:/rating.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id				int		'$.Id', 
            Rating			float	'$.Rating'
        )  AS [Rating]
GO

UPDATE      Products
SET         Rating = t2.Rating
FROM        Products t1
INNER JOIN  tmp t2 
ON          t1.Id = t2.Id
GO

DROP TABLE tmp
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

SET IDENTITY_INSERT Users ON
GO
INSERT INTO Users (Id, Name, Email, Role)
SELECT Id, Name, Email, Role FROM OPENROWSET  (
    BULK 'D:/users.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id				int				'$.id', 
            Name			Nvarchar(MAX)	'$.name', 
			Email			Nvarchar(MAX)	'$.email',
			Role			int				'$.role'
        ) AS [Users]
GO
SET IDENTITY_INSERT Users OFF
GO

-- Set default password is 'string'
UPDATE Users
SET PasswordHash = CONVERT(VARBINARY(max), '0x916345A0A2CE77E985CECB7CEA20298B7A443D66AD43C6CBB06A915376F204729D86EF18BA15561F1EB8ED1F8CC30D44E28BCDD4513D106A5782ECC1D29F8F8B', 1),
	PasswordSalt = CONVERT(VARBINARY(max), '0xF19B4CC587AB67449FC183AB984ABE4623EA0B572AE0EBAD592607F498CF60B909ED70A02B963BFFC63F336A9FF018A5BB73C217ECB88EC7F4643C309C20EC94931096031093F87D0244F6551F4ABE54DA0C581658A8642896BEF19240876114C3A9D0FCBFD98B74B5AA4415E0C2591156761563B8AF03C9B0C8E7E8DF238D60', 1)
GO

SET DATEFORMAT YMD;
GO
INSERT INTO Comments (UserId, ProductVariantId, Rating, Content, CommentDate, ProductId, ProductTitle, UserName, ProductSize, ProductColor)
SELECT UserId, ProductVariantId, Rating, Content, CommentDate, ProductId, ProductTitle, UserName, ProductSize, ProductColor FROM OPENROWSET  (
    BULK 'D:/comments.json', 
    SINGLE_NCLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            UserId				int				'$.UserId', 
            ProductVariantId	int				'$.ProductVariantId', 
			Rating				Real			'$.Rating',
			Content				Nvarchar(MAX)	'$.Content',
			CommentDate			DateTime		'$.CommentDate',
			ProductId			int				'$.ProductId',
			ProductTitle		Nvarchar(MAX)	'$.Title',
			UserName			Nvarchar(MAX)	'$.UserName',
			ProductSize			Nvarchar(MAX)	'$.Size',
			ProductColor		Nvarchar(MAX)	'$.Color'
        ) AS [Comments]
GO

