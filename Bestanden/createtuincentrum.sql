create database Tuincentrum
go
use TuinCentrum
go

CREATE TABLE [Leveranciers](
	[LevNr] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](30) NOT NULL,
	[Adres] [nvarchar](30) NOT NULL,
	[PostNr] [nvarchar](10) NOT NULL,
	[Woonplaats] [nvarchar](30) NOT NULL,
 CONSTRAINT [Leveranciers$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[LevNr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Leveranciers] ON
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (2, N'Baumgarten', N'Takstraat 13', N'8500', N'Kortrijk')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (3, N'Struik', N'Bessenlaan 1', N'8560', N'Wevelgem')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (4, N'Spitman', N'Achtertuin 9', N'8930', N'Menen')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (5, N'Dezaaier', N'De Gronden 11', N'8560', N'Wevelgem')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (6, N'Mooiweer', N'Verlengde zomerstraat 24', N'8930', N'Menen')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (7, N'Bloem', N'Linnaeushof 17', N'8500', N'Kortrijk')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (8, N'De Plukker', N'Koeleplekstraat 10', N'8560', N'Wevelgem')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (9, N'Erica', N'Berkenweg 87', N'8930', N'Menen')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (10, N'De groene kas', N'Glasweg 1', N'8930', N'Menen')
INSERT [Leveranciers] ([LevNr], [Naam], [Adres], [PostNr], [Woonplaats]) VALUES (11, N'Flora', N'Oeverstraat 76', N'8930', N'Menen')
SET IDENTITY_INSERT [Leveranciers] OFF
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Soorten](
	[SoortNr] [int] IDENTITY(1,1) NOT NULL,
	[Soort] [nvarchar](10) NOT NULL,
	[MagazijnNr] [tinyint] NOT NULL,
 CONSTRAINT [Soorten$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[SoortNr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Soorten] ON
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (1, N'1-jarig', 2)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (2, N'2-jarig', 3)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (3, N'Bol', 1)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (4, N'Boom', 3)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (5, N'Heester', 4)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (6, N'Heide', 2)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (7, N'Klim', 2)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (8, N'Kruid', 3)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (9, N'Vast', 1)
INSERT [Soorten] ([SoortNr], [Soort], [MagazijnNr]) VALUES (10, N'Water', 4)
SET IDENTITY_INSERT [Soorten] OFF
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Planten](
	[PlantNr] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](30) NOT NULL,
	[SoortNr] [int] NOT NULL,
	[Levnr] [int] NOT NULL,
	[Kleur] [nvarchar](10) NOT NULL,
	[VerkoopPrijs] [money] NOT NULL,
 CONSTRAINT [Planten$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[PlantNr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Planten] ON
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (1, N'Rododendron', 5, 6, N'Rood', 7.2525)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (2, N'Sering', 5, 6, N'Paars', 7.2525)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (3, N'Kruisdistel', 9, 6, N'Blauw', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (4, N'Vuurdoorn', 5, 2, N'Wit', 1.8600)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (5, N'Klokjesbloem', 2, 4, N'Blauw', 0.6675)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (6, N'Kogeldistel', 9, 6, N'Blauw', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (7, N'Paardekastanje', 4, 10, N'Wit', 6.5100)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (8, N'Linde', 4, 4, N'Geel', 15.8025)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (9, N'Wijnstok', 7, 3, N'Onbekend', 3.7200)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (10, N'Violier', 2, 10, N'Gemengd', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (11, N'Zonnebloem', 2, 4, N'Geel', 0.6675)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (12, N'Populier', 4, 3, N'Wit', 1.6725)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (13, N'Cypergras', 10, 2, N'Onbekend', 1.8600)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (14, N'Zilverspar', 4, 6, N'Onbekend', 7.2525)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (16, N'Boterbloem', 9, 6, N'Wit', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (17, N'Hondstong', 9, 7, N'Blauw', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (18, N'Stokroos', 9, 8, N'Rood', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (19, N'Dwergcypres', 4, 7, N'Onbekend', 5.0175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (20, N'Ganzerik', 9, 2, N'Rood', 0.8400)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (21, N'Daglelie', 9, 6, N'Rood', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (22, N'Dovenetel', 9, 8, N'Geel', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (23, N'Muurbloem', 2, 4, N'Bruin', 0.6675)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (24, N'Pioen', 9, 3, N'Rood', 1.6725)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (25, N'Korenbloem', 9, 8, N'Blauw', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (26, N'Bereklauw', 9, 5, N'Wit', 1.3050)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (27, N'Klokjesbloem', 9, 6, N'Blauw', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (28, N'Lupine', 9, 7, N'Gemengd', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (29, N'Violier', 8, 11, N'Gemengd', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (30, N'Judaspenning', 2, 10, N'Lila', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (31, N'Azalea', 5, 10, N'Oranje', 6.5100)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (32, N'Esdoorn', 4, 10, N'Groen', 6.5100)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (33, N'Dragon', 8, 8, N'Wit', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (34, N'Forsythia', 5, 3, N'Geel', 2.0475)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (35, N'Kornoelje', 5, 3, N'Geel', 2.0475)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (36, N'Basilicum', 8, 8, N'Wit', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (37, N'Begonia', 7, 3, N'Rood', 0.2400)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (38, N'Sierui', 3, 10, N'Blauw', 1.3950)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (39, N'Vingerhoedskruid', 9, 8, N'Gemengd', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (40, N'Vlambloem', 8, 9, N'Gemengd', 0.5550)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (41, N'Aster', 4, 5, N'Gemengd', 0.2775)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (42, N'Akelei', 9, 7, N'Blauw', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (43, N'Viooltje', 2, 11, N'Gemengd', 0.1875)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (44, N'Azijnboom', 4, 4, N'Rood', 3.5325)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (45, N'Margriet', 9, 7, N'Wit', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (46, N'Krokus', 3, 3, N'Wit', 0.0375)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (47, N'Monnikskap', 9, 6, N'Violet', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (48, N'Lijsterbes', 4, 8, N'Wit', 2.7900)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (49, N'Bosrank', 7, 10, N'Paars', 2.4150)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (50, N'Iris', 3, 3, N'Blauw', 0.0375)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (51, N'Kikkerbeet', 10, 4, N'Wit', 0.4650)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (52, N'Magnolia', 5, 5, N'Wit', 5.3925)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (53, N'Acacia', 4, 10, N'Wit', 6.5100)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (54, N'Dotterbloem', 10, 3, N'Geel', 1.6725)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (55, N'Rozemarijn', 8, 8, N'Blauw', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (56, N'Meidoorn', 4, 6, N'Roze', 7.2525)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (57, N'Liguster', 5, 9, N'Wit', 0.1500)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (58, N'Ridderspoor', 9, 6, N'Lila', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (59, N'Dille', 8, 8, N'Geel', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (60, N'Engels gras', 9, 8, N'Rood', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (61, N'Kamille', 9, 6, N'Wit', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (62, N'Zuurbes', 5, 5, N'Oranje', 1.3050)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (63, N'Blauw druifje', 3, 3, N'Blauw', 0.0375)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (64, N'Winterheide', 6, 8, N'Wit', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (65, N'Berk', 4, 8, N'Onbekend', 2.7900)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (66, N'Passiebloem', 7, 4, N'Blauw', 3.5325)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (67, N'Toverhazelaar', 5, 3, N'Geel', 11.9025)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (68, N'Korenbloem', 1, 10, N'Gemengd', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (69, N'Beuk', 4, 9, N'Groen', 4.6500)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (70, N'Waterhyacint', 10, 2, N'Blauw', 1.8600)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (71, N'Klaproos', 9, 6, N'Rood', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (72, N'Tulpeboom', 4, 11, N'Geel', 8.3700)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (73, N'Peterselie', 8, 8, N'Onbekend', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (74, N'Majoraan', 8, 8, N'Paars', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (75, N'Pampusgras', 9, 4, N'Wit', 3.5325)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (76, N'Leverkruid', 9, 7, N'Paars', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (77, N'Lisdodde', 10, 3, N'Geel', 1.6725)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (78, N'Chrysant', 1, 6, N'Geel', 0.3000)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (79, N'Nagelkruid', 9, 6, N'Oranje', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (80, N'Zuring', 8, 8, N'Rood', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (81, N'Waterlelie', 10, 10, N'Wit', 4.4625)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (82, N'Struikheide', 6, 8, N'Gemengd', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (83, N'Blaasjeskruid', 10, 7, N'Geel', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (84, N'Boomheide', 6, 3, N'Roze', 2.0475)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (85, N'Tijm', 8, 7, N'Paars', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (86, N'Ridderspoor', 1, 10, N'Gemengd', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (87, N'Spar', 4, 9, N'Onbekend', 4.6500)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (88, N'Gipskruid', 9, 3, N'Wit', 1.6725)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (89, N'Brem', 5, 2, N'Geel', 1.8600)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (90, N'Dopheide', 6, 9, N'Rood', 0.5550)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (91, N'Wolfsmelk', 9, 7, N'Geel', 0.9300)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (92, N'Klaproos', 2, 10, N'Gemengd', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (93, N'Dahlia', 1, 10, N'Gemengd', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (94, N'Vuurwerkplant', 9, 5, N'Roze', 1.3050)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (95, N'Anemoon', 9, 5, N'Roze', 1.3050)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (96, N'Salie', 8, 8, N'Violet', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (97, N'Judasboom', 4, 4, N'Roze', 3.5325)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (98, N'Peperboompje', 5, 4, N'Roze', 5.5800)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (99, N'Hondstong', 2, 10, N'Blauw', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (100, N'Hulst', 5, 11, N'Onbekend', 8.3700)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (101, N'Gouden regen', 4, 3, N'Geel', 8.1825)
GO
print 'Processed 100 total records'
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (102, N'Wolgras', 10, 5, N'Wit', 1.3050)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (103, N'Bieslook', 8, 8, N'Paars', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (104, N'Alyssum', 1, 3, N'Paars', 0.2025)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (105, N'Klimop', 7, 8, N'Onbekend', 2.7900)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (106, N'Kalmoes', 10, 3, N'Onbekend', 1.6725)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (107, N'Petunia', 1, 6, N'Roze', 0.3000)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (108, N'Munt', 8, 8, N'Paars', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (109, N'Anjer', 9, 6, N'Wit', 1.1175)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (110, N'Sleutelbloem', 2, 8, N'Gemengd', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (111, N'Kervel', 8, 8, N'Wit', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (112, N'Zonnebloem', 9, 8, N'Geel', 0.7425)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (113, N'Leeuwebekje', 1, 6, N'Gemengd', 0.3000)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (114, N'Tulp', 3, 9, N'Geel', 0.1500)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (115, N'Gipskruid', 1, 10, N'Wit', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (116, N'Olijfwilg', 5, 3, N'Geel', 3.7200)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (117, N'Klaproos', 1, 10, N'Gemengd', 0.3750)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (118, N'Vuurpijl', 9, 5, N'Rood', 1.3050)
INSERT [Planten] ([PlantNr], [Naam], [SoortNr], [Levnr], [Kleur], [VerkoopPrijs]) VALUES (119, N'Jeneverbes', 4, 10, N'Onbekend', 2.4150)
SET IDENTITY_INSERT [Planten] OFF
ALTER TABLE [Planten]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$Planten$Kleur$disallow_zero_length] CHECK  ((len([Kleur])>(0)))
GO
ALTER TABLE [Planten] CHECK CONSTRAINT [SSMA_CC$Planten$Kleur$disallow_zero_length]
GO
ALTER TABLE [Planten]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$Planten$Naam$disallow_zero_length] CHECK  ((len([Naam])>(0)))
GO
ALTER TABLE [Planten] CHECK CONSTRAINT [SSMA_CC$Planten$Naam$disallow_zero_length]
GO
ALTER TABLE [Leveranciers]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$Leveranciers$Adres$disallow_zero_length] CHECK  ((len([Adres])>(0)))
GO
ALTER TABLE [Leveranciers] CHECK CONSTRAINT [SSMA_CC$Leveranciers$Adres$disallow_zero_length]
GO
ALTER TABLE [Leveranciers]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$Leveranciers$Naam$disallow_zero_length] CHECK  ((len([Naam])>(0)))
GO
ALTER TABLE [Leveranciers] CHECK CONSTRAINT [SSMA_CC$Leveranciers$Naam$disallow_zero_length]
GO
ALTER TABLE [Leveranciers]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$Leveranciers$PostNr$disallow_zero_length] CHECK  ((len([PostNr])>(0)))
GO
ALTER TABLE [Leveranciers] CHECK CONSTRAINT [SSMA_CC$Leveranciers$PostNr$disallow_zero_length]
GO
ALTER TABLE [Leveranciers]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$Leveranciers$Woonplaats$disallow_zero_length] CHECK  ((len([Woonplaats])>(0)))
GO
ALTER TABLE [Leveranciers] CHECK CONSTRAINT [SSMA_CC$Leveranciers$Woonplaats$disallow_zero_length]
GO
ALTER TABLE [Soorten]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$Soorten$Soort$disallow_zero_length] CHECK  ((len([Soort])>(0)))
GO
ALTER TABLE [Soorten] CHECK CONSTRAINT [SSMA_CC$Soorten$Soort$disallow_zero_length]
GO
ALTER TABLE [Planten]  WITH NOCHECK ADD  CONSTRAINT [Planten$LeveranciersPlanten] FOREIGN KEY([Levnr])
REFERENCES [Leveranciers] ([LevNr])
ON UPDATE CASCADE
GO
ALTER TABLE [Planten] CHECK CONSTRAINT [Planten$LeveranciersPlanten]
GO
ALTER TABLE [Planten]  WITH NOCHECK ADD  CONSTRAINT [Planten$SoortenPlanten] FOREIGN KEY([SoortNr])
REFERENCES [Soorten] ([SoortNr])
ON UPDATE CASCADE
GO
ALTER TABLE [Planten] CHECK CONSTRAINT [Planten$SoortenPlanten]
GO
