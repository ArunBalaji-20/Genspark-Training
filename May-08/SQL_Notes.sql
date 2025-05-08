  create table Products
(id int identity(1,1) constraint pk_productId primary key,
name nvarchar(100) not null,
details nvarchar(max))
go
create proc proc_InsertProduct(@pname nvarchar(100),@pdetails nvarchar(max))
as
begin
	insert into products(name,details) values(@pname,@pdetails)
end
go
proc_InsertProduct 'Laptop','{"brand":"Dell","spec":{"RAM":"16GB","CPU":"i5"}}'
go
select * from Products

select JSON_QUERY(details,'$.spec') from products;
  
  select * from products where 
  try_cast(json_value(details,'$.spec.CPU') as nvarchar(20)) ='i5'

  create proc proc_FilterProductsss(@pcpu varchar(20), @pcount int out)
  as
  begin
      set @pcount = (select count(*) from products where 
	  try_cast(json_value(details,'$.spec.CPU') as nvarchar(20)) =@pcpu)
  end

 begin
  declare @cnt int
 exec proc_FilterProductsss 'i5', @cnt out
  print concat('The number of computers is ',@cnt)
  end

 ---------------------------------

 create table people
(id int primary key,
name nvarchar(20),
age int)

create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
   declare @insertQuery nvarchar(max)

   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
   with(
   FIRSTROW =2,
   FIELDTERMINATOR='','',
   ROWTERMINATOR = ''\n'')'
   exec sp_executesql @insertQuery
end

proc_BulkInsert 'C:\Users\arunbalajib\Downloads\Data.csv'

select * from people

------------logging and try catch
create table BulkInsertLog
(LogId int identity(1,1) primary key,
FilePath nvarchar(1000),
status nvarchar(50) constraint chk_status Check(status in('Success','Failed')),
Message nvarchar(1000),
InsertedOn DateTime default GetDate())

create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
  Begin try
	   declare @insertQuery nvarchar(max)

	   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
	   with(
	   FIRSTROW =2,
	   FIELDTERMINATOR='','',
	   ROWTERMINATOR = ''\n'')'

	   exec sp_executesql @insertQuery

	   insert into BulkInsertLog(filepath,status,message)
	   values(@filepath,'Success','Bulk insert completed')
  end try
  begin catch
		 insert into BulkInsertLog(filepath,status,message)
		 values(@filepath,'Failed',Error_Message())
  END Catch
end

truncate table people

proc_BulkInsert 'C:\Users\arunbalajib\Downloads\Data.csv'

select * from BulkInsertLog

-----------CTE's
with cteAuthorsName
as
(select au_id,au_fname from authors)

select * from cteAuthorsName

with cteAuthors
as
(select au_id, concat(au_fname,' ',au_lname) author_name from authors)

select * from cteAuthors
--------------------------

declare @page int =2, @pageSize int=10;
with PaginatedBooks as
( select  title_id,title, price, ROW_Number() over (order by price desc) as RowNum
  from titles
)
select * from PaginatedBooks where rowNUm between((@page-1)*@pageSize) and (@page*@pageSize)

--create a sp that will take the page number and size as param and print the books

create or alter proc proc_paginated(@page int =1, @pageSize int=10)
as
begin
with PaginatedBooks as
( select  title_id,title, price, ROW_Number() over (order by price desc) as RowNum
  from titles
)
select * from PaginatedBooks where rowNUm between((@page-1)*@pageSize+1) and (@page*@pageSize)
end

exec proc_paginated 1,5
-----------------------------------------
--using offset
 select  title_id,title, price
  from titles
  order by price desc
  offset 10 rows fetch next 10 rows only
------------------------------------
-----functions-----------
--scalar functions----------

  create function  fn_CalculateTax(@baseprice float, @tax float)
  returns float
  as
  begin
     return (@baseprice +(@baseprice*@tax/100))
  end

  select dbo.fn_CalculateTax(1000,10)

  select title,dbo.fn_CalculateTax(price,12) from titles

  ------------------------------------
  --table value functions------------
create function fn_tableSample(@minprice float)
returns table
as
    return select title,price from titles where price>= @minprice

select * from dbo.fn_tableSample(10)

--older and slower but supports more logic
create function fn_tableSampleOld(@minprice float)
  returns @Result table(Book_Name nvarchar(100), price float)
  as
  begin
    insert into @Result select title,price from titles where price>= @minprice
    return 
end
select * from dbo.fn_tableSampleOld(10)

-------------------------
--cursor
select * from Products;
DECLARE vend_cursor CURSOR
    FOR SELECT * FROM Products
OPEN vend_cursor
FETCH NEXT FROM vend_cursor;
FETCH NEXT FROM vend_cursor;
FETCH NEXT FROM vend_cursor;
CLOSE vend_cursor;

-------------------------------------------------
-------tranactions----------
CREATE TABLE ValueTable (id INT);
BEGIN TRANSACTION;
    INSERT INTO ValueTable VALUES(12);
    INSERT INTO ValueTable VALUES(22);
ROLLBACK;
select * from ValueTable;