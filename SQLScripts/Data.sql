USE [WebAsync]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePoints]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdatePoints]
GO
/****** Object:  StoredProcedure [dbo].[InsertPoints]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[InsertPoints]
GO
/****** Object:  StoredProcedure [dbo].[InsertDocuments]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[InsertDocuments]
GO
/****** Object:  StoredProcedure [dbo].[GetList]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[GetList]
GO
/****** Object:  Table [dbo].[Points]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP TABLE IF EXISTS [dbo].[Points]
GO
/****** Object:  Table [dbo].[JSONDocuments]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP TABLE IF EXISTS [dbo].[JSONDocuments]
GO
/****** Object:  UserDefinedFunction [dbo].[UNIX_TIMESTAMP]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP FUNCTION IF EXISTS [dbo].[UNIX_TIMESTAMP]
GO
/****** Object:  UserDefinedTableType [dbo].[PointsTable]    Script Date: 10/17/2018 4:11:42 PM ******/
DROP TYPE IF EXISTS [dbo].[PointsTable]
GO
/****** Object:  UserDefinedTableType [dbo].[PointsTable]    Script Date: 10/17/2018 4:11:42 PM ******/
CREATE TYPE [dbo].[PointsTable] AS TABLE(
	[Points_id] [int] NOT NULL,
	[ptsCustomerNo] [nvarchar](50) NULL,
	[ptsTotal] [int] NULL,
	[ptsValue] [decimal](12, 2) NULL,
	[ptsDiscount] [decimal](12, 2) NULL,
	[ptsMode] [nvarchar](1) NULL,
	[ptsLocation] [nvarchar](50) NOT NULL,
	[ptsJSON] [nvarchar](max) NOT NULL,
	[ptsTStamp] [timestamp] NULL,
	PRIMARY KEY CLUSTERED 
(
	[Points_id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  UserDefinedFunction [dbo].[UNIX_TIMESTAMP]    Script Date: 10/17/2018 4:11:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UNIX_TIMESTAMP] (
@ctimestamp datetime
)
RETURNS integer
AS 
BEGIN
  /* Function body */
  declare @return integer

  SELECT @return = DATEDIFF(SECOND,{d '1970-01-01'}, @ctimestamp)

  return @return
END
GO
/****** Object:  Table [dbo].[JSONDocuments]    Script Date: 10/17/2018 4:11:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JSONDocuments](
	[Document_id] [int] IDENTITY(1,1) NOT NULL,
	[docPointsJSON] [nvarchar](max) NULL,
	[docAddresses] [nvarchar](max) NULL,
	[docCards] [nvarchar](max) NULL,
	[docEmailAddresses] [nvarchar](max) NULL,
	[docNotes] [nvarchar](max) NULL,
	[docPhones] [nvarchar](max) NULL,
	[docTimeStamp] [datetime] NULL,
 CONSTRAINT [JSONDocumentsPk] PRIMARY KEY CLUSTERED 
(
	[Document_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Points]    Script Date: 10/17/2018 4:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Points](
	[Points_id] [bigint] IDENTITY(1,1) NOT NULL,
	[Document_id] [bigint] NULL,
	[ptsCustomerNo] [nvarchar](50) NULL,
	[ptsUnitType] [nvarchar](2) NULL,
	[ptsMode] [nvarchar](2) NULL,
	[ptsTotal] [int] NULL,
	[ptsValue] [decimal](12, 2) NULL,
	[ptsDiscount] [decimal](12, 2) NULL,
	[ptsLocation] [nvarchar](20) NULL,
	[ptsCashier] [nvarchar](20) NULL,
	[ptsSalesData] [datetime] NULL,
	[ptsUnix] [nvarchar](50) NULL,
 CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED 
(
	[Points_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[JSONDocuments] ON 

INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (1, N'{"Points_id":null,"Document_id":null,"ptsCustomerNo":"7678976890222","ptsUnitType":"P","ptsMode":"P","ptsTotal":60,"ptsValue":600.00,"ptsDiscount":6.00,"ptsLocation":"SS","ptsCashier":"Wayne"}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-15T22:17:07.783' AS DateTime))
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (2, N'{"Points_id":null,"Document_id":null,"ptsCustomerNo":"7678976890222","ptsUnitType":"P","ptsMode":"P","ptsTotal":60,"ptsValue":600.00,"ptsDiscount":6.00,"ptsLocation":"SS","ptsCashier":"Wayne"}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-16T07:44:04.057' AS DateTime))
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (3, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-17T14:29:55.607' AS DateTime))
SET IDENTITY_INSERT [dbo].[JSONDocuments] OFF
SET IDENTITY_INSERT [dbo].[Points] ON 

INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsDiscount], [ptsLocation], [ptsCashier], [ptsSalesData], [ptsUnix]) VALUES (1, 1, N'7678976890222', N'P', N'P', 60, CAST(600.00 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), N'SS', N'Wayne', CAST(N'2018-10-15T22:17:07.783' AS DateTime), N'1539641827')
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsDiscount], [ptsLocation], [ptsCashier], [ptsSalesData], [ptsUnix]) VALUES (2, 3, N'7678976890222', N'P', N'P', 60, CAST(600.00 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), N'SS', N'Wayne', CAST(N'2018-10-17T14:29:55.607' AS DateTime), N'1539786595')
SET IDENTITY_INSERT [dbo].[Points] OFF
/****** Object:  StoredProcedure [dbo].[GetList]    Script Date: 10/17/2018 4:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetList]
 @mode varchar(10)= 'select'--,
-- @results NVARCHAR(MAX)  output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	Declare @results NVARCHAR(MAX)--,@mode varchar(10) = 'select'
	SET NOCOUNT ON;

	If @mode = 'select'
		Begin
			-- Insert statements for procedure here
			--Set @results =Select (SELECT Main.Main_ID AS ID, Main.Comp_ID, RTRIM(Main.InternalRef) AS InternalRef, Company.Comp_ID as "Company.Comp_ID", Company.Company as "Company.CompName" , Company.VAT as "Company.VAT"
			--			 FROM  Main INNER JOIN
   --                      Company ON Main.Comp_ID = Company.Comp_ID
			--			FOR JSON PATH,INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER  )  as re--
					
			--Set @results = 
			--Select CAST ( JSON_QUERY((SELECT Main.Main_ID AS ID, Main.Comp_ID, RTRIM(Main.InternalRef) AS InternalRef, 
			--					Company.Comp_ID as "Company.Comp_ID", Company.Company as "Company.CompName" , Company.VAT as "Company.VAT"
			--			 FROM  Main INNER JOIN
   --                      Company ON Main.Comp_ID = Company.Comp_ID
			--			FOR JSON PATH,INCLUDE_NULL_VALUES  )) --as results) --, WITHOUT_ARRAY_WRAPPER
			--			-- For Json path, WITHOUT_ARRAY_WRAPPER
			--			as nvarchar(max)) as result
			 
		--Select  @results =	()JSON_QUERY	
		SELECT Company.Comp_ID, Company.Company as CompName , Company.VAT							
		FROM Company 

		SELECT   M.Comp_ID, RTRIM(M.InternalRef) AS InternalRef	,	M.Main_ID AS ID
		FROM Main as M


		--Select (SELECT  M.Main_ID AS ID, M.Comp_ID, RTRIM(M.InternalRef) AS InternalRef, 
		--	JSON_QUERY(( Select Company.Comp_ID, Company.Company as CompName , Company.VAT							
		--				 FROM Company 
		--				 Where  Company.Comp_ID = M.Comp_ID 
		--				 FOR JSON PATH,INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		--				 )) as CompList
		--FROM Main as M
		--FOR JSON PATH,INCLUDE_NULL_VALUES )  as result --, WITHOUT_ARRAY_WRAPPER, ROOT('DataDto')



--SELECT 'Text' as myText,  
--      JSON_QUERY((SELECT 12 day, 8 mon FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)) as myJson  
--FOR JSON PATH 

			--Select  @results as results, Current_timestamp as GetTime
			--return @results
		End
	Else if @mode = 'insert'
		Begin
		  INSERT INTO Main  
		  SELECT *   
		  FROM OPENJSON(@results)  
		  WITH (Comp_ID nvarchar(50), InternalRef nvarchar(50))


		  INSERT INTO Company  
		  SELECT *   
		  FROM OPENJSON(@results,  N'$.Company.CompanyArray')  
			WITH (--Comp_ID int			N'$.Company.Comp_ID', 
				  CompName nvarchar(50) N'$.Company.CompName', 
				  VAT nvarchar(50)      N'$.Company.VAT')
		  AS CompanyJsonData;

		End
	--Else if @mode = 'update'
	--	Begin
	--		UPDATE Main
	--		SET Company=JSON_MODIFY(Company,"$Company.CompName",'London')
	--		WHERE Main_ID=2
	--	End
	--Else if @mode = 'delete'
	--	Begin

	--	End


		--SET @town = JSON_VALUE(@jsonInfo, '$.info.address.town')  
END
GO
/****** Object:  StoredProcedure [dbo].[InsertDocuments]    Script Date: 10/17/2018 4:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertDocuments]
    @document nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	Declare @document_ID bigint, @Points_id bigint, @timeStamp datetime 

	Begin Try

		Select @timeStamp= CURRENT_TIMESTAMP
		Set @Points_id = 0

		-- Insert statements for procedure here 
		-- OUTPUT Inserted.Document_id

		INSERT INTO JSONDocuments(docPointsJSON,docTimeStamp)
		VALUES (@document,@timeStamp)
		SELECT @document_ID =  IDENT_CURRENT('JSONDocuments') 

		Execute InsertPoints @document_ID = @document_ID, @timeStamp = @timeStamp, @Points_id = @Points_id output

		--return new record details for JSON Object
		--Select(SELECT  Points_id, Document_id, ptsCustomerNo, ptsUnitType, ptsMode, 
		--				ptsTotal, ptsValue, ptsDiscount, ptsLocation, ptsCashier
		--		FROM Points 
		--		WHERE  (Points_id = @Points_id) 
		--		FOR JSON PATH,INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		--) as Points

		SELECT *
		FROM   Points
		WHERE  (Points_id = @Points_id)

	End Try
	Begin Catch
	SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState ,ERROR_PROCEDURE() AS ErrorProcedure  
    ,ERROR_LINE() AS ErrorLine ,ERROR_MESSAGE() AS ErrorMessage;  
	End Catch

END
GO
/****** Object:  StoredProcedure [dbo].[InsertPoints]    Script Date: 10/17/2018 4:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertPoints]
	@document_ID bigint null,
	@timeStamp datetime = null,
	@Points_id bigint output
AS
BEGIN
	SET NOCOUNT ON;
	
	Declare @Points nvarchar(max)
	
	BEGIN TRY

		INSERT INTO dbo.Points (
			ptsCustomerNo, ptsUnitType, ptsMode, ptsTotal, ptsValue, ptsDiscount, ptsLocation, ptsCashier)
		SELECT --*
		   ptsCustomerNo, ptsUnitType, ptsMode, ptsTotal, ptsValue, ptsDiscount, ptsLocation, ptsCashier
		FROM JSONDocuments
			CROSS APPLY
			OPENJSON(JSONDocuments.docPointsJSON) 
				WITH (
					ptsCustomerNo nvarchar(50), --N'$.ptsCustomerNo',
					ptsUnitType nvarchar(2),
					ptsMode nvarchar(2),
					ptsTotal int,  --N'$.ptsTotal',
					ptsValue decimal(12, 2),
					ptsDiscount decimal(12, 2),
					ptsLocation nvarchar(20),
					ptsCashier nvarchar(20)
				)
		where Document_id = @document_ID

		SET @Points_id = IDENT_CURRENT('Points') --@@SCOPE_IDENTITY(Points)
	
		Update Points
		Set Document_id = @document_ID, ptsSalesData = @timeStamp , ptsUnix = dbo.UNIX_TIMESTAMP(@timeStamp)
		Where Points_id = @Points_id

	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState ,ERROR_PROCEDURE() AS ErrorProcedure  
				,ERROR_LINE() AS ErrorLine ,ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePoints]    Script Date: 10/17/2018 4:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePoints]
	(@Points [dbo].[PointsTable] READONLY)
AS
BEGIN

	SET NOCOUNT ON;

	--have a list of objects to send to the database
	--you need a user-defined table type and a stored procedure that accepts the table type

	UPDATE Points
	SET ptsCustomerNo = upd.ptsCustomerNo, ptsTotal = upd.ptsTotal, 
		ptsValue = upd.ptsValue, ptsDiscount = upd.ptsDiscount, ptsMode = upd.ptsMode, 
		ptsLocation = upd.ptsLocation
	FROM @Points upd
	WHERE (Points.Points_id = upd.Points_id)



END
GO
