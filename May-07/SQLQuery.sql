use pubs 
go



select * from titles;

select title , pub_name
from titles join publishers
on titles.pub_id = publishers.pub_id;

--print pub name who never publishsed
select pub_name from publishers where pub_id not in (select pub_id from titles);

select * from publishers;

select title , pub_name
from titles right join publishers
on titles.pub_id = publishers.pub_id;

--auth id for all books and authid and book name
select title , au_id 
from titles right join titleauthor
on titles.title_id=titleauthor.title_id;
--or
select au_id , title from titleauthor JOIN titles on titleauthor.title_id = titles.title_id;

select concat(au_fname,' ',au_lname)  Author_Name, title Book_Name from authors a
join titleauthor ta on a.au_id = ta.au_id
join titles t on ta.title_id = t.title_id

select concat(au_fname,' ',au_lname), title_id Author_Name from authors a
join titleauthor ta on a.au_id = ta.au_id
order by title_id
--or
select concat(au_fname,' ',au_lname), title_id Author_Name from authors a
join titleauthor ta on a.au_id = ta.au_id
order by 2

--Print the publisher's name, book name and the order date of the  books

select pub_name ,title , ord_date from titles ta
join publishers pb on ta.pub_id=pb.pub_id
join sales sa on sa.title_id=ta.title_id;

--Print the publisher name and the first book sale date for all the publishers
-- this won't print null values since inner join is used
select pub_name  , MIN(ord_date) from titles ta
join publishers pb on ta.pub_id=pb.pub_id
join sales sa on sa.title_id=ta.title_id
group by pub_name
order by 2 desc;

-- this will print null values 
SELECT pub_name Publisher_Name, min(ord_date) First_Order_Date
FROM publishers p left outer JOIN titles t ON p.pub_id = t.pub_id
left outer JOIN sales s ON s.title_id = t.title_id
group by  pub_name
order by 2 desc;
 
 --print the bookname and the store address of the sale
 select title bookName , stor_address store_address 
 from titles t join sales s on t.title_id=s.title_id
 join stores st on st.stor_id=s.stor_id;

create procedure proc_FirstProcedures
as
begin
	print 'Hello world'
end
go
exec proc_FirstProcedures

--example insert using stored proc
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

------------UPDATE JSON DATA
create proc proc_UpdateProductSpec(@pid int,@newvalue varchar(20))
as
begin
   update products set details = JSON_MODIFY(details, '$.spec.RAM',@newvalue) where id = @pid
end
GO
proc_UpdateProductSpec 1, '224GB'
GO
SELECT * FROM products;

---- to get individual values from json array
select id,name,JSON_VALUE(details,'$.brand') Brand_Name
from Products;

-----bulk insert using json data
 create table Posts
  (id int primary key,
  title nvarchar(100),
  user_id int,
  body nvarchar(max));

select * from Posts;

 create proc proc_BulInsertPosts(@jsondata nvarchar(max))
  as
  begin
		insert into Posts(user_id,id,title,body)
	  select userId,id,title,body from openjson(@jsondata)
	  with (userId int,id int, title varchar(100), body varchar(max))
  end

 proc_BulInsertPosts '
[
  {
    "userId": 1,
    "id": 1,
    "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
    "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
  },
  {
    "userId": 1,
    "id": 2,
    "title": "qui est esse",
    "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
  }]'

  select * from Posts;

  ---try cast
proc_InsertProduct 'Laptop','{"brand":"hp","spec":{"RAM":"16GB","CPU":"i7"}}'
    select * from products where 
  try_cast(json_value(details,'$.spec.CPU') as nvarchar(20)) ='i7';

--create a procedure that brings post by taking the user_id as parameter
 create proc postById(@pUserId int)
 as
 begin
	select * from Posts where user_id=@pUserId;
end

exec postById 1;