USE master;
GO
ALTER DATABASE TravelAgencyDB1 
SET SINGLE_USER 
WITH ROLLBACK IMMEDIATE;
GO

DROP database IF EXISTS TravelAgencyDB1;
GO
Create database TravelAgencyDB1
GO
use TravelAgencyDB1;

create table TOUR
(
 Tour_ID int primary key,
 Destination varchar(80) not null,
 Price int not null,
 Max_no int not null,
 Min_no int not null,
 Photo varchar(80),
 Start__date varchar(80) not null,
 End__date varchar(80) not null,
 Num_tickets int not null,
 Describtion varchar(max)
);


CREATE TABLE HOTEL
(
  Hotel_Id INT  PRIMARY KEY,
  Name VARCHAR(255) NOT NULL,
  Location VARCHAR(255) NOT NULL,
  room_price int,
  numofrooms int,
  
);

CREATE TABLE ACTIVITIES 
(
  Name VARCHAR(200)  PRIMARY KEY,
  Type VARCHAR(200) NOT NULL,
  Capacity INT NOT NULL,
  Price DECIMAL(10, 2) NOT NULL,
  Description varchar(max)
);

CREATE TABLE TOUR_ACTIV
(
    Tour_ID INT NOT NULL,
    Activ_Name VARCHAR(200) NOT NULL,
    FOREIGN KEY (Tour_ID) REFERENCES TOUR(Tour_ID)ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Activ_Name) REFERENCES ACTIVITIES(Name)ON DELETE CASCADE ON UPDATE CASCADE,
    PRIMARY KEY (Tour_ID, Activ_Name),
	dayofactivity int 
);

CREATE TABLE TOUR_HOTEL
(
Tour_Id INT not null,
Hotel_Id INT not null ,
Rooms INT NOT NULL,
PRIMARY KEY (Tour_Id, Hotel_Id),
FOREIGN KEY (Tour_Id) REFERENCES TOUR(Tour_ID)ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (Hotel_Id) REFERENCES HOTEL(Hotel_Id)ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE TELEPHONE_HOTEL
(
Hotel_Id INT ,
Telephone INT NOT NULL,
PRIMARY KEY (Hotel_Id, Telephone),
FOREIGN KEY (Hotel_Id) REFERENCES HOTEL(Hotel_Id)ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE PEARSON
(
    ID varchar(50) PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    Email VARCHAR(200) NOT NULL,
    Pass VARCHAR(50) NOT NULL,
	usertype int
    
);

CREATE TABLE TELEPHONE_PEARSON
(
    Per_ID varchar(50) NOT NULL,
    Per_Telephone VARCHAR(200) NOT NULL,
    FOREIGN KEY (Per_ID) REFERENCES PEARSON(ID)ON DELETE CASCADE ON UPDATE CASCADE,
    PRIMARY KEY (Per_ID,Per_Telephone)
);



CREATE TABLE EMPLOYEE
(
	Employee_ID varchar(50) NOT NULL,
	Role VARCHAR(200) NOT NULL,
    Salary DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES PEARSON(ID)ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (Employee_ID)
	
);

CREATE TABLE PAYMENT 
(
  Payment_ID INT NOT NULL,
  Methods VARCHAR(200) NOT NULL,
  Date DATE
  PRIMARY KEY (Payment_ID)
);

CREATE TABLE BOOK
(
    Tour_ID int NOT NULL,
    Pay_ID int NOT NULL,
    Custo_ID varchar(50) NOT NULL,
    FOREIGN KEY (Custo_ID) REFERENCES PEARSON(ID)ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Pay_ID) REFERENCES PAYMENT(Payment_ID)ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (Tour_ID) REFERENCES TOUR(Tour_ID)ON DELETE CASCADE ON UPDATE CASCADE,
    PRIMARY KEY (Pay_ID,Tour_ID,Custo_ID)
);
create table Messages
(
messages varchar(max),
name varchar(50),
contactway varchar(50)
);

