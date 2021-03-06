USE [WebAsync]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePoints]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP PROCEDURE [dbo].[UpdatePoints]
GO
/****** Object:  StoredProcedure [dbo].[InsertPoints]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP PROCEDURE [dbo].[InsertPoints]
GO
/****** Object:  StoredProcedure [dbo].[InsertDocuments]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP PROCEDURE [dbo].[InsertDocuments]
GO
/****** Object:  StoredProcedure [dbo].[GetRedeemProfile]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP PROCEDURE [dbo].[GetRedeemProfile]
GO
/****** Object:  StoredProcedure [dbo].[GetPointsProfile]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP PROCEDURE [dbo].[GetPointsProfile]
GO
/****** Object:  StoredProcedure [dbo].[GetList]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP PROCEDURE [dbo].[GetList]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerPoints]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP PROCEDURE [dbo].[GetCustomerPoints]
GO
/****** Object:  Index [IX_ThaniStoreCodes]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP INDEX [IX_ThaniStoreCodes] ON [dbo].[ThaniStoreCodes]
GO
/****** Object:  Index [IX_Registration]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP INDEX [IX_Registration] ON [dbo].[Registration]
GO
/****** Object:  Index [IX_Points]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP INDEX [IX_Points] ON [dbo].[Points]
GO
/****** Object:  Table [dbo].[ThaniStoreCodes]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[ThaniStoreCodes]
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[Registration]
GO
/****** Object:  Table [dbo].[Points]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[Points]
GO
/****** Object:  Table [dbo].[MassyApiCodes]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[MassyApiCodes]
GO
/****** Object:  Table [dbo].[Main]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[Main]
GO
/****** Object:  Table [dbo].[JSONDocuments]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[JSONDocuments]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[Company]
GO
/****** Object:  Table [dbo].[Apps]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TABLE [dbo].[Apps]
GO
/****** Object:  UserDefinedFunction [dbo].[UNIX_TIMESTAMP]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP FUNCTION [dbo].[UNIX_TIMESTAMP]
GO
/****** Object:  UserDefinedTableType [dbo].[PointsTable]    Script Date: 11/11/2018 11:08:09 PM ******/
DROP TYPE [dbo].[PointsTable]
GO
/****** Object:  UserDefinedTableType [dbo].[PointsTable]    Script Date: 11/11/2018 11:08:09 PM ******/
CREATE TYPE [dbo].[PointsTable] AS TABLE(
	[Points_id] [int] NOT NULL,
	[ptsCustomerNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsTotal] [int] NULL,
	[ptsValue] [decimal](12, 2) NULL,
	[ptsDiscount] [decimal](12, 2) NULL,
	[ptsMode] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsLocation] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ptsJSON] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ptsTStamp] [timestamp] NULL,
	PRIMARY KEY CLUSTERED 
(
	[Points_id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  UserDefinedFunction [dbo].[UNIX_TIMESTAMP]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UNIX_TIMESTAMP](@ctimestamp [datetime])
RETURNS [int] WITH EXECUTE AS CALLER
AS 
BEGIN
  /* Function body */
  declare @return integer

  SELECT @return = DATEDIFF(SECOND,{d '1970-01-01'}, @ctimestamp)

  return @return
END
GO
/****** Object:  Table [dbo].[Apps]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apps](
	[App_ID] [int] NULL,
	[Reg_ID] [int] NULL,
	[AppBrok] [int] NULL,
	[AppBrokDate] [datetime] NULL,
	[AppMani] [int] NULL,
	[AppManiDate] [datetime] NULL,
	[AppWare] [int] NULL,
	[AppWareDate] [datetime] NULL,
	[AppBusi] [int] NULL,
	[AppBusiDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Comp_ID] [int] IDENTITY(1,1) NOT NULL,
	[Company] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VAT] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__Test2__3214EC07C8719005] PRIMARY KEY CLUSTERED 
(
	[Comp_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JSONDocuments]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JSONDocuments](
	[Document_id] [int] IDENTITY(1,1) NOT NULL,
	[docPointsJSON] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[docAddresses] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[docCards] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[docEmailAddresses] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[docNotes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[docPhones] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[docTimeStamp] [datetime] NULL,
 CONSTRAINT [JSONDocumentsPk] PRIMARY KEY CLUSTERED 
(
	[Document_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Main]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Main](
	[Main_ID] [int] IDENTITY(1,1) NOT NULL,
	[Comp_ID] [int] NULL,
	[InternalRef] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Main] PRIMARY KEY CLUSTERED 
(
	[Main_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MassyApiCodes]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MassyApiCodes](
	[Secret_ID] [int] IDENTITY(1,1) NOT NULL,
	[Secret_Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Secret_Code] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Secret_Mlid] [int] NULL,
 CONSTRAINT [PK_MassyApiCodes] PRIMARY KEY CLUSTERED 
(
	[Secret_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Points]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Points](
	[Points_id] [bigint] IDENTITY(1,1) NOT NULL,
	[Document_id] [bigint] NULL,
	[ptsCustomerNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsFirstName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsLastName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsUnitType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsMode] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsTotal] [decimal](12, 2) NULL,
	[ptsValue] [decimal](12, 2) NULL,
	[ptsValueRate] [decimal](12, 2) NULL,
	[ptsDiscount] [decimal](12, 2) NULL,
	[ptsDiscountrate] [decimal](12, 2) NULL,
	[ptsLocation] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsCashier] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsUnix] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsSalesDate] [datetime] NULL,
	[ptsInvoice] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsLimit] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ptsfcn] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED 
(
	[Points_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registration](
	[Reg_ID] [int] IDENTITY(1,1) NOT NULL,
	[Comp_ID] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_Address] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_Country] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_CtyAbb] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_VAT] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_ImpNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_DecNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_VISA] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_Expire] [datetime] NULL,
	[Comp_CreateDate] [datetime] NULL,
	[Comp_Owner] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_Password] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comp_Active] [int] NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[Reg_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThaniStoreCodes]    Script Date: 11/11/2018 11:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThaniStoreCodes](
	[Store_ID] [int] IDENTITY(1,1) NOT NULL,
	[Secret_ID] [int] NULL,
	[Store_Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Store_Code] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_StoreCodes] PRIMARY KEY CLUSTERED 
(
	[Store_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 
GO
INSERT [dbo].[Company] ([Comp_ID], [Company], [VAT]) VALUES (1, N'UBRITE', N'6806200090')
GO
INSERT [dbo].[Company] ([Comp_ID], [Company], [VAT]) VALUES (2, N'Networks', N'6768690089')
GO
SET IDENTITY_INSERT [dbo].[Company] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[Main] ON 
GO
INSERT [dbo].[Main] ([Main_ID], [Comp_ID], [InternalRef]) VALUES (1, 1, N'BB01-001-00000001')
GO
INSERT [dbo].[Main] ([Main_ID], [Comp_ID], [InternalRef]) VALUES (2, 1, N'BB01-001-000230123')
GO
INSERT [dbo].[Main] ([Main_ID], [Comp_ID], [InternalRef]) VALUES (3, 2, N'BB01-001-000234757')
GO
SET IDENTITY_INSERT [dbo].[Main] OFF
GO
SET IDENTITY_INSERT [dbo].[MassyApiCodes] ON 
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (1, N'Thani’s Test location 1', N'3200952a13c9de101be13a999eeb1e22', 5101)
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (2, N'Thani’s Test location 2', N'8e5ec547f95e950ed6bd97c814ea769e', 5105)
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (3, N'Thani’s Test location 3', N'cf79ef08f3fcdac9792d273706fa1b1a', 5106)
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (4, N'Shoelocker Test location', N'ea6c8d483b5b24a92d86f53a6a84c646', 5102)
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (5, N'Indian Grill Test location', N'69f6e15a7b5ba286afddf9d8abfafd6c', 5103)
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (6, N'Brands Test location', N'7183577a2b6bad08f984c9dfd664b6c3', 5104)
GO
SET IDENTITY_INSERT [dbo].[MassyApiCodes] OFF
GO
SET IDENTITY_INSERT [dbo].[Registration] ON 
GO
INSERT [dbo].[Registration] ([Reg_ID], [Comp_ID], [Comp_Name], [Comp_Address], [Comp_Country], [Comp_CtyAbb], [Comp_VAT], [Comp_ImpNo], [Comp_DecNo], [Comp_VISA], [Comp_Expire], [Comp_CreateDate], [Comp_Owner], [Comp_Email], [Comp_Password], [Comp_Active]) VALUES (1, N'BB00001', N'Demo Company', N'23 Regency Park, Christ Church, Barbados.', N'BARBADOS', N'BB', N'00012289', N'56253', N'23', N'1234123412341234-546', NULL, CAST(N'2017-12-27T12:00:00.000' AS DateTime), N'Financially Responsible', N'demo@webasync.com', N'12345678', 1)
GO
SET IDENTITY_INSERT [dbo].[Registration] OFF
GO
SET IDENTITY_INSERT [dbo].[ThaniStoreCodes] ON 
GO
INSERT [dbo].[ThaniStoreCodes] ([Store_ID], [Secret_ID], [Store_Name], [Store_Code]) VALUES (1, 1, N'Thani Swan Street', N'SS')
GO
INSERT [dbo].[ThaniStoreCodes] ([Store_ID], [Secret_ID], [Store_Name], [Store_Code]) VALUES (2, 2, N'Thani Bridge Street', N'BS')
GO
INSERT [dbo].[ThaniStoreCodes] ([Store_ID], [Secret_ID], [Store_Name], [Store_Code]) VALUES (3, 3, N'Thani Shoelocker', N'SL')
GO
SET IDENTITY_INSERT [dbo].[ThaniStoreCodes] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Points]    Script Date: 11/11/2018 11:08:10 PM ******/
CREATE NONCLUSTERED INDEX [IX_Points] ON [dbo].[Points]
(
	[ptsLocation] ASC,
	[Document_id] ASC,
	[ptsFirstName] ASC,
	[ptsLastName] ASC,
	[ptsSalesDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Registration]    Script Date: 11/11/2018 11:08:10 PM ******/
CREATE NONCLUSTERED INDEX [IX_Registration] ON [dbo].[Registration]
(
	[Comp_ID] ASC,
	[Comp_CtyAbb] ASC,
	[Comp_VAT] ASC,
	[Comp_Name] ASC,
	[Comp_Active] ASC,
	[Comp_Password] ASC,
	[Comp_VISA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ThaniStoreCodes]    Script Date: 11/11/2018 11:08:10 PM ******/
CREATE NONCLUSTERED INDEX [IX_ThaniStoreCodes] ON [dbo].[ThaniStoreCodes]
(
	[Store_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerPoints]    Script Date: 11/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCustomerPoints]
	@ID [int] = -1,
	@XMode [int] = 0
WITH EXECUTE AS CALLER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @XMode = 0 
	begin
		SELECT * FROM  Points
	End
	Else
	Begin
    	SELECT *
		FROM   Points
		WHERE  (Points_id = @id)
	End

END
GO
/****** Object:  StoredProcedure [dbo].[GetList]    Script Date: 11/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetList]
	@mode [varchar](10) = 'select'
WITH EXECUTE AS CALLER
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
/****** Object:  StoredProcedure [dbo].[GetPointsProfile]    Script Date: 11/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPointsProfile]
	@Loc_ID [nvarchar](20),
	@Card [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
	Declare  @timeStamp datetime 
    
	Select @timeStamp= CURRENT_TIMESTAMP

	SELECT        @Card AS ptsCustomerNo, M.Secret_Mlid AS ptsMlid, CAST('00000' AS nvarchar(5)) AS ptsPin, M.Secret_Code AS ptsSecret, dbo.UNIX_TIMESTAMP(@timeStamp) AS ptsUnix
	FROM            ThaniStoreCodes AS T INNER JOIN
							 MassyApiCodes AS M ON T.Secret_ID = M.Secret_ID
	WHERE        (T.Store_Code = @Loc_ID)

END
GO

/****** Object:  StoredProcedure [dbo].[InsertDocuments]    Script Date: 11/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDocuments]
	@Document [nvarchar](max)
WITH EXECUTE AS CALLER
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
		VALUES (@Document,@timeStamp)
		SELECT @document_ID =  IDENT_CURRENT('JSONDocuments') 

		Execute InsertPoints @document_ID = @document_ID, @timeStamp = @timeStamp, @Points_id = @Points_id output

		--return new record details for JSON Object
		--Select(SELECT  Points_id, Document_id, ptsCustomerNo, ptsUnitType, ptsMode, 
		--				ptsTotal, ptsValue, ptsDiscount, ptsLocation, ptsCashier
		--		FROM Points 
		--		WHERE  (Points_id = @Points_id) 
		--		FOR JSON PATH,INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
		--) as Points

		
		------ TESTING   ---Massy Points Message
		--SELECT P.Points_id, P.Document_id, '42100999892'as ptsCustomerNo,P.ptsFirstName,P.ptsLastName, 
		--       P.ptsUnitType, P.ptsMode, P.ptsTotal, 
		--       P.ptsValue, P.ptsValueRate, P.ptsDiscount, P.ptsDiscountrate, P.ptsCashier, 
		--	   P.ptsUnix, P.ptsSalesDate, P.ptsLocation, '' AS ptsQsa, 
		--		'9999' AS ptsMlid, Cast('00000' as nvarchar(5)) AS ptsPin,
		--		M.Secret_Code as ptsSecret
		--FROM  ThaniStoreCodes AS T RIGHT OUTER JOIN
		--	    Points AS P ON T.Store_Code = P.ptsLocation LEFT OUTER JOIN
		--		  MassyApiCodes AS M ON T.Secret_ID = M.Secret_ID
		--WHERE (P.Points_id = @Points_id)



		--Massy Points Message LIVE
		SELECT P.Points_id, P.Document_id, P.ptsCustomerNo,P.ptsFirstName,P.ptsLastName, 
		       P.ptsUnitType, P.ptsMode, P.ptsTotal, 
		       P.ptsValue, P.ptsValueRate, P.ptsDiscount, P.ptsDiscountrate, P.ptsCashier, 
			   P.ptsUnix, P.ptsSalesDate, P.ptsLocation, --'' AS ptsQsa, 
				M.Secret_Mlid AS ptsMlid, Cast('00000' as nvarchar(5)) AS ptsPin,
				M.Secret_Code as ptsSecret, Isnull(ptsInvoice,'') as ptsInvoice, 
				Isnull(ptsLimit,'') as ptsLimit, Isnull(ptsfcn,'') as ptsfcn
		FROM  ThaniStoreCodes AS T RIGHT OUTER JOIN
			    Points AS P ON T.Store_Code = P.ptsLocation LEFT OUTER JOIN
				  MassyApiCodes AS M ON T.Secret_ID = M.Secret_ID
		WHERE (P.Points_id = @Points_id)

	End Try
	Begin Catch
	SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState ,ERROR_PROCEDURE() AS ErrorProcedure  
    ,ERROR_LINE() AS ErrorLine ,ERROR_MESSAGE() AS ErrorMessage;  
	End Catch

END

GO


/****** Object:  StoredProcedure [dbo].[InsertPoints]    Script Date: 14/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPoints]
	@document_ID [bigint],
	@timeStamp [datetime] = null,
	@Points_id [bigint] OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
	
	Declare @Points nvarchar(max)
	
	BEGIN TRY

		INSERT INTO dbo.Points (
			ptsCustomerNo,ptsFirstName,ptsLastName, ptsUnitType, ptsMode, ptsTotal, 
			ptsValue,ptsValueRate, ptsDiscount, ptsDiscountRate, ptsLocation, ptsCashier,
			ptsInvoice,ptsLimit,ptsfcn)
		SELECT 
		   ptsCustomerNo,ptsFirstName,ptsLastName, ptsUnitType, ptsMode, ptsTotal, 
		   ptsValue,ptsValueRate, ptsDiscount, ptsDiscountRate, ptsLocation, ptsCashier,
		   IsNull(ptsInvoice,'') as ptsInvoice, Isnull(ptsLimit,'') as ptsLimit, 
		   Isnull(ptsfcn,'') as ptsfcn
		FROM JSONDocuments
			CROSS APPLY
			OPENJSON(JSONDocuments.docPointsJSON) 
				WITH (
					ptsCustomerNo nvarchar(50), --N''''$.ptsCustomerNo'''',
					ptsFirstName nvarchar(50),
					ptsLastName nvarchar(50),
					ptsUnitType nvarchar(2),
					ptsMode nvarchar(2),
					ptsTotal decimal(12, 2),  --N''''$.ptsTotal'''',
					ptsValue decimal(12, 2),
					ptsValueRate decimal(12, 2),
					ptsDiscount decimal(12, 2),
					ptsDiscountRate decimal(12, 2),
					ptsLocation nvarchar(20),
					ptsCashier nvarchar(20),
					ptsInvoice nvarchar(50),
					ptsLimit nvarchar(20),
					ptsfcn nvarchar(50)
				)
		where Document_id = @document_ID

		SET @Points_id = IDENT_CURRENT('Points') --@@SCOPE_IDENTITY(Points)
	
		Update Points
		Set Document_id = @document_ID, ptsSalesDate = @timeStamp , ptsUnix = dbo.UNIX_TIMESTAMP(@timeStamp)
		Where Points_id = @Points_id

	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState ,ERROR_PROCEDURE() AS ErrorProcedure  
				,ERROR_LINE() AS ErrorLine ,ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[UpdatePoints]    Script Date: 11/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePoints]
	@Points [dbo].[PointsTable] READONLY
WITH EXECUTE AS CALLER
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
/****** Object:  StoredProcedure [dbo].[GetRedeemProfile]    Script Date: 11/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRedeemProfile]
	@Loc_ID [nvarchar](20),
	@Card [nvarchar](50),
	@Units [decimal](12, 2),
	@UnitType [nvarchar](10)
WITH EXECUTE AS CALLER
AS
BEGIN
	Declare  @timeStamp datetime 
    
	Select @timeStamp= CURRENT_TIMESTAMP

	Select @Card as ptsCustomerNo, @Units as ptsUnits, @UnitType as ptsUnitType, M.Secret_Mlid AS ptsMlid, Cast('00000' as nvarchar(5)) AS ptsPin,
				M.Secret_Code as ptsSecret, dbo.UNIX_TIMESTAMP(@timeStamp) as ptsUnix
	FROM  ThaniStoreCodes AS T INNer JOIN
			MassyApiCodes AS M ON T.Secret_ID = M.Secret_ID
	WHERE (T.Store_Code = @Loc_ID)

END

GO

/****** Object:  StoredProcedure [dbo].[GetVoidProfile]    Script Date: 11/11/2018 11:08:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetVoidProfile]
	@Loc_ID [nvarchar](20),
	@Invoice [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
	Declare  @timeStamp datetime 
    
	Select @timeStamp= CURRENT_TIMESTAMP

	Select @Invoice as ptsInvoice, M.Secret_Mlid AS ptsMlid, Cast('00000' as nvarchar(5)) AS ptsPin,
				M.Secret_Code as ptsSecret, dbo.UNIX_TIMESTAMP(@timeStamp) as ptsUnix
	FROM  ThaniStoreCodes AS T INNer JOIN
			MassyApiCodes AS M ON T.Secret_ID = M.Secret_ID
	WHERE (T.Store_Code = @Loc_ID)

END
GO