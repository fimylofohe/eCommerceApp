USE [eTicaret]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Addresses]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Banks]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Blogs]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Carts]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Comments]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[CouponHistorys]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Coupons]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[PaymentNotifications]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Pictures]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Settings]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Slider]    Script Date: 29.05.2024 21:49:48 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 29.05.2024 21:49:48 ******/
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
GO
SET IDENTITY_INSERT [dbo].[Blogs] ON 

INSERT [dbo].[Blogs] ([BlogId], [Title], [SeoURL], [Text], [Picture], [PublishedDate], [Status]) VALUES (2, N'Bir Dönemin Efsanesi Winamp Açık Kaynak Kodlu Oluyor', N'bir-donemin-efsanesi-winamp-acik-kaynak-kodlu-oluyor', N'<p>Özellikleri 2000''li yılların başında çok kullanılan Winamp programı, açık kaynak kodlu hâle getiriliyor.</p><p>Bir dönem müzik dinlemek için insanların aklına ilk gelen program <strong>Winamp </strong>idi. Müzik yayın servislerinin olmadığı yıllarda büyük başarı yakalayan program daha sonraları gözden düşmüştü. Varlığını devam ettiren program daha küçük bir kullanıcı grubuna hitap etmeye başlasa da ayakta kalmıştı. Winamp CEO''su Alexandre Saboundjian, <strong>24 Eylül 2024''te</strong> Winamp''ın açık kaynak kodlu hâle getirileceğini duyurdu. </p><p> </p><p>Programın <strong>bütün kaynak kodları</strong> açıklanan tarihte herkesin kullanımına açık hâle getirilecek. Böylelikle isteyen herkes Winamp''ın kendi isteklerine göre değiştirilmiş bir versiyonunu oluşturabilecek. Firmanın amacı ise insanlarının yaratıcılıklarını göstermesi sayesinde uygulamanın yeniden öne çıkmasını sağlamak. </p><p> </p><h2>Winamp, geliştiricilere güveniyor</h2><p><img src="https://www.webtekno.com/images/editor/default/0004/36/055587c886866e7c2dc9b98457603c7d1485689f.jpeg" alt="winamp" width="788" height="443"></p><p>Konuyla ilgili açıklamalarda bulunan <strong>Saboundjian</strong>, <i>"Bu karar, </i><a href="https://www.webtekno.com/konu/dunya"><i>dünya</i></a><i> çapında milyonlarca kullanıcıyı memnun edecek. Ortak noktamız yeni mobil oynatıcılar ve diğer platformlar olacak. Temmuz ayının başında yeni bir mobil oynatıcı yayımlayacağız. Yine de Windows''ta yazılımı kullanan ve binlerce geliştiricinin deneyimlerinden ve yaratıcılığından yararlanacak on milyonlarca kullanıcıyı unutmak istemiyoruz. Winamp yazılımın sahibi olarak kalacak ve resmî sürümde yapılacak yeniliklere karar verecek.</i>" ifadelerini kullandı. </p><p> </p><p>2021 yılında Winamp bir yenileme sürecine başlamıştı ancak <strong>çok da başarılı olamamıştı</strong>. Özellikle <a href="https://www.webtekno.com/konu/spotify">Spotify</a> gibi firmaların piyasaya girmesiyle birlikte artık insanlar üçüncü parti uygulamalardan uzaklaşmaya başladı. Bakalım açık kaynak kodlu sisteme geçiş ile birlikte Winamp eski günlerine dönebilecek mi? </p><p> </p>', N'54241a52-37ab-48fa-b593-942cc273aabb.jpg', CAST(N'2024-05-29T21:28:44.2062539' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Blogs] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name], [Status]) VALUES (4, N'Outdoor Ayakkabı', 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Pictures] ON 

INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (12, 6, N'cae9db0f-15d1-47d3-a8be-fc699d3b7e72.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (13, 6, N'bd93dc45-c24e-4b09-9f8a-a650be48250e.webp')
INSERT [dbo].[Pictures] ([PictureId], [ProductId], [Path]) VALUES (14, 6, N'16a5a861-18a2-4199-82d4-ad7da6bd4483.webp')
SET IDENTITY_INSERT [dbo].[Pictures] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [CategoryId], [SKU], [Name], [Description], [Price], [Stock], [Status], [SeoURL]) VALUES (6, 4, N'824815757', N'Salomon Xa Pro 3D V9 Gtx Gore-Tex® L47270 Outdoor Erkek Spor Ayakkabı MAVİ', N'<ul><li>Kampanya fiyatından satılmak üzere 5 adetten az stok bulunmaktadır.</li><li>İncelemiş olduğunuz ürünün satış fiyatını satıcı belirlemektedir.</li><li>Bir ürün, birden fazla satıcı tarafından satılabilir. Birden fazla satıcı tarafından satışa sunulan ürünlerin satıcıları ürün için belirledikleri fiyata, satıcı puanlarına, teslimat statülerine, ürünlerdeki promosyonlara, kargonun bedava olup olmamasına ve ürünlerin hızlı teslimat ile teslim edilip edilememesine, ürünlerin stok ve kategorileri bilgilerine göre sıralanmaktadır.</li><li>Bu üründen en fazla 10 adet sipariş verilebilir. 10 adedin üzerindeki siparişleri Trendyol iptal etme hakkını saklı tutar. Belirlenen bu limit kurumsal siparişlerde geçerli olmayıp, kurumsal siparişler için farklı limitler belirlenebilmektedir.</li><li>15 gün içinde ücretsiz iade.</li><li>Salomon Xa Pro 3D V9 Gtx Gore-Tex® L47270 Outdoor Erkek Spor Ayakkabı MAVİ</li></ul>', 9899.9, 100, 1, N'salomon-xa-pro-3d-v9-gtx-gore-tex-l47270-outdoor-erkek-spor-ayakkabi-mavi')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 

INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (1, N'iyzico_apikey', N'sandbox-o1vhmzSLiHkDSfR5OMlp99ssR47xCrLJ')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (2, N'iyzico_secretkey', N'NWhxYI2RCXVa64IGMXatIvqdTHUU6ZMQ')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (3, N'iyzico_test', N'true')
INSERT [dbo].[Settings] ([SettingId], [Mkey], [Mval]) VALUES (4, N'api_url', N'https://localhost:7279')
SET IDENTITY_INSERT [dbo].[Settings] OFF
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
