CREATE DATABASE FoodWeightDB

Use FoodWeightDB

CREATE TABLE Diet(DietId int primary key, Name varchar(50))

CREATE TABLE Food(FoodId int primary key, Name varchar(50))

CREATE TABLE Breakfast(BreakFastId int primary key,FoodId int )

ALTER TABLE Breakfast ADD CONSTRAINT FK_Breakfast_Food FOREIGN KEY (FoodId) REFERENCES Food(FoodId);

CREATE TABLE Lunch(LunchId int primary key,FoodId int )

ALTER TABLE Lunch ADD CONSTRAINT FK_Lunch_Food FOREIGN KEY (FoodId) REFERENCES Food(FoodId);

CREATE TABLE Dinner(DinnerId int primary key,FoodId int )

ALTER TABLE Dinner ADD CONSTRAINT FK_Dinner_Food FOREIGN KEY (FoodId) REFERENCES Food(FoodId);

CREATE TABLE Consumer(ConsumerId int primary key,Name varchar(50), Age int, DOB date,Gender char,Weight int, Email varchar(50),Address varchar(150),DietId int,BreakFastId int,LunchId int,DinnerId int,Calories int)

ALTER TABLE Consumer ADD CONSTRAINT FK_Consumer_Breakfast FOREIGN KEY (BreakFastId) REFERENCES Breakfast(BreakFastId);

ALTER TABLE Consumer ADD CONSTRAINT FK_Consumer_Lunch FOREIGN KEY (LunchId) REFERENCES Lunch(LunchId);

ALTER TABLE Consumer ADD CONSTRAINT FK_Consumer_Dinner FOREIGN KEY (DinnerId) REFERENCES Dinner(DinnerId);

ALTER TABLE Consumer ADD CONSTRAINT FK_Consumer_Diet FOREIGN KEY (DietId) REFERENCES Diet(DietId);

INSERT INTO Food(Name) values('Idly'),('Dosa'),('Chapathi'),('Bread'),('Egg'),('Meals'),('Green Vegatables'),('Boiled Meat')

SELECT * from Food

INSERT INTO Breakfast(FoodId) values(1),(2),(3),(4),(5),(7),(8)

SELECT * from Breakfast

INSERT INTO Lunch(FoodId) values(5),(6),(7),(8)

SELECT * from Lunch

INSERT INTO Dinner(FoodId) values(1),(2),(3),(4),(5),(7),(8)

INSERT INTO Diet(Name) values('Paleo'),('Low-Carb'),('Vegan'),('Ultra Low Fat'),('Zone'),('Dukan')
SELECT * from Consumer
SELECT * from Diet
SELECT * from Food
SELECT * from Breakfast
SELECT * from Lunch
SELECT * from Dinner

INSERT INTO Consumer(Name,Age,DOB,Gender,Weight,Email,Address,DietId,BreakFastId,LunchId,DinnerId,Calories)
Values('Vijay Ragavan',26,'1995-04-28','M',76,'ragav@gmail.com','Udumalpet',5,5,2,5,120)

SELECT CON.ConsumerId,
CON.Name,
DIT.Name as Diet,
BF.BreakFastId,
LU.LunchId,
DIN.DinnerId
From dbo.Consumer as CON
INNER JOIN dbo.Diet as DIT on CON.DietId = DIT.DietId
INNER JOIN dbo.Breakfast as BF on CON.BreakFastId = BF.BreakFastId
INNER JOIN dbo.Lunch as LU on CON.LunchId = LU.LunchId
INNER JOIN dbo.Dinner as DIN on CON.DinnerId = DIN.DinnerId










