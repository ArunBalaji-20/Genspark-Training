create table EMP(empno INTEGER PRIMARY KEY,
empname VARCHAR(20) ,
salary INTEGER,
bossno INTEGER  FOREIGN KEY REFERENCES EMP(empno) NOT NULL);

create table Department(deptname varchar(30) PRIMARY KEY,
deptfloor INTEGER,
deptPhone INTEGER,
mgrid INTEGER FOREIGN KEY REFERENCES EMP(empno) NOT NULL);

alter table EMP add  deptname varchar(30) NOT NULL;

alter table EMP 
ADD CONSTRAINT fk_deptname 
FOREIGN KEY(deptname) REFERENCES Department(deptname);

create table item( itemname varchar(30) PRIMARY KEY,
itemtype varchar(30),
itemcolor varchar(20));



create table sales(salesno integer PRIMARY KEY,
saleqty INTEGER,
itemname varchar(30) FOREIGN KEY REFERENCES item(itemname) NOT NULL,
deptname varchar(30) FOREIGN KEY REFERENCES Department(deptname) NOT NULL);



ALTER TABLE EMP
ALTER COLUMN bossno INT NULL;

ALTER TABLE EMP
ALTER COLUMN deptname varchar(30) NULL;

ALTER TABLE EMP
DROP CONSTRAINT fk_deptname ;


insert into EMP values(1,
'Alice',75000,NULL,'Management'),
(2, 'Ned', 45000, 'Marketing', 1),
(3, 'Andrew', 25000, 'Marketing', 2),
(4, 'Clare', 22000, 'Marketing', 2),
(5, 'Todd', 38000, 'Accounting', 1),
(6, 'Nancy', 22000, 'Accounting', 5),
(7, 'Brier', 43000, 'Purchasing', 1),
(8, 'Sarah', 56000, 'Purchasing', 7),
(9, 'Sophia', 35000, 'Personnel', 1),
(10, 'Sanjay', 15000, 'Navigation', 3),
(11, 'Rita', 15000, 'Books', 4),
(12, 'Gigi', 16000, 'Clothes', 4),
(13, 'Maggie', 11000, 'Clothes', 4),
(14, 'Paul', 15000, 'Equipment', 3),
(15, 'James', 15000, 'Equipment', 3),
(16, 'Pat', 15000, 'Furniture', 3),
(17, 'Mark', 15000, 'Recreation', 3);


insert into Department values
('Management',5,34,1),
('Books',1,81,4 ),
('Clothes',	2,	24,4 ),
('Equipment',3,57,3 ),
('Furniture', 4, 14, 3),
('Navigation', 1, 41, 3),
('Recreation', 2, 29, 4),
('Accounting', 5, 35, 5),
('Purchasing', 5, 36, 7),
('Personnel', 5, 37, 9),
('Marketing', 5, 38, 2);


--adding fk constraint now after inserting data

alter table EMP 
ADD CONSTRAINT fk_deptname 
FOREIGN KEY(deptname) REFERENCES Department(deptname);

insert into item (itemname,itemtype,itemcolor)
values
('Pocket Knife-Nile','E','Brown'), 

('Pocket Knife-Avon','E','Brown') ,

('Compass','N',null),

('Geo positioning system','N',null),

('Elephant Polo stick','R','Bamboo' ),

('Camel Saddle','R','Brown' ),

('Sextant','N',null),

('Map Measure','N',null);

INSERT INTO item (itemname, itemtype, itemcolor)
VALUES
('Boots-snake proof', 'Footwear', 'Brown'),
('Pith Helmet', 'Headgear', 'Khaki'),
('Hat-polar Explorer', 'Headgear', 'White'),
('Exploring in 10 easy lessons', 'Book', 'Blue'),
('How to win foreign friends', 'Book', 'Green');


INSERT INTO sales (salesno, saleqty, itemname, deptname)
VALUES
(101, 2, 'Boots-snake proof', 'Clothes'),
(102, 1, 'Pith Helmet', 'Clothes'),
(103, 1, 'Sextant', 'Navigation'),
(104, 3, 'Hat-polar Explorer', 'Clothes'),
(105, 5, 'Pith Helmet', 'Equipment'),
(106, 2, 'Pocket Knife-Nile', 'Clothes'),
(107, 3, 'Pocket Knife-Nile', 'Recreation'),
(108, 1, 'Compass', 'Navigation'),
(109, 2, 'Geo positioning system', 'Navigation'),
(110, 5, 'Map Measure', 'Navigation'),
(111, 1, 'Geo positioning system', 'Books'),
(112, 1, 'Sextant', 'Books'),
(113, 3, 'Pocket Knife-Nile', 'Books'),
(114, 1, 'Pocket Knife-Nile', 'Navigation'),
(115, 1, 'Pocket Knife-Nile', 'Equipment'),
(116, 1, 'Sextant', 'Clothes'),
(117, 1, 'Sextant', 'Equipment'),
(118, 1, 'Sextant', 'Recreation'),
(119, 1, 'Sextant', 'Furniture'),
(120, 1, 'Pocket Knife-Nile', 'Furniture'),
(121, 1, 'Exploring in 10 easy lessons', 'Books'),
(122, 1, 'How to win foreign friends', 'Books'),
(123, 1, 'Compass', 'Books'),
(124, 1, 'Pith Helmet', 'Books'),
(125, 1, 'Elephant Polo stick', 'Recreation'),
(126, 1, 'Camel Saddle', 'Recreation');

select * from EMP;
select * from item;
select * from sales;
select * from Department;