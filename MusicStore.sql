CREATE DATABASE MusicStore



CREATE TABLE Collectives(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(40) NOT NULL,

CONSTRAINT CK_Collective_Name CHECK(Name<>' ' ),
CONSTRAINT UQ_Collective_Name UNIQUE(Name)
)  

CREATE TABLE Publishers(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(40) NOT NULL,

CONSTRAINT CK_Publisher_Name CHECK(Name<>' ' ),
CONSTRAINT UQ_Publisher_Name UNIQUE(Name)
)

CREATE TABLE Genres(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(40) NOT NULL,

CONSTRAINT CK_Genres_Name CHECK(Name<>' ' ),
CONSTRAINT UQ_Genres_Name UNIQUE(Name)
)
insert into Records(Name,MusicQuantity,YearPublishing,CostPrice,PriceSale,CollectiveId,PublisherId,GenreId)
values ('123',123,2000,123,123,1,1,1)

select * from ClientBoughtGoods
select * from Collectives
select * from Publishers

CREATE TABLE Records(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(40) NOT NULL,
MusicQuantity INT NOT NULL DEFAULT(1),
YearPublishing INT NOT NULL DEFAULT(YEAR(GETDATE())),
CostPrice INT NOT NULL,
PriceSale INT NOT NULL,
CollectiveId INT NOT NULL,
PublisherId INT NOT NULL,
GenreId INT NOT  NULL,

CONSTRAINT CK_Records_Name CHECK(Name<>' '),
CONSTRAINT UQ_Records_Name UNIQUE(Name),
CONSTRAINT CK_Records_MusicQuantity CHECK(MusicQuantity>0),
CONSTRAINT FK_Records_CollectiveId FOREIGN KEY (CollectiveId) REFERENCES Collectives(Id),
CONSTRAINT FK_Records_PublisherId  FOREIGN KEY (PublisherId ) REFERENCES Publishers(Id),
CONSTRAINT FK_Records_GenreId FOREIGN KEY (GenreId) REFERENCES Genres(Id),
CONSTRAINT CK_Records_YearPublishing CHECK(YearPublishing BETWEEN 1900 AND YEAR(GETDATE())),
CONSTRAINT CK_Records_CostPrice CHECK(CostPrice>0),
CONSTRAINT CK_Records_PriceSale CHECK(PriceSale>0),

)  



CREATE TABLE Users(
Id INT PRIMARY KEY IDENTITY,
FullName NVARCHAR(40) NOT NULL,
Login NVARCHAR(40) NOT NULL,
Password NVARCHAR(40) NOT NULL,
Money INT NOT NULL DEFAULT(500),

CONSTRAINT CK_Users_FullName CHECK(FullName<>' ' ),
CONSTRAINT UQ_Users_FullName UNIQUE(FullName),
CONSTRAINT CK_Users_Login CHECK(Login<>' '),
CONSTRAINT UQ_Users_Login UNIQUE(Login),
CONSTRAINT CK_Users_Password CHECK(Password<>' '),
CONSTRAINT CK_Users_Money CHECK(Money>=0)
)

CREATE TABLE Storages (
Id INT PRIMARY KEY IDENTITY,
RecordId INT NOT NULL,
Quantity INT NOT NULL,
CONSTRAINT FK_Storages_RecordId  FOREIGN KEY (RecordId ) REFERENCES Records(Id),
CONSTRAINT CK_Storages_Quantity CHECK(Quantity>0)
)

CREATE TABLE ClientBoughtGoods(
RecordId INT NOT NULL,
UserId INT NOT NULL,
CONSTRAINT FK_ClientBoughtGoods_RecordId  FOREIGN KEY (RecordId) REFERENCES Records(Id),
CONSTRAINT FK_ClientBoughtGoods_UserId FOREIGN KEY (UserId ) REFERENCES Users(Id),

)  


  