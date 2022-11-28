DELETE FROM ProductVariants
GO
DELETE FROM Products
GO
DELETE FROM Categories
GO
DELETE FROM Images
GO
DELETE FROM ProductColors
GO
DELETE FROM colors
GO

INSERT INTO Colors(Id, Name)
SELECT Id, Name  FROM OPENROWSET  (
    BULK 'D:/color.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id     Nvarchar(450)     '$.Id', 
            Name   Nvarchar(MAX)     '$.Name' 
        ) AS [Colors]
GO

SET IDENTITY_INSERT ProductColors ON
GO
INSERT INTO ProductColors (ColorId,Id )
VALUES ('HTR GREY', 1), ('GOLD', 2), ('BLACK', 3), ('NAVY', 4), ('BROWN', 5), ('MOSS', 6), ('DK BLUE', 7), ('823P', 8), ('025A', 9), ('BL WHITE', 10), ('BLUE ATOLL', 11), ('ORANGE', 12), ('BL WHITE', 13), ('GREY HEATHER', 14), ('ORCHID', 15), ('BL WHITE', 16), ('ECKO NAVY', 17), ('SPRING', 18), ('BL WHITE', 19), ('GREY HEATHER', 20), ('SPRING', 21), ('BL WHITE', 22), ('ECKO NAVY', 23), ('VICTORIA BLUE', 24), ('ORCHID', 25), ('BL WHITE', 26), ('BLUE ATOLL', 27), ('ECKO NAVY', 28), ('BL WHITE', 29), ('ECKO NAVY', 30), ('ORCHID', 31), ('BEIGE', 32), ('GREY HEATHER', 33), ('GREY HEATHER', 34), ('GREY HEATHER', 35), ('NAVY', 36), ('NAVY', 37), ('BL WHITE', 38), ('R.ORANGE', 39), ('NAVY', 40), ('BRICK', 41), ('DK BLUE', 42), ('TRUE ECKO RED', 43), ('CLAY', 44), ('CLAY', 45), ('TEAL', 46), ('GOLD', 47), ('ROYAL BLUE', 48), ('GOLD', 49), ('RED', 50), ('MANGO', 51), ('MANGO', 52), ('BLUE ATOLL', 53), ('A15', 54), ('A804', 55), ('001Y', 56), ('781Y', 57), ('001B', 58), ('A1B', 59), ('001P', 60), ('CAMO', 61), ('CLAY', 62), ('SAGE', 63), ('BLACK', 64), ('BL WHITE', 65), ('ECKO NAVY', 66), ('GREY HEATHER', 67), ('BL WHITE', 68), ('GREY HEATHER', 69), ('VICTORIA BLUE', 70), ('BL WHITE', 71), ('ECKO NAVY', 72), ('GREY HEATHER', 73), ('ECKO NAVY', 74), ('GREY HEATHER', 75), ('VICTORIA BLUE', 76), ('BL WHITE', 77), ('ECKO NAVY', 78), ('VICTORIA BLUE', 79), ('BL WHITE', 80), ('ECKO NAVY', 81), ('VICTORIA BLUE', 82), ('BL WHITE', 83), ('ECKO NAVY', 84), ('ORCHID', 85), ('BL WHITE', 86), ('GREY HEATHER', 87), ('ORCHID', 88), ('CLASSIC BLUE', 89), ('BL WHITE', 90), ('CLASSIC BLUE', 91), ('GREY HEATHER', 92), ('MAGENTA PURPLE', 93), ('BL WHITE', 94), ('TIDEPOOL', 95), ('BL WHITE', 96), ('GREY HEATHER', 97), ('NAVY', 98), ('BLUE ATOLL', 99), ('ECKO NAVY', 100), ('GREY HEATHER', 101), ('AMAZON GREEN', 102), ('BLUE JAY', 103), ('GREY HEATHER', 104), ('BL WHITE', 105), ('ECKO NAVY', 106), ('MOSS', 107), ('MOSS', 108), ('WINE', 109), ('LT BLUE', 110), ('BLUE', 111), ('NAVY', 112), ('BLACK', 113), ('RUBY WINE', 114), ('BL WHITE', 115), ('ECKO NAVY', 116), ('SEAPORT', 117), ('BL WHITE', 118), ('BLACK', 119), ('IGUANA', 120), ('GREY HEATHER', 121), ('TRUE ECKO RED', 122), ('CHARCOAL', 123), ('BL WHITE', 124), ('TRUE ECKO RED', 125), ('A0A', 126), ('A0P', 127), ('A09', 128), ('A0F', 129), ('WI2', 130), ('001B', 131), ('BRW', 132), ('A0G', 133), ('A09', 134), ('A0F', 135), ('001B', 136), ('A1K', 137), ('GBG', 138), ('A0B', 139), ('AFR', 140), ('M14', 141), ('M14', 142), ('WI2', 143), ('A0Y', 144), ('A0B', 145), ('A07', 146), ('A0F', 147), ('BGG', 148), ('A06', 149), ('A04', 150), ('A0F', 151), ('A00', 152), ('A0K', 153), ('ALD', 154), ('A0J', 155), ('A0L', 156), ('A5K', 157), ('A5L', 158), ('A5M', 159), ('A5N', 160), ('F67', 161), ('A65', 162), ('A67', 163), ('ALP', 164), ('AL1', 165), ('AL3', 166), ('AL5', 167), ('AMAZON GREEN', 168), ('H03', 169), ('ANC', 170), ('B53', 171), ('A0S', 172), ('A00', 173), ('A08', 174), ('A0P', 175), ('A0W', 176), ('A22', 177), ('BLACK DENIM', 178), ('BLACK/DK GREY', 179), ('Black/Green', 180), ('BLACK/PINK', 181), ('Black/Red', 182), ('BlackDENIM', 183), ('BLUE', 184), ('BLUE ATOLL', 185), ('NAVY', 186), ('BLACK', 187), ('CLAY', 188), ('BLACK', 189), ('L.BLUE', 190), ('GREY', 191), ('WHITE', 192), ('GREY', 193), ('BLACK', 194), ('WHITE', 195), ('L.BLUE', 196), ('BLACK', 197), ('WHITE', 198), ('NAVY', 199), ('KHAKI', 200), ('BLACK', 201), ('GREY', 202), ('BLACK', 203), ('WHITE', 204), ('L.BLUE', 205), ('GREY', 206), ('WHITE', 207), ('L.BLUE', 208), ('BLACK', 209), ('WHITE', 210), ('SAGE', 211), ('BLACK', 212), ('CREAM/PINK', 213), ('CREAM/TAN', 214), ('CHACOAL', 215), ('CHARCOAL', 216), ('CHESTNUT', 217), ('D.GREEN', 218), ('D.GREY', 219), ('A56', 220), ('A57', 221), ('A5X', 222), ('AL5', 223), ('DK BLUE', 224), ('DK. DENIM', 225), ('DK. DENIM WASH', 226), ('DKGREEN', 227), ('GOLD', 228), ('BLACK', 229), ('HTR GREY', 230), ('Đen-Xanh lá', 231), (N'Đỏ', 232), (N'Đỏ gạch', 233), (N'Đỏ rượu', 234), ('Black/Red', 235), ('E26', 236), ('ECKO NAVY', 237), ('ECKONAVY', 238), ('ECRU', 239), ('EMPIRE YELLOW', 240), ('A0K', 241), ('F.GREEN', 242), ('F07', 243), ('A5T', 244), ('X7I', 245), ('A2K', 246), ('G44', 247), ('GALAPAGOS GREEN', 248), ('GBG', 249), ('WHITE', 250), ('001R', 251), ('001G', 252), ('990Y', 253), ('001Y', 254), ('001R', 255), ('001G', 256), ('Grey/Blue', 257), ('A804', 258), ('H03', 259), ('A804', 260), (N'Hoa nhí hồng', 261), ('A804', 262), (N'Hoa nhí tím', 263), ('A804', 264), ('990Y', 265), ('001R', 266), ('001B', 267), ('IGUANA', 268), ('Kem', 269), ('Khaki', 270), ('L.BLUE', 271), ('L.GREY', 272), ('H03', 273), ('LIGHT BROWN', 274), ('A06', 275), ('A0K', 276), ('LILAC', 277), ('A00', 278), ('A01', 279), ('BLACK', 280), ('NAVY', 281), ('HTR GREY', 282), ('LT. DENIM WASH', 283), ('A06', 284), ('A08', 285), ('A05', 286), ('A04', 287), ('MANGO', 288), ('A09', 289), ('A02', 290), ('A00', 291), ('BLUE', 292), ('ALU', 293), ('ALV', 294), ('AL3', 295), ('AL4', 296), ('OFF WHITE', 297), ('A0O', 298), ('OLIVE', 299), ('AL1', 300), ('AL2', 301), ('AL7', 302), ('AL8', 303), ('PESTO', 304), ('PINK', 305), ('POWDER PINK', 306), ('PURPLE', 307), ('R.ORANGE', 308), ('A04', 309), ('A0E', 310), ('Red/Navy', 311), ('A01', 312), (N'XANH DƯƠNG', 313), (N'XÁM - XANH', 314), ('A0V', 315), ('AL3', 316), ('ALQ', 317), ('AL1', 318), ('AL3', 319), ('AL3', 320), ('ALQ', 321), ('A01', 322), ('SPRING', 323), ('STONE', 324), ('T16', 325), ('T73', 326), ('TAN', 327), ('TAUPE', 328), ('TEAL', 329), ('TIDEPOOL', 330), ('A0C', 331), ('A63', 332), ('A61', 333), (N'Trắng Kem', 334), ('A63', 335), ('AL1', 336), ('AL2', 337), ('AL8', 338), ('BL WHITE', 339), ('LT BLUE', 340), ('BL WHITE', 341), ('ECKO NAVY', 342), ('WHITE', 343), (N'Vàng Đồng', 344), (N'Vàng-Cam', 345), ('VICTORIA BLUE', 346), (N'Viền hồng', 347), ('W71', 348), ('WHITE', 349), ('WHITE/BLACK', 350), ('WHITE/BLUE', 351), ('WHITE/FUCHSIA', 352), ('WHITE/GREEN', 353), ('WHITE/LILAC', 354), ('WHITE/NAVY', 355), ('WHITE/PINK', 356), ('AL5', 357), ('ALA', 358), ('A0C', 359), ('A5C', 360), ('A5D', 361), ('A5E', 362), ('A5F', 363), ('WWG', 364), ('E26', 365), ('W71', 366), ('A01', 367), ('A02', 368), ('A0C', 369), ('ĐEN', 370), ('XÁM', 371), ('A01', 372), ('D.GREEN', 373), ('M.BLUE', 374), ('L.GREY', 375), ('BLACK', 376), ('D.GREY', 377), ('NAVY', 378), ('RED', 379), ('BLACK', 380), ('L.BLUE', 381), ('BLUE', 382), ('GREEN', 383), ('NAVY', 384), ('M.BLUE', 385), ('L.BLUE', 386), ('L.GREY', 387), ('BLACK', 388), ('L.BLUE', 389), ('D.GREY', 390), ('NAVY', 391), ('BLACK', 392), ('L.BLUE', 393), ('BLUE', 394), ('GREEN', 395), ('NAVY', 396), ('A0E', 397), ('WWG', 398), ('A00', 399)
GO
SET IDENTITY_INSERT ProductColors OFF
GO
SET IDENTITY_INSERT Categories ON
GO
INSERT INTO Categories (Id, Name, Url, Gender, Type)
SELECT Id, Name, Url, Gender, Type  FROM OPENROWSET  (
    BULK 'D:/category.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id		Nvarchar(MAX)		'$.Id', 
            Name	Nvarchar(MAX)		'$.Name' ,
			Url		Nvarchar(MAX)		'$.Url',
			Gender  Nvarchar(MAX)		'$.Gender',
			Type	Nvarchar(MAX)		'$.Type'
        ) AS [Categories]
GO
SET IDENTITY_INSERT Categories OFF
GO
INSERT INTO Images (Data, ProductColorId)
SELECT Data, ProductColorId  FROM OPENROWSET  (
    BULK 'D:/images.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Data					Nvarchar(MAX)		'$.data' ,
			ProductColorId		Nvarchar(MAX)		'$.ProductColorId'
        ) AS [Images]
GO
SET IDENTITY_INSERT Products ON
GO
INSERT INTO Products (Id, Title, Description, CategoryId, BrandId)
SELECT Id, Title, Description, CategoryId, BrandId  FROM OPENROWSET  (
    BULK 'D:/product.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            Id				int					'$.Id', 
            Title			Nvarchar(MAX)		'$.title' ,
			Description		Nvarchar(MAX)		'$.description',
			CategoryId		int					'$.categoryId',
			BrandId			int					'$.brandId'
        ) AS [Products]
GO
SET IDENTITY_INSERT Products OFF
GO
INSERT INTO ProductVariants (ProductId, ProductColorId, Price, OriginalPrice, Quantity, ProductSize)
SELECT ProductId, ProductColorId, Price, OriginalPrice, Quantity, ProductSize  FROM OPENROWSET  (
    BULK 'D:/variants.json', 
    SINGLE_CLOB) AS [Json]    
    CROSS APPLY OPENJSON ( BulkColumn, '$' )
    WITH  (
            ProductId		int					'$.ProductId', 
            ProductColorId			Nvarchar(MAX)		'$.ProductColorId' ,
			Price			decimal(18,2)		'$.Price',
			OriginalPrice	decimal(18,2)		'$.OriginalPrice',
			Quantity		int					'$.Quantity',
			ProductSize		Nvarchar(MAX)		'$.ProductSize'			
        ) AS [ProductVariants]
GO
