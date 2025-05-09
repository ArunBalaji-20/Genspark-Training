-- 1) SELECT Queries
-- List all films with their length and rental rate, sorted by length descending.
-- Columns: title, length, rental_rate
select title ,length,rental_rate from film 
order by length desc
-------------------------------------------------------------------------------------------------------
-- 2)Find the top 5 customers who have rented the most films.
-- Hint: Use the rental and customer tables.
-- select * from rental
select CONCAT(first_name,' ',last_name) as Name, COUNT(rental_id) as total_rental from customer
join rental on
customer.customer_id=rental.customer_id
group by customer.customer_id
order by total_rental desc
limit 5;

-------------------------------------------------------------------------------------------------------
-- 3)Display all films that have never been rented.
-- Hint: Use LEFT JOIN between film and inventory → rental.

select f.film_id,f.title from film f
left join inventory i on
f.film_id=i.film_id
left join rental r on
i.inventory_id = r.inventory_id
WHERE r.rental_id IS NULL;
----------------------------------------------------------------------------------------------------------------
-- 4) JOIN Queries
-- List all actors who appeared in the film ‘Academy Dinosaur’.
-- Tables: film, film_actor, actor

select concat(a.first_name,' ',a.last_name) as ActorName,f.title from actor a
join film_actor fa on
a.actor_id=fa.actor_id 
join film f on
f.film_id = fa.film_id
where f.title='Academy Dinosaur';

------------------------------------------------------------------------------------------------
-- 5)List each customer along with the total number of rentals they made and the total amount paid.
-- Tables: customer, rental, payment

select CONCAT(c.first_name,' ',c.last_name) as Name, COUNT(r.rental_id) as total_rental, sum(p.amount) as total_amount from customer c
join rental r on
c.customer_id=r.customer_id
join payment p on
r.rental_id = p.rental_id
group by c.customer_id
ORDER BY total_rental DESC;

--------------------------------------------------------------------------------------------
-- 6)
-- CTE-Based Queries
-- Using a CTE, show the top 3 rented movies by number of rentals.
-- Columns: title, rental_count
with TopRentedMovies
AS
(select f.title, COUNT(rental_id) as total_rental from film f
join inventory i on
f.film_id=i.film_id
join rental r on
r.inventory_id = i.inventory_id
group by f.title
order by total_rental desc )

select * from TopRentedMovies limit 3;

-----------------------------------------------------------------------------------------------------------------
-- 7) Find customers who have rented more than the average number of films.
-- Use a CTE to compute the average rentals per customer, then filter.
WITH CustomerRentalCounts AS (
SELECT 
	c.customer_id,
	CONCAT(c.first_name, ' ', c.last_name) AS Name,
	COUNT(r.rental_id) AS rental_count
FROM customer c
JOIN rental r ON c.customer_id = r.customer_id
GROUP BY c.customer_id),

AverageRentals AS (
SELECT AVG(rental_count) AS avg_rentals
FROM CustomerRentalCounts)

SELECT crc.Name,crc.rental_count
FROM CustomerRentalCounts crc, AverageRentals ar
WHERE crc.rental_count > ar.avg_rentals
ORDER BY crc.rental_count DESC;

---------------------------------------------------------------------------------------
--  8)Function Questions
-- Write a function that returns the total number of rentals for a given customer ID.
-- Function: get_total_rentals(customer_id INT)
 CREATE OR REPLACE FUNCTION get_total_rentals(cust_id INT) 
 RETURNS INT AS 
 $$ 
 DECLARE total_rentals INT; 
 BEGIN 
 SELECT COUNT(*) INTO total_rentals FROM rental WHERE customer_id = cust_id; RETURN total_rentals; END; 
 $$ 
 LANGUAGE plpgsql

select get_total_rentals(21)

--------------------------------------------------------------------------------------------------
-- 9)Stored Procedure Questions
-- Write a stored procedure that updates the rental rate of a film by film ID and new rate.
-- Procedure: update_rental_rate(film_id INT, new_rate NUMERIC)

create or replace procedure update_rental_rate(pfilm_id INT, new_rate NUMERIC)
as $$
begin
	update  film set rental_rate = new_rate where film_id=pfilm_id;
end
$$
LANGUAGE plpgsql

CALL update_rental_rate (1 , 9)

select * from film where film_id=1
--------------------------------------------------------------------------
-- Write a procedure to list overdue rentals (return date is NULL and rental date older than 7 days).
-- Procedure: get_overdue_rentals() that selects relevant columns.

CREATE OR REPLACE PROCEDURE get_overdue_rentals()
LANGUAGE plpgsql
AS $$
BEGIN
    -- Create a temporary table to hold the overdue rentals
    CREATE TEMP TABLE IF NOT EXISTS overdue_rentals_temp AS
    SELECT 
        r.rental_id,
        CONCAT(c.first_name, ' ', c.last_name) AS customer_name,
        r.rental_date,
        f.title AS film_title
    FROM rental r
    JOIN customer c ON r.customer_id = c.customer_id
    JOIN inventory i ON r.inventory_id = i.inventory_id
    JOIN film f ON i.film_id = f.film_id
    WHERE r.return_date IS NULL
      AND r.rental_date < CURRENT_DATE - INTERVAL '7 days'
    ORDER BY r.rental_date;
END;
$$;

CALL get_overdue_rentals();

select * from overdue_rentals_temp

