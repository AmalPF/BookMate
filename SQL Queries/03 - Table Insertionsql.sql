
USE BookMateDB

-- Admin Table
INSERT INTO Admin VALUES ('admin', 'admin')
SELECT * FROM Admin

-- Users Table
insert into Users values('Brijo','brijo@1234','Brijo','Prince','11/09/2000','brijo@gmail.com','9400419216')
insert into Users values('Abhinav','abhinav@1234','Abhinav','Krishna','12/24/1998','abhinav@gmail.com','7558832632')
insert into Users values('Fayaz','fayaz@1234','Fayaz','Azeem','10/10/2000','fayaz@yahoo.com','9998887770')
insert into Users values('abc','abc','Fayaz','Azeem','10/10/2000','fayaz1@yahoo.com','9998887771')
SELECT * FROM Users

-- Address Table
INSERT INTO Address VALUES
	(1, '1 Line1', '1 Line2', 'City1', 'District1', 'State1', 'user1.1@gmail.com', 'user1.2@gmail.com', '1234567890', '1234567891', '123456'),
	(1, '2 Line1', '2 Line2', 'City2', 'District1', 'State1', 'user1.1@gmail.com', 'user1.2@gmail.com', '1234567891', '1234567892', '123456'),
	(2, '1 Line1', '1 Line2', 'City3', 'District2', 'State2', 'user2.1@gmail.com', 'user2.2@gmail.com', '1234567892', '1234567893', '123456'),
	(2, '2 Line1', '2 Line2', 'City4', 'District2', 'State2', 'user2.1@gmail.com', 'user2.2@gmail.com', '1234567893', '1234567894', '123456')
SELECT * FROM Address

-- Category Table
insert into Category values ('Education'),('Autobiography'),('Travel'),('Sports'),('Romance'),('Drama'),
	('Detective'),('Thriller'),('Sci-Fi'),('Fiction'),('Comedy'),('Horror'),('Historical'),('Poetry')
SELECT * FROM Category

-- Books Table
--TRUNCATE TABLE BOOKS
insert into Books Values('Gullivers Travels','Jonathan','Hari kp',2000,'Drama','img/photo1',2300.85,2,45, 0)
insert into Books Values('Lovers','Nathan','kp',2010,'Romance','img/photo2',2240.85,3,35, 0)
insert into Books Values('Ers','Kaan','Jp',2011,'Romance','img/photo3',250,3,35, 0)
SELECT * FROM Books

-- Rating Table
--TRUNCATE TABLE RATING
insert into Rating values(1,1,1,'FeelGood')
insert into Rating values(1,2,1,'FeelGood')
insert into Rating values(1,3,5,'FeelGood')
insert into Rating values(2,1,4,'Excellent')
insert into Rating values(3,2,5,'FeelGood')
SELECT * FROM Rating
SELECT * FROM Books
DELETE FROM Rating WHERE RId = 1
SELECT * FROM Rating
SELECT * FROM Books
UPDATE Rating SET RRating = 3 WHERE RId = 2
SELECT * FROM Rating
SELECT * FROM Books

-- Purchase Table
insert into Purchase values(1,2,GETDATE(),2,234.5,1)
insert into Purchase values(2,1,GETDATE(),3,334.5,1)
insert into Purchase values(3,2,GETDATE(),5,134.5,1)
SELECT * FROM Purchase

-- Cart Table
insert into cart values(1,1)
insert into cart values(1,2)
insert into cart values(2,3)
insert into cart values(2,1)
insert into cart values(3,2)
SELECT * FROM Cart
