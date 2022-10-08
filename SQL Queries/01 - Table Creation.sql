--use DAY22DB
--drop database BookMateDB

CREATE DATABASE BookMateDB
USE BookMateDB

CREATE TABLE Admin (
	AUserName NVARCHAR(32) PRIMARY KEY,
	APassword NVARCHAR(32) NOT NULL
)

CREATE TABLE Users (
	UId INT PRIMARY KEY IDENTITY,
	UUserName NVARCHAR(32) NOT NULL UNIQUE,
	UPassword NVARCHAR(32) NOT NULL,
	UFName NVARCHAR(50) NOT NULL,
	ULName NVARCHAR(50) NOT NULL,
	UDOB DATE NOT NULL,
	UEmail NVARCHAR(100) NOT NULL UNIQUE,
	UPhone NVARCHAR(10) NOT NULL UNIQUE CHECK(UPhone LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)

CREATE TABLE Address (
	AId INT PRIMARY KEY IDENTITY,
	UId INT FOREIGN KEY REFERENCES Users(UId) ON DELETE CASCADE,
	AAddressL1 NVARCHAR(100) NOT NULL,
	AAddressL2 NVARCHAR(100),
	ACity NVARCHAR(50) NOT NULL,
	ADistrict NVARCHAR(50) NOT NULL,
	AState NVARCHAR(50) NOT NULL,
	AEmail1 NVARCHAR(100) NOT NULL,
	AEmail2 NVARCHAR(100),
	APhone1 NVARCHAR(10) NOT NULL CHECK(APhone1 LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	APhone2 NVARCHAR(10) CHECK(APhone2 LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	APIN NVARCHAR(6) NOT NULL
)

CREATE TABLE Category (
	CCategoryName NVARCHAR(20) PRIMARY KEY
)

CREATE TABLE Books (
	BId INT PRIMARY KEY IDENTITY,
	BName NVARCHAR(200) NOT NULL,
	BAuthor NVARCHAR(200) NOT NULL,
	BPublisher NVARCHAR(200) NOT NULL,
	BYearOfPublication INT NOT NULL,
	BCategory NVARCHAR(20) FOREIGN KEY REFERENCES Category(CCategoryName) NOT NULL,
	BImage NVARCHAR(200) NOT NULL,
	BPrice FLOAT NOT NULL CHECK(BPrice > 0),
	BQuantity INT NOT NULL,
	BNPurchases INT NOT NULL DEFAULT 0,
	BRating FLOAT
)

CREATE TABLE Rating (
	BId INT FOREIGN KEY REFERENCES Books(BId) ON DELETE CASCADE NOT NULL,
	UId INT FOREIGN KEY REFERENCES Users(UId) ON DELETE CASCADE NOT NULL,
	RRating FLOAT CHECK(1 <= RRating AND RRating <= 5) NOT NULL,
	RComments NVARCHAR(500),
	CONSTRAINT PK_Rating PRIMARY KEY (BId, UId)
)

CREATE TABLE Purchase (
	PId INT PRIMARY KEY IDENTITY,
	UId INT FOREIGN KEY REFERENCES Users(UId),
	BId INT FOREIGN KEY REFERENCES Books(BId),
	AId INT FOREIGN KEY REFERENCES Address(AId),
	PDateTime DATETIME NOT NULL,
	PQuantity INT NOT NULL DEFAULT 1,
	PAmount FLOAT NOT NULL,
	FOREIGN KEY (PK_Rating) REFERENCES Rating(PK_Rating)
)

create table Cart (
	CId INT PRIMARY KEY IDENTITY,
	UId INT FOREIGN KEY REFERENCES Users(UId) ON DELETE CASCADE,
	BId INT FOREIGN KEY REFERENCES Books(BId) ON DELETE CASCADE
)


