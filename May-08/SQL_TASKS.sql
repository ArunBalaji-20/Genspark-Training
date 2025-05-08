--1) List all orders with the customer name and the employee who handled the order.

--(Join Orders, Customers, and Employees)

select c.ContactName as 'customer name' ,concat(FirstName,' ',LastName) as 'employee name' 
from customers c
join orders o on
c.CustomerID=o.CustomerID
join employees e on
e.EmployeeID=o.EmployeeID;

------------------------------------------------------------------------
--2) Get a list of products along with their category and supplier name.

--(Join Products, Categories, and Suppliers)

select p.ProductName, c.CategoryName , s.ContactName 
from Products p
join Categories c on
c.CategoryID=p.CategoryID
join Suppliers s on
s.SupplierID=p.SupplierID
order by 2;

-----------------------------------------------------------------------------------

--3) Show all orders and the products included in each order with quantity and unit price.

--(Join Orders, Order Details, Products)
select o.orderid ,p.productId,p.ProductName,od.quantity , od.unitPrice 
from  Orders o
join [Order Details] od on
o.OrderID=od.OrderID
join Products p on
p.ProductID=od.ProductID

------------------------------------------------------------------------------------------------
--4) List employees who report to other employees (manager-subordinate relationship).

--(Self join on Employees)

select Concat(e.FirstName,' ',e.LastName) as Employee_Name ,Concat(m.FirstName,' ',m.LastName) as Manager_Name 
from employees e
join employees m on
e.ReportsTo=m.EmployeeID;

-----------------------------------------------------------------------------------------------------------
--5) Display each customer and their total order count.

--(Join Customers and Orders, then GROUP BY)

select c.contactname as customer_name , count(o.orderid) as total_orders 
from Customers c
left join orders o on
c.CustomerID=o.CustomerID 
group by c.contactname;

-----------------------------------------------------------------------------------------
--6) Find the average unit price of products per category.

--Use AVG() with GROUP BY

select AVG(p.unitprice) as avg_price ,c.CategoryName 
from Products p
join Categories c on
p.CategoryID=c.CategoryID
group by CategoryName;

-------------------------------------------------------------------------------------
--7) List customers where the contact title starts with 'Owner'.

--Use LIKE or LEFT(ContactTitle, 5)

select * from Customers 
where ContactTitle like 'Owner%';

----------------------------------------------------------------
--8) Show the top 5 most expensive products.

--Use ORDER BY UnitPrice DESC and TOP 5

select TOP 5 productName,UnitPrice from Products order by UnitPrice desc;

----------------------------------------------------------------

--9) Return the total sales amount (quantity × unit price) per order.

--Use SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) and GROUP BY

select sum(quantity * unitprice) as total_sales_amount,orderid from [Order Details] group by orderid;

---------------------------------------------------------------------------------
--10) Create a stored procedure that returns all orders for a given customer ID.

--Input: @CustomerID

create or alter proc AllOrders(@pCusId nchar(5))
as
begin
	select * from Orders 
	where  CustomerID= @pCusId;
	
end

exec AllOrders 'VINET'

----------------------------------------------------------------------------------------
--11) Write a stored procedure that inserts a new product.

--Inputs: ProductName, SupplierID, CategoryID, UnitPrice, etc.

create or alter proc InsertDataProc(  @pProdName nvarchar(40),@pSupId int , @pCatId int,@punitPrice int, @pDiscon int)
as
begin
	insert into Products (ProductName,SupplierID,CategoryID,UnitPrice,Discontinued)
	values( @pProdName,@pSupId  , @pCatId ,@punitPrice , @pDiscon)
end

exec InsertDataProc 'test',1,1,50,0

-------------------------------------------------------------------------------------------------
--12) Create a stored procedure that returns total sales per employee.

--Join Orders, Order Details, and Employees

create or alter proc TotalSalesProc
as
begin
	select SUM(od.UnitPrice * od.Quantity ) as 'total sales' ,e.EmployeeID from employees e
	join Orders o on
	o.EmployeeID=e.EmployeeID
	join [Order Details] od on
	od.OrderID=o.OrderID
	group by e.EmployeeID
	order by e.EmployeeID;
end

exec TotalSalesProc

SELECT COUNT(DISTINCT o.OrderID) AS TotalOrders, e.EmployeeID
FROM Employees e
JOIN Orders o ON o.EmployeeID = e.EmployeeID
JOIN [Order Details] od ON od.OrderID = o.OrderID
GROUP BY e.EmployeeID;

---------------------------------------------------------------------------
--13) Use a CTE to rank products by unit price within each category.

--Use ROW_NUMBER() or RANK() with PARTITION BY CategoryID

WITH PRODRANK
AS
(
select ProductName,UnitPrice,
ROW_NUMBER() over (partition by CategoryID order by UnitPrice desc) AS PRODUCT_RANK 
from products)

select * from PRODRANK;


-------------------------------------------------------------------------------
--14) Create a CTE to calculate total revenue per product and filter products with revenue > 10,000

WITH ProductRevenue AS (
SELECT p.ProductID,p.ProductName,
SUM(od.UnitPrice * od.Quantity ) AS TotalRevenue
FROM Products p
JOIN [Order Details] od ON p.ProductID = od.ProductID
GROUP BY p.ProductID, p.ProductName
)
SELECT * FROM ProductRevenue
WHERE TotalRevenue > 10000 
order by ProductID;

-----------------------------------------------------------------------------------
--15) Use a CTE with recursion to display employee hierarchy.

--Start from top-level employee (ReportsTo IS NULL) and drill down

with EmployeeHierarchy
as
(
	select EmployeeID,
	CONCAT(FirstName,' ',LastName) as EmployeeName,
	ReportsTo,
	1 as Level
	from Employees
	where ReportsTo is NULL

	union all

	select e.EmployeeID,
	CONCAT(e.FirstName,' ',e.LastName) as EmployeeName,
	e.ReportsTo, 
	eh.Level + 1
    FROM Employees e
    INNER JOIN EmployeeHierarchy eh ON e.ReportsTo = eh.EmployeeID
)

select * from EmployeeHierarchy
order by level;