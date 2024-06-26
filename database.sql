USE [eTicaret]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AddressText] [nvarchar](max) NULL,
	[Province] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banks]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banks](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[AccountName] [nvarchar](max) NULL,
	[AccountNo] [nvarchar](max) NULL,
	[Branch] [nvarchar](max) NULL,
	[IBAN] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[SeoURL] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Picture] [nvarchar](max) NULL,
	[PublishedDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[GuestToken] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderId] [int] NULL,
	[AddDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[PublishedDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[NameSurname] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[PublishedDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CouponHistorys]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouponHistorys](
	[CouponHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[CouponId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_CouponHistorys] PRIMARY KEY CLUSTERED 
(
	[CouponHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[CouponId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[DiscountAmount] [int] NULL,
	[CouponCode] [nvarchar](max) NULL,
	[ValidityDate] [datetime2](7) NULL,
	[SingleUse] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Coupons] PRIMARY KEY CLUSTERED 
(
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[AddressId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[OrderKey] [nvarchar](max) NULL,
	[Amount] [float] NULL,
	[CouponAmount] [float] NULL,
	[TotalAmount] [float] NULL,
	[CouponHistoryId] [int] NULL,
	[OrderNote] [nvarchar](max) NULL,
	[OrderPay] [int] NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[OrderStatus] [int] NULL,
	[Status] [bit] NOT NULL,
	[CargoCode] [nvarchar](max) NULL,
	[CargoCompany] [nvarchar](max) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentNotifications]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentNotifications](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[BankId] [int] NOT NULL,
	[NameSurname] [nvarchar](max) NULL,
	[TotalAmount] [float] NULL,
	[Receipt] [nvarchar](max) NULL,
	[PayNote] [nvarchar](max) NULL,
	[PayDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentNotifications] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pictures]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pictures](
	[PictureId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Path] [nvarchar](max) NULL,
 CONSTRAINT [PK_Pictures] PRIMARY KEY CLUSTERED 
(
	[PictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[SKU] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [float] NULL,
	[Stock] [int] NULL,
	[Status] [bit] NOT NULL,
	[SeoURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingId] [int] IDENTITY(1,1) NOT NULL,
	[Mkey] [nvarchar](max) NULL,
	[Mval] [nvarchar](max) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slider]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slider](
	[SliderId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[SubTitle] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[ButtonTitle] [nvarchar](max) NULL,
	[ButtonUrl] [nvarchar](max) NULL,
	[BackgroundImg] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[SliderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1.06.2024 15:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Surname] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
	[Admin] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240429110058_firstA', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240430065009_firstB', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240522053035_firstC', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240528113824_firstD', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240529115455_firstE', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240529164110_firstF', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240529174307_firstG', N'8.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240531085937_firstH', N'8.0.3')
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([AddressId], [UserId], [AddressText], [Province], [District], [Country], [PostalCode]) VALUES (4, 1, N'Aşağitaşçili,29, 67902, Çaycuma, Zonguldak, Turkey', N' Zonguldak', N'Aşağitaşçili', N'Türkiye', N' 67902')
INSERT [dbo].[Addresses] ([AddressId], [UserId], [AddressText], [Province], [District], [Country], [PostalCode]) VALUES (5, 1, N'Kuşkavağı, Belediye Cd. No:77, 07070 Konyaaltı/Antalya', N'Antalya', N'Konyaaltı', N'Türkiye', N'07070')
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Banks] ON 

INSERT [dbo].[Banks] ([BankId], [Name], [AccountName], [AccountNo], [Branch], [IBAN], [Status]) VALUES (4, N'Enpara', N'Halil ŞAHİN', N'2856584152', N'Antalya', N'TR14 0006 2657 9236 9593 3224 31', 1)
INSERT [dbo].[Banks] ([BankId], [Name], [AccountName], [AccountNo], [Branch], [IBAN], [Status]) VALUES (5, N'Garanti', N'Halil ŞAHİN', N'255122341', N'Antalya', N'TR42 0006 2435 5439 7554 2682 79', 1)
SET IDENTITY_INSERT [dbo].[Banks] OFF
GO
SET IDENTITY_INSERT [dbo].[Blogs] ON 

INSERT [dbo].[Blogs] ([BlogId], [Title], [SeoURL], [Text], [Picture], [PublishedDate], [Status]) VALUES (2, N'Bir Dönemin Efsanesi Winamp Açık Kaynak Kodlu Oluyor', N'bir-donemin-efsanesi-winamp-acik-kaynak-kodlu-oluyor', N'<p>Özellikleri 2000''li yılların başında çok kullanılan Winamp programı, açık kaynak kodlu hâle getiriliyor.</p><p>Bir dönem müzik dinlemek için insanların aklına ilk gelen program <strong>Winamp </strong>idi. Müzik yayın servislerinin olmadığı yıllarda büyük başarı yakalayan program daha sonraları gözden düşmüştü. Varlığını devam ettiren program daha küçük bir kullanıcı grubuna hitap etmeye başlasa da ayakta kalmıştı. Winamp CEO''su Alexandre Saboundjian, <strong>24 Eylül 2024''te</strong> Winamp''ın açık kaynak kodlu hâle getirileceğini duyurdu. </p><p> </p><p>Programın <strong>bütün kaynak kodları</strong> açıklanan tarihte herkesin kullanımına açık hâle getirilecek. Böylelikle isteyen herkes Winamp''ın kendi isteklerine göre değiştirilmiş bir versiyonunu oluşturabilecek. Firmanın amacı ise insanlarının yaratıcılıklarını göstermesi sayesinde uygulamanın yeniden öne çıkmasını sağlamak. </p><p> </p><h2>Winamp, geliştiricilere güveniyor</h2><p><img src="https://www.webtekno.com/images/editor/default/0004/36/055587c886866e7c2dc9b98457603c7d1485689f.jpeg" alt="winamp" width="788" height="443"></p><p>Konuyla ilgili açıklamalarda bulunan <strong>Saboundjian</strong>, <i>"Bu karar, </i><a href="https://www.webtekno.com/konu/dunya"><i>dünya</i></a><i> çapında milyonlarca kullanıcıyı memnun edecek. Ortak noktamız yeni mobil oynatıcılar ve diğer platformlar olacak. Temmuz ayının başında yeni bir mobil oynatıcı yayımlayacağız. Yine de Windows''ta yazılımı kullanan ve binlerce geliştiricinin deneyimlerinden ve yaratıcılığından yararlanacak on milyonlarca kullanıcıyı unutmak istemiyoruz. Winamp yazılımın sahibi olarak kalacak ve resmî sürümde yapılacak yeniliklere karar verecek.</i>" ifadelerini kullandı. </p><p> </p><p>2021 yılında Winamp bir yenileme sürecine başlamıştı ancak <strong>çok da başarılı olamamıştı</strong>. Özellikle <a href="https://www.webtekno.com/konu/spotify">Spotify</a> gibi firmaların piyasaya girmesiyle birlikte artık insanlar üçüncü parti uygulamalardan uzaklaşmaya başladı. Bakalım açık kaynak kodlu sisteme geçiş ile birlikte Winamp eski günlerine dönebilecek mi? </p><p> </p>', N'54241a52-37ab-48fa-b593-942cc273aabb.jpg', CAST(N'2024-05-29T21:28:44.2062539' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Blogs] OFF
GO
SET IDENTITY_INSERT [dbo].[Carts] ON 

INSERT [dbo].[Carts] ([CartId], [GuestToken], [UserId], [ProductId], [Quantity], [OrderId], [AddDate]) VALUES (21, NULL, 1, 7, 1, 11, CAST(N'2024-05-30T10:26:41.1963095' AS DateTime2))
INSERT [dbo].[Carts] ([CartId], [GuestToken], [UserId], [ProductId], [Quantity], [OrderId], [AddDate]) VALUES (22, NULL, 1, 13, 1, 11, CAST(N'2024-05-30T10:26:47.5471922' AS DateTime2))
INSERT [dbo].[Carts] ([CartId], [GuestToken], [UserId], [ProductId], [Quantity], [OrderId], [AddDate]) VALUES (23, NULL, 1, 8, 1, 12, CAST(N'2024-05-30T10:41:08.6946279' AS DateTime2))
INSERT [dbo].[Carts] ([CartId], [GuestToken], [UserId], [ProductId], [Quantity], [OrderId], [AddDate]) VALUES (24, NULL, 1, 12, 1, 12, CAST(N'2024-05-30T10:41:13.5630551' AS DateTime2))
INSERT [dbo].[Carts] ([CartId], [GuestToken], [UserId], [ProductId], [Quantity], [OrderId], [AddDate]) VALUES (26, N'f91de647-62df-41db-a364-935348d1d811', NULL, 8, 3, NULL, CAST(N'2024-05-31T15:22:14.4435909' AS DateTime2))
INSERT [dbo].[Carts] ([CartId], [GuestToken], [UserId], [ProductId], [Quantity], [OrderId], [AddDate]) VALUES (27, NULL, 1, 8, 1, NULL, CAST(N'2024-05-31T15:22:49.7803299' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Carts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name], [Status]) VALUES (4, N'Ayakkabı', 1)
INSERT [dbo].[Categories] ([CategoryId], [Name], [Status]) VALUES (5, N'Sweatshirt', 1)
INSERT [dbo].[Categories] ([CategoryId], [Name], [Status]) VALUES (6, N'Şort', 1)
INSERT [dbo].[Categories] ([CategoryId], [Name], [Status]) VALUES (7, N'Aksesuar', 1)
INSERT [dbo].[Categories] ([CategoryId], [Name], [Status]) VALUES (8, N'Saat', 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([CommentId], [ProductId], [UserId], [Text], [PublishedDate], [Status], [OrderId]) VALUES (7, 7, 1, N'rengi duruşu kalınlığı çok güzel, kalıbı oversize ama yine de 1 beden büyük almakta fayda var ', CAST(N'2024-05-30T10:27:59.1446115' AS DateTime2), 1, 11)
INSERT [dbo].[Comments] ([CommentId], [ProductId], [UserId], [Text], [PublishedDate], [Status], [OrderId]) VALUES (8, 13, 1, N'Gayet Şık Gayet Güzel beğendim paketlenme biraz daha iyi olabilirdi ama yinede ellerinize sağlık teşekkür ederim Hediye içinde ayrı teşekkür ederim 😊', CAST(N'2024-05-30T10:28:29.6593796' AS DateTime2), 1, 11)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([ContactId], [NameSurname], [Email], [Subject], [Text], [PublishedDate], [Status]) VALUES (1, N'Halil Şahin', N'halilsahin0750@gmail.com', N'E-Posta değişimi', N'deneme', CAST(N'2024-05-31T12:04:09.4831283' AS DateTime2), 0)
INSERT [dbo].[Contacts] ([ContactId], [NameSurname], [Email], [Subject], [Text], [PublishedDate], [Status]) VALUES (2, N'Halil Şahin', N'halilsahin0750@gmail.com', N'E-Posta değişimi', N'deneme', CAST(N'2024-05-31T12:04:31.2713635' AS DateTime2), 0)
INSERT [dbo].[Contacts] ([ContactId], [NameSurname], [Email], [Subject], [Text], [PublishedDate], [Status]) VALUES (3, N'Halil Şahin', N'halilsahin0750@gmail.com', N'E-Posta değişimi', N'deneme', CAST(N'2024-05-31T13:31:24.5783848' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET IDENTITY_INSERT [dbo].[CouponHistorys] ON 

INSERT [dbo].[CouponHistorys] ([CouponHistoryId], [CouponId], [UserId], [OrderId], [Status]) VALUES (6, 8, 1, 11, 0)
INSERT [dbo].[CouponHistorys] ([CouponHistoryId], [CouponId], [UserId], [OrderId], [Status]) VALUES (7, 9, 1, 12, 0)
SET IDENTITY_INSERT [dbo].[CouponHistorys] OFF
GO
SET IDENTITY_INSERT [dbo].[Coupons] ON 

INSERT [dbo].[Coupons] ([CouponId], [Name], [Type], [DiscountAmount], [CouponCode], [ValidityDate], [SingleUse], [Status]) VALUES (8, N'Yaz İndirimi', N'2', 15, N'123', CAST(N'2025-11-27T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[Coupons] ([CouponId], [Name], [Type], [DiscountAmount], [CouponCode], [ValidityDate], [SingleUse], [Status]) VALUES (9, N'Kış İndirimi', N'2', 1, N'321', CAST(N'2025-11-27T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[Coupons] ([CouponId], [Name], [Type], [DiscountAmount], [CouponCode], [ValidityDate], [SingleUse], [Status]) VALUES (10, N'deneme', N'1', 123, N'132312321', CAST(N'2025-11-27T00:00:00.0000000' AS DateTime2), 1, 0)
SET IDENTITY_INSERT [dbo].[Coupons] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [AddressId], [UserId], [OrderKey], [Amount], [CouponAmount], [TotalAmount], [CouponHistoryId], [OrderNote], [OrderPay], [OrderDate], [OrderStatus], [Status], [CargoCode], [CargoCompany]) VALUES (11, 4, 1, N'6da3bb67-316c-4892-a455-50e25bc93d77', 2625.6414999999997, 463.34849999999994, 3088.99, 6, NULL, 1, CAST(N'2024-05-30T10:26:59.2871267' AS DateTime2), 3, 1, NULL, NULL)
INSERT [dbo].[Orders] ([OrderId], [AddressId], [UserId], [OrderKey], [Amount], [CouponAmount], [TotalAmount], [CouponHistoryId], [OrderNote], [OrderPay], [OrderDate], [OrderStatus], [Status], [CargoCode], [CargoCompany]) VALUES (12, 4, 1, N'4fa42a22-bcb3-41d9-b0bf-bf843b2e1cf2', 3366.495, 34.005, 3400.5, 7, N'özel sipariş', 2, CAST(N'2024-05-30T10:41:29.7869596' AS DateTime2), 0, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentNotifications] ON 

INSERT [dbo].[PaymentNotifications] ([PaymentId], [OrderId], [BankId], [NameSurname], [TotalAmount], [Receipt], [PayNote], [PayDate], [Status]) VALUES (3, 12, 4, N'Halil Şahin', 3000, N'ef5fe5cd-3eb0-4482-aba9-4bf616df7ffc.jpg', N'ödedim', CAST(N'2024-05-30T10:43:43.9463288' AS DateTime2), 1)
INSERT [dbo].[PaymentNotifications] ([PaymentId], [OrderId], [BankId], [NameSurname], [TotalAmount], [Receipt], [PayNote], [PayDate], [Status]) VALUES (4, 12, 5, N'Halil Şahin', 366.49, N'c0c491c3-7795-4b8c-a4a5-f3d6e386304b.jpg', N'öde', CAST(N'2024-05-30T10:44:12.6950586' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[PaymentNotifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Pictures] ON 

INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (12, 6, N'cae9db0f-15d1-47d3-a8be-fc699d3b7e72.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (13, 6, N'bd93dc45-c24e-4b09-9f8a-a650be48250e.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (14, 6, N'16a5a861-18a2-4199-82d4-ad7da6bd4483.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (15, 7, N'074fcbd9-71c1-4157-b175-22b24a7a39e5.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (16, 7, N'f3dfaf05-9a58-4a13-aadf-e25231df1c71.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (17, 7, N'885f4347-f24f-4430-9f8c-aa6ad46ab388.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (18, 8, N'6cb382e4-6927-443f-b135-9706adf5db1c.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (19, 8, N'dbc02998-9916-43d4-b46d-b5195478dd4c.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (20, 9, N'ad3189de-6284-4e3c-8016-8509b9307424.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (21, 9, N'32aed8ae-6d2d-42bc-8766-9eedf7603def.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (22, 9, N'6b30d4f3-0509-4fb6-9c3e-746da364d992.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (23, 10, N'21c055f5-a9d9-4277-9bc1-d20700da74a8.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (24, 10, N'007445bc-af48-498d-af2e-86dbf9a4a06d.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (25, 11, N'5ba303ef-5693-4c64-b257-cc2d5c6d4690.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (26, 11, N'9dc68987-5e9a-4088-b0dc-70b23cf89421.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (27, 11, N'b6bba08c-dd06-4290-b044-046a7a0bcb74.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (28, 12, N'248fcf3b-9e14-40dc-833a-38a2f643876f.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (29, 12, N'b84c472e-5023-4058-8e79-663f09ad832c.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (30, 12, N'9b253959-5c46-4d4c-bec0-b117e9365c39.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (31, 13, N'9b04d1b9-2f60-4eae-958f-928fdfdf6cf6.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (32, 13, N'a205a10a-7405-4d1f-a7a4-4dae47f9d0c9.webp')
SET IDENTITY_INSERT [dbo].[Pictures] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (6, 4, N'824815757', N'Salomon Xa Pro 3D V9 Gtx Gore-Tex® L47270 Outdoor Erkek Spor Ayakkabı MAVİ', N'<ul><li>Kampanya fiyatından satılmak üzere 5 adetten az stok bulunmaktadır.</li><li>İncelemiş olduğunuz ürünün satış fiyatını satıcı belirlemektedir.</li><li>Bir ürün, birden fazla satıcı tarafından satılabilir. Birden fazla satıcı tarafından satışa sunulan ürünlerin satıcıları ürün için belirledikleri fiyata, satıcı puanlarına, teslimat statülerine, ürünlerdeki promosyonlara, kargonun bedava olup olmamasına ve ürünlerin hızlı teslimat ile teslim edilip edilememesine, ürünlerin stok ve kategorileri bilgilerine göre sıralanmaktadır.</li><li>Bu üründen en fazla 10 adet sipariş verilebilir. 10 adedin üzerindeki siparişleri Trendyol iptal etme hakkını saklı tutar. Belirlenen bu limit kurumsal siparişlerde geçerli olmayıp, kurumsal siparişler için farklı limitler belirlenebilmektedir.</li><li>15 gün içinde ücretsiz iade.</li><li>Salomon Xa Pro 3D V9 Gtx Gore-Tex® L47270 Outdoor Erkek Spor Ayakkabı MAVİ</li></ul>', 9899.9, 100, 1, N'salomon-xa-pro-3d-v9-gtx-gore-tex-l47270-outdoor-erkek-spor-ayakkabi-mavi')
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (7, 5, N'2545134', N'Oversize Fit Bisiklet Yaka Basic Pamuklu Sweatshirt', N'<p>Ürün Bilgileri</p><ul><li>Modelin Ölçüleri:Boy: 1,87Göğüs: 95Bel: 74Basen: 97</li><li>Ana Kumaş İçeriği: : Pamuk 62%,Poliester 38%</li><li>Ürün Grubu: : Erkek Sweatshirt</li><li>Kalıp : Oversize Fit</li><li>Ürün Tipi : Sweatshirt</li><li>Ürün Mevsim Koşul : Sonbahar/Kış</li><li>Cinsiyet : Erkek</li><li>Renk : Kahverengi / Camel</li></ul><p>Ürün Hakkında</p><ul><li>Pamuk karışımlı yumuşak sweatshirt kumaşı, sade, basic tasarım, kol ve etek uçları esnek ribanalı, etek ucu etiket detaylı, bisiklet yaka, geniş kalıplı modeliyle spor şık bir görünüme sahip, günlük tarzınız için kurtarıcı parçanız olacak DeFacto Erkek Oversize Fit Bisiklet Yaka Basic Pamuklu Sweatshirt.</li><li>Bu ürünü evinizde deneyin, bedeni olmazsa size en yakın mağazamızdan ücretsiz değiştirin.</li></ul>', 689.99, 99, 1, N'oversize-fit-bisiklet-yaka-basic-pamuklu-sweatshirt')
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (8, 6, N'101291985', N'Uncertaın Erkek Comfort Fit Grimelanj Şort & Bermuda', N'<h2>Ürün Özellikleri</h2><ul><li>Materyal <strong>%100 Pamuk</strong></li><li>Kumaş Tipi <strong>Örme</strong></li><li>Stil <strong>Trend</strong></li><li>Renk <strong>Gri</strong></li><li>Desen <strong>Düz</strong></li><li>Kapama Şekli <strong>Kapamasız</strong></li><li>Bel <strong>Normal Bel</strong></li><li>Kalıp <strong>Comfort</strong></li><li>Paket İçeriği <strong>Tekli</strong></li><li>Cep Tipi <strong>Cepli</strong></li><li>Cep <strong>Çift cepli</strong></li><li>Ürün Detayı <strong>Cep</strong></li><li>Teknik <strong>Yok</strong></li><li>Ürün Tipi <strong>Düz</strong></li><li>Koleksiyon <strong>Young</strong></li><li>Astar Durumu <strong>Astarsız</strong></li><li>Paça Tipi <strong>Boru Paça</strong></li><li>Kordon Durumu <strong>Kordonsuz</strong></li><li>Ek Özellik <strong>Ek Özellik Mevcut Değil</strong></li><li>Dokuma Tipi <strong>3 İplik</strong></li><li>Persona <strong>Cool & Comfort</strong></li><li>Siluet <strong>Relaxed</strong></li><li>Kemer/Kuşak Durumu <strong>Kemersiz</strong></li><li>Ortam <strong>Casual/Günlük</strong></li><li>Boy <strong>Regular</strong></li><li>Yaş <strong>Tüm Yaş Grupları</strong></li></ul><h2>Materyal Bileşeni</h2><ul><li>Materyal Bileşeni<strong>100% Pamuk</strong></li></ul><h2>Yıkama Talimatları</h2><ul><li>Type 1</li></ul>', 750, 99, 1, N'uncertain-erkek-comfort-fit-grimelanj-sort-bermuda')
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (9, 7, N'743165420', N'Sertifikalı Mat Hematit Doğal Taşlı Örgü 925 Aayar Gümüş Tasarım Bileklik', N'<ul><li>15 gün içinde ücretsiz iade.</li><li>SERTİFİKALI MAT HEMATİT DOĞAL TAŞLI ÖRGÜ 925 AYAR GÜMÜŞ TASARIM BİLEKLİK (APOLLON SEMBOLLÜ) Apollon sembolünün anlamı: Apollon Antik Yunan döneminde Güneş Tanrısı olarak bilinmekteydi. Buğday sarısı saçlarıyla güneşin niteliklerini taşırdı. Şansının parlaması, Şanslılık, Parlak Gelecek ve Aydınlığı sembolize eder. Bilekliğimiz makrome kapama olduğu için bilek ölçünüze göre ayarlanabilir</li><li>18- 22 cm arası bileklere uygundur. Daha farklı bir ölçü için sipariş notunuza ölçü yazmayı unutmayın. Ürünlerimiz %100 doğal taşlardan oluşmaktadır ve tamamen el işçiliğiyle üretilmiştir. Ara aparatlar ve arka kapaması 925 Ayar Gümüştür. Bileklikte yer alan taşlar: Hematit Hematit Taşının Faydaları: Eklem ve Kas Ağrısı, Özgün Düşünme, Topraklama Sağlar, Takıntılardan Kurtulma, Hayat Dengesi, Dayanıklılık ve Canlılık, Modu Yükseltir, Cesaret, Özgüven, İrade Gücü, Bağışlayabilme, Güçlü Hafıza, Güvenilirlik Sağlar, Özgün Olma, Farklı Olma, Psikolojik Destek, Bağımlılıkların giderilmesi</li></ul>', 1239.85, 100, 1, N'sertifikali-mat-hematit-dogal-tasli-orgu-925-aayar-gumus-tasarim-bileklik')
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (10, 8, N'814353208', N'Ferrucci Çelik Kordon Erkek Kol Saati', N'<h2>Ürün Özellikleri</h2><ul><li>Kasa Çapı <strong>41-45 mm</strong></li><li>Kasa Materyali <strong>Çelik</strong></li><li>Kordon Materyali <strong>Çelik</strong></li><li>Su Geçirmezlik <strong>3 ATM</strong></li><li>Mekanizma <strong>Quartz</strong></li><li>Kordon Renk <strong>Gümüş</strong></li><li>Ek Özellik <strong>Su Geçirmez</strong></li><li>Cam Tipi <strong>Mineral</strong></li><li>Garanti Süresi <strong>2 Yıl</strong></li><li>Renk <strong>Gümüş</strong></li></ul>', 1549, 100, 1, N'ferrucci-celik-kordon-erkek-kol-saati')
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (11, 4, N'A41y8029', N'Avva Erkek Bej %100 Süet Deri Makosen Ayakkabı', N'<h2>Ürün Özellikleri</h2><ul><li>Topuk Boyu <strong>Kısa Topuklu (1- 4 cm)</strong></li><li>Topuk Tipi <strong>İnce Topuklu</strong></li><li>Renk <strong>Bej</strong></li><li>Ortam <strong>Casual/Günlük</strong></li></ul>', 2771.99, 100, 1, N'avva-erkek-bej-100-suet-deri-makosen-ayakkabi')
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (12, 5, N'365726467', N'Ruck & Maul Erkek Sweatshirt 23024 1000 - Beyaz', N'<h2>Ürün Özellikleri</h2><ul><li>Desen <strong>Yazı Karekterli / Motto</strong></li><li>Kalıp <strong>Oversize</strong></li><li>Kapama Şekli <strong>Fermuarlı</strong></li><li>Materyal <strong>%100 Organik Pamuk</strong></li><li>Yaka Tipi <strong>Bisiklet Yaka</strong></li><li>Kumaş Tipi <strong>Örme</strong></li><li>Kol Tipi <strong>Uzun Kol</strong></li><li>Renk <strong>Beyaz</strong></li><li>Kesim <strong>Oversize</strong></li><li>Kol Boyu <strong>Uzun</strong></li><li>Boy <strong>Standart</strong></li><li>Dokuma Tipi <strong>3 iplik penye şardonlu</strong></li></ul>', 2650.5, 99, 1, N'ruck-maul-erkek-sweatshirt-23024-1000---beyaz')
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (13, 8, N'769427688', N'Reward Çelik Fonksinları aktif kurmalı Erkek kol saati', N'<h2>Ürün Özellikleri</h2><ul><li>Kasa Çapı <strong>41-45 mm</strong></li><li>Kasa Materyali <strong>Çelik</strong></li><li>Kordon Materyali <strong>Çelik</strong></li><li>Su Geçirmezlik <strong>3 ATM</strong></li><li>Mekanizma <strong>Otomatik</strong></li><li>Kordon Renk <strong>Gümüş</strong></li><li>Ek Özellik <strong>Su Geçirmez</strong></li><li>Cam Tipi <strong>Mineral</strong></li><li>Garanti Süresi <strong>2 Yıl</strong></li><li>Renk <strong>Yeşil</strong></li></ul>', 2399, 99, 1, N'reward-celik-fonksinlari-aktif-kurmali-erkek-kol-saati')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 

INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (1, N'iyzico_apikey', N'sandbox-o1vhmzSLiHkDSfR5OMlp99ssR47xCrLJ')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (2, N'iyzico_secretkey', N'NWhxYI2RCXVa64IGMXatIvqdTHUU6ZMQ')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (3, N'iyzico_test', N'true')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (4, N'api_url', N'https://localhost:7279')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (5, N'title', N'MaleFashion')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (6, N'description', N'Erkekler için online giyim satış mağazası')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (7, N'keywords', N'malefashion, erkek, giyim, mağaza')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (8, N'logo', N'478e474a-8710-47c6-9bb6-9767841f3bfe.png')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (9, N'favicon', N'80ae30bb-6c0c-413f-b4d2-60faf552f4aa.png')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (10, N'google_iframe', N'https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d111551.9926412813!2d-90.27317134641879!3d38.606612219170856!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x54eab584e432360b%3A0x1c3bb99243deb742!2sUnited%20States!5e0!3m2!1sen!2sbd!4v1597926938024!5m2!1sen!2sbd')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (11, N'address', N'Antalya / Türkiye')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (12, N'phonenumber', N'+90 850 550 50 07')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (13, N'email', N'info@si4.net')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (14, N'recaptcha_sitekey', N'6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (15, N'recaptcha_secretkey', N'6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe')
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO
SET IDENTITY_INSERT [dbo].[Slider] ON 

INSERT [dbo].[Slider] ([SliderId], [Title], [SubTitle], [Description], [ButtonTitle], [ButtonUrl], [BackgroundImg], [Status]) VALUES (8, N'Stiliniz Soğuk Dinlemez: Kışa Hazır Olun!', N'Kış ayları geldiğinde stilinizden ödün vermeyin!', N'Erkek Kış Modasında Sıcak ve Şık Çözümler', N'Alışveriş Yap', N'/Shop', N'757b8701-fbf5-4829-82fe-1a682bbe9934.jpg', 1)
INSERT [dbo].[Slider] ([SliderId], [Title], [SubTitle], [Description], [ButtonTitle], [ButtonUrl], [BackgroundImg], [Status]) VALUES (9, N'Zarif ve Şık: Her Durumda Göz Alıcı Olun!', N'Avva', N'Sonsuz Şıklık!', N'Alışveriş Yap', N'/Shop', N'e24d8344-769e-4405-9d74-319782b50f4e.png', 1)
SET IDENTITY_INSERT [dbo].[Slider] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Name], [Surname], [Email], [Password], [PhoneNumber], [Status], [Admin]) VALUES (1, N'Halil', N'Şahin', N'halilsahin0750@gmail.com', N'12345', N'05425380750', 0, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Admin]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Users_UserId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Orders_OrderId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Products_ProductId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Users_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Products_ProductId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[CouponHistorys]  WITH CHECK ADD  CONSTRAINT [FK_CouponHistorys_Coupons_CouponId] FOREIGN KEY([CouponId])
REFERENCES [dbo].[Coupons] ([CouponId])
GO
ALTER TABLE [dbo].[CouponHistorys] CHECK CONSTRAINT [FK_CouponHistorys_Coupons_CouponId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Addresses_AddressId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_CouponHistorys_CouponHistoryId] FOREIGN KEY([CouponHistoryId])
REFERENCES [dbo].[CouponHistorys] ([CouponHistoryId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_CouponHistorys_CouponHistoryId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[PaymentNotifications]  WITH CHECK ADD  CONSTRAINT [FK_PaymentNotifications_Banks_BankId] FOREIGN KEY([BankId])
REFERENCES [dbo].[Banks] ([BankId])
GO
ALTER TABLE [dbo].[PaymentNotifications] CHECK CONSTRAINT [FK_PaymentNotifications_Banks_BankId]
GO
ALTER TABLE [dbo].[PaymentNotifications]  WITH CHECK ADD  CONSTRAINT [FK_PaymentNotifications_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[PaymentNotifications] CHECK CONSTRAINT [FK_PaymentNotifications_Orders_OrderId]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_Products_ProductId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
