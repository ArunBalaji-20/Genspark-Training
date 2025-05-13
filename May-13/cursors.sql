-- Cursors 
--------------------------------------------------------------------------
-- Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.
create table rentals_summary(sumid serial primary key ,cusid int , name varchar(30),total_rentals int);

Do $$
declare
	rec record;
	customers_cur cursor for
		select c.customer_id,concat(c.first_name,' ',c.last_name) as name, count(r.rental_id) as Total_rentals 
		from customer c
		join rental r on
		c.customer_id= r.customer_id
		group by c.customer_id;
begin
	open customers_cur;
	
	loop
		fetch customers_cur into rec;
		exit when not found;

		insert into rentals_summary (cusid, name, total_rentals)
		values(rec.customer_id,rec.name,rec.Total_rentals);

	end loop;

	close customers_cur;
end;
$$;


select * from rentals_summary
select * from customer
select * from rental

--------------------------------------------------------------------------
-- Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.

Do $$
declare
	rec record;
	comedy_films_cur  cursor for
		select f.film_id,f.title  ,  count(r.rental_id) AS rental_count
		from film f
		join film_category fc on
		f.film_id=fc.film_id
		join category c on
		c.category_id=fc.category_id
		join inventory i ON f.film_id = i.film_id
		join rental r ON r.inventory_id = i.inventory_id
		where c.name like 'Comedy'
		group by f.title,f.film_id
		having count(r.rental_id) > 10;
		
begin
	open comedy_films_cur;

	loop
		fetch comedy_films_cur into rec;
		exit when not found;

		raise notice 'film_id:%, title:%, rental_count:%',rec.film_id,rec.title,rec.rental_count;
		
	end loop;
	close comedy_films_cur;
end;
$$
				
--------------------------------------------------------------------------
-- Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.
CREATE TABLE store_film_report (
    report_id SERIAL PRIMARY KEY,
    store_id INT,
    film_count INT
);


do $$
declare
    rec record;
    film_count int;
    store_cursor cursor for
        select distinct store_id FROM store;
begin
    open store_cursor;

    loop
        fetch store_cursor into rec;
		exit when not found;

        select count(distinct i.film_id)
        into film_count
        from inventory i
        where i.store_id = rec.store_id;

        -- Insert into report table
        insert into store_film_report (store_id, film_count)
        values (rec.store_id, film_count);

        raise notice 'Store ID: %, Film Count: %', rec.store_id, film_count;

    end loop;

    close store_cursor;
end;
$$ LANGUAGE plpgsql;

select * from store_film_report

select * from store
select * from inventory
select * from film
--------------------------------------------------------------------------

-- Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.

CREATE TABLE inactive_customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(45),
    last_name VARCHAR(45),
    last_rental_date DATE,
    marked_inactive_on TIMESTAMP
);

DO $$
DECLARE
    rec RECORD;
    six_months_ago DATE := CURRENT_DATE - INTERVAL '6 months';
    last_rental DATE;
BEGIN
    FOR rec IN 
        SELECT 
            c.customer_id, c.first_name, c.last_name
        FROM 
            customer c
        LEFT JOIN (
            SELECT customer_id, MAX(rental_date) AS last_rental_date
            FROM rental
            GROUP BY customer_id
        ) r ON c.customer_id = r.customer_id
        WHERE r.last_rental_date IS NULL OR r.last_rental_date < six_months_ago
    LOOP
        SELECT MAX(rental_date)
        INTO last_rental
        FROM rental
        WHERE customer_id = rec.customer_id;

        INSERT INTO inactive_customers (customer_id, first_name, last_name, last_rental_date,marked_inactive_on)
        VALUES (rec.customer_id, rec.first_name, rec.last_name, last_rental,now());
        
        RAISE NOTICE 'Inserted inactive customer: %, Last rental: %', rec.customer_id, last_rental;
    END LOOP;
END;
$$ LANGUAGE plpgsql;

select * from inactive_customers


--------------------------------------------------------------------------