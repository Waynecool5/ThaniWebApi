USE [WebAsync]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePoints]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP PROCEDURE [dbo].[UpdatePoints]
GO
/****** Object:  StoredProcedure [dbo].[InsertPoints]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP PROCEDURE [dbo].[InsertPoints]
GO
/****** Object:  StoredProcedure [dbo].[InsertDocuments]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP PROCEDURE [dbo].[InsertDocuments]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerPoints]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP PROCEDURE [dbo].[GetCustomerPoints]
GO
/****** Object:  Index [IX_Points]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP INDEX [IX_Points] ON [dbo].[Points]
GO
/****** Object:  Table [dbo].[ThaniStoreCodes]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP TABLE [dbo].[ThaniStoreCodes]
GO
/****** Object:  Table [dbo].[Points]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP TABLE [dbo].[Points]
GO
/****** Object:  Table [dbo].[MassyApiCodes]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP TABLE [dbo].[MassyApiCodes]
GO
/****** Object:  Table [dbo].[JSONDocuments]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP TABLE [dbo].[JSONDocuments]
GO
/****** Object:  UserDefinedFunction [dbo].[UNIX_TIMESTAMP]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP FUNCTION [dbo].[UNIX_TIMESTAMP]
GO
/****** Object:  UserDefinedTableType [dbo].[PointsTable]    Script Date: 10/18/2018 10:07:30 PM ******/
DROP TYPE [dbo].[PointsTable]
GO
/****** Object:  UserDefinedTableType [dbo].[PointsTable]    Script Date: 10/18/2018 10:07:30 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[UNIX_TIMESTAMP]    Script Date: 10/18/2018 10:07:30 PM ******/
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
/****** Object:  Table [dbo].[JSONDocuments]    Script Date: 10/18/2018 10:07:30 PM ******/
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
/****** Object:  Table [dbo].[MassyApiCodes]    Script Date: 10/18/2018 10:07:30 PM ******/
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
/****** Object:  Table [dbo].[Points]    Script Date: 10/18/2018 10:07:30 PM ******/
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
 CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED 
(
	[Points_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThaniStoreCodes]    Script Date: 10/18/2018 10:07:30 PM ******/
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
SET IDENTITY_INSERT [dbo].[JSONDocuments] ON 
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (1, N'{"Points_id":null,"Document_id":null,"ptsCustomerNo":"7678976890222","ptsFirstName":"NewCustome","ptsLastName":"LastCustomer","ptsUnitType":"P","ptsMode":"P","ptsTotal":60,"ptsValue":600.00,"ptsValueRate":0.10,"ptsDiscount":6.00,"ptsDiscountRate":0.10,"ptsLocation":"SS","ptsCashier":"Wayne"}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T13:01:19.957' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (2, N'{"Points_id":-1,"Document_id":-1,"ptsCustomerNo":"7678976890222","ptsFirstName":"Test","ptsLastName":"Testers","ptsUnitType":"P","ptsMode":"P","ptsTotal":60.0,"ptsValue":600.0,"ptsValueRate":0.1,"ptsDiscount":6.0,"ptsDiscountRate":0.1,"ptsLocation":"SS","ptsCashier":"Wayne","ptsMlid":0,"ptsUnix":0,"ptsPin":0,"ptsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T14:05:49.337' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (3, N'{"Points_id":-1,"Document_id":-1,"ptsCustomerNo":"7678976890222","ptsFirstName":"Test","ptsLastName":"Testers","ptsUnitType":"P","ptsMode":"P","ptsTotal":60.0,"ptsValue":600.0,"ptsValueRate":0.1,"ptsDiscount":6.0,"ptsDiscountRate":0.1,"ptsLocation":"SS","ptsCashier":"Wayne","ptsMlid":0,"ptsUnix":0,"ptsPin":0,"ptsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T14:11:13.373' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (4, N'{"Points_id":-1,"Document_id":-1,"ptsCustomerNo":"7678976890222","ptsFirstName":"Test","ptsLastName":"Testers","ptsUnitType":"P","ptsMode":"P","ptsTotal":60.0,"ptsValue":600.0,"ptsValueRate":0.1,"ptsDiscount":6.0,"ptsDiscountRate":0.1,"ptsLocation":"SS","ptsCashier":"Wayne","ptsMlid":0,"ptsUnix":0,"ptsPin":0,"ptsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T14:14:51.633' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (5, N'{"Points_id":-1,"Document_id":-1,"ptsCustomerNo":"7678976890222","ptsFirstName":"Test","ptsLastName":"Testers","ptsUnitType":"P","ptsMode":"P","ptsTotal":60.0,"ptsValue":600.0,"ptsValueRate":0.1,"ptsDiscount":6.0,"ptsDiscountRate":0.1,"ptsLocation":"SS","ptsCashier":"Wayne","ptsMlid":0,"ptsUnix":0,"ptsPin":0,"ptsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T14:26:44.510' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (6, N'{"Points_id":-1,"Document_id":-1,"ptsCustomerNo":"7678976890222","ptsFirstName":"Test","ptsLastName":"Testers","ptsUnitType":"P","ptsMode":"P","ptsTotal":60.0,"ptsValue":600.0,"ptsValueRate":0.1,"ptsDiscount":6.0,"ptsDiscountRate":0.1,"ptsLocation":"SS","ptsCashier":"Wayne","ptsMlid":0,"ptsUnix":0,"ptsPin":0,"ptsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T14:37:55.443' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (7, N'{"Points_id":-1,"Document_id":-1,"ptsCustomerNo":"7678976890222","ptsFirstName":"Test","ptsLastName":"Testers","ptsUnitType":"P","ptsMode":"P","ptsTotal":60.0,"ptsValue":600.0,"ptsValueRate":0.1,"ptsDiscount":6.0,"ptsDiscountRate":0.1,"ptsLocation":"SS","ptsCashier":"Wayne","ptsMlid":0,"ptsUnix":0,"ptsPin":0,"ptsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T14:49:51.603' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (8, N'{"Points_id":-1,"Document_id":-1,"PtsCustomerNo":"7678976890222","PtsFirstName":"Test","PtsLastName":"Testers","PtsUnitType":"P","PtsMode":"P","PtsTotal":60.0,"PtsValue":600.0,"PtsValueRate":0.1,"PtsDiscount":6.0,"PtsDiscountRate":0.1,"PtsLocation":"SS","PtsCashier":"Wayne","PtsMlid":0,"PtsUnix":0,"PtsPin":0,"PtsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T15:14:16.147' AS DateTime))
GO
INSERT [dbo].[JSONDocuments] ([Document_id], [docPointsJSON], [docAddresses], [docCards], [docEmailAddresses], [docNotes], [docPhones], [docTimeStamp]) VALUES (9, N'{"Points_id":-1,"Document_id":-1,"PtsCustomerNo":"7678976890222","PtsFirstName":"Test","PtsLastName":"Testers","PtsUnitType":"P","PtsMode":"P","PtsTotal":60.0,"PtsValue":600.0,"PtsValueRate":0.1,"PtsDiscount":6.0,"PtsDiscountRate":0.1,"PtsLocation":"SS","PtsCashier":"Wayne","PtsMlid":0,"PtsUnix":0,"PtsPin":0,"PtsQsa":null}', NULL, NULL, NULL, NULL, NULL, CAST(N'2018-10-18T15:17:27.543' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[JSONDocuments] OFF
GO
SET IDENTITY_INSERT [dbo].[MassyApiCodes] ON 
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (1, N'Thani’s Test location 1', N'3200952a13c9de101be13a999eeb1e22', 5101)
GO
INSERT [dbo].[MassyApiCodes] ([Secret_ID], [Secret_Name], [Secret_Code], [Secret_Mlid]) VALUES (2, N'
Thani’s Test location 2', N'8e5ec547f95e950ed6bd97c814ea769e', 5105)
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
SET IDENTITY_INSERT [dbo].[Points] ON 
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (1, 1, N'7678976890222', N'NewCustome', N'LastCustomer', N'P', N'P', CAST(60.00 AS Decimal(12, 2)), CAST(600.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), N'SS', N'Wayne', N'1539867679', CAST(N'2018-10-18T13:01:19.957' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (2, 2, N'7678976890222', N'Test', N'Testers', N'P', N'P', CAST(60.00 AS Decimal(12, 2)), CAST(600.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), N'SS', N'Wayne', N'1539871549', CAST(N'2018-10-18T14:05:49.337' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (3, 3, N'7678976890222', N'Test', N'Testers', N'P', N'P', CAST(60.00 AS Decimal(12, 2)), CAST(600.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), N'SS', N'Wayne', N'1539871873', CAST(N'2018-10-18T14:11:13.373' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (4, 4, N'7678976890222', N'Test', N'Testers', N'P', N'P', CAST(60.00 AS Decimal(12, 2)), CAST(600.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), N'SS', N'Wayne', N'1539872091', CAST(N'2018-10-18T14:14:51.633' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (5, 5, N'7678976890222', N'Test', N'Testers', N'P', N'P', CAST(60.00 AS Decimal(12, 2)), CAST(600.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), N'SS', N'Wayne', N'1539872804', CAST(N'2018-10-18T14:26:44.510' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (6, 6, N'7678976890222', N'Test', N'Testers', N'P', N'P', CAST(60.00 AS Decimal(12, 2)), CAST(600.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), N'SS', N'Wayne', N'1539873475', CAST(N'2018-10-18T14:37:55.443' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (7, 7, N'7678976890222', N'Test', N'Testers', N'P', N'P', CAST(60.00 AS Decimal(12, 2)), CAST(600.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(6.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), N'SS', N'Wayne', N'1539874191', CAST(N'2018-10-18T14:49:51.603' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (8, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1539875656', CAST(N'2018-10-18T15:14:16.147' AS DateTime))
GO
INSERT [dbo].[Points] ([Points_id], [Document_id], [ptsCustomerNo], [ptsFirstName], [ptsLastName], [ptsUnitType], [ptsMode], [ptsTotal], [ptsValue], [ptsValueRate], [ptsDiscount], [ptsDiscountrate], [ptsLocation], [ptsCashier], [ptsUnix], [ptsSalesDate]) VALUES (9, 9, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1539875847', CAST(N'2018-10-18T15:17:27.543' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Points] OFF
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
/****** Object:  Index [IX_Points]    Script Date: 10/18/2018 10:07:30 PM ******/
CREATE NONCLUSTERED INDEX [IX_Points] ON [dbo].[Points]
(
	[ptsLocation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerPoints]    Script Date: 10/18/2018 10:07:30 PM ******/
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
/****** Object:  StoredProcedure [dbo].[InsertDocuments]    Script Date: 10/18/2018 10:07:30 PM ******/
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

		--Massy Points Message
		SELECT P.Points_id, P.Document_id, P.ptsCustomerNo,P.ptsFirstName,P.ptsLastName, 
		       P.ptsUnitType, P.ptsMode, P.ptsTotal, 
		       P.ptsValue, P.ptsValueRate, P.ptsDiscount, P.ptsDiscountrate, P.ptsCashier, 
			   P.ptsUnix, P.ptsSalesDate, P.ptsLocation, M.Secret_Code AS ptsQsa, 
				M.Secret_Mlid AS ptsMlid, '00000' AS ptsPin
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
/****** Object:  StoredProcedure [dbo].[InsertPoints]    Script Date: 10/18/2018 10:07:30 PM ******/
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
			ptsValue,ptsValueRate, ptsDiscount, ptsDiscountRate, ptsLocation, ptsCashier)
		SELECT 
		   ptsCustomerNo,ptsFirstName,ptsLastName, ptsUnitType, ptsMode, ptsTotal, 
		   ptsValue,ptsValueRate, ptsDiscount, ptsDiscountRate, ptsLocation, ptsCashier
		FROM JSONDocuments
			CROSS APPLY
			OPENJSON(JSONDocuments.docPointsJSON) 
				WITH (
					ptsCustomerNo nvarchar(50), --N'$.ptsCustomerNo',
					ptsFirstName nvarchar(50),
					ptsLastName nvarchar(50),
					ptsUnitType nvarchar(2),
					ptsMode nvarchar(2),
					ptsTotal decimal(12, 2),  --N'$.ptsTotal',
					ptsValue decimal(12, 2),
					ptsValueRate decimal(12, 2),
					ptsDiscount decimal(12, 2),
					ptsDiscountRate decimal(12, 2),
					ptsLocation nvarchar(20),
					ptsCashier nvarchar(20)
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
/****** Object:  StoredProcedure [dbo].[UpdatePoints]    Script Date: 10/18/2018 10:07:30 PM ******/
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
