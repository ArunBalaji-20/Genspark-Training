-- Cursor-Based Questions (5)
-- ---------------------------------------------------------------------------
-- 1) Write a cursor that loops through all films and prints titles longer than 120 minutes.

select * from film where length>120;

DO $$
DECLARE
    film_record RECORD;
    film_cursor CURSOR FOR
        SELECT title, length FROM film WHERE length > 120;
BEGIN
    OPEN film_cursor;

    LOOP
        FETCH film_cursor INTO film_record;
        EXIT WHEN NOT FOUND;

        RAISE NOTICE 'Title: %, Length: % minutes', film_record.title, film_record.length;
    END LOOP;

    CLOSE film_cursor;
END;
$$;


-------------------------------------------------------------------------------------------------------
-- Create a cursor that iterates through all customers and counts how many rentals each made.

Do $$
Declare
	custo_rents RECORD;
	rental_count INT;

	customer_rentals cursor for 
			SELECT customer_id, first_name, last_name  FROM customer;
BEGIN
	OPEN customer_rentals;

	Loop
		Fetch customer_rentals into custo_rents;
		EXIT WHEN NOT FOUND;

		 SELECT COUNT(*) INTO rental_count
        FROM rental
        WHERE customer_id = custo_rents.customer_id;

	     RAISE NOTICE 'Customer: % %, Rentals: %',
            custo_rents.first_name, custo_rents.last_name, rental_count;
    END LOOP;

    CLOSE customer_rentals;
END;
$$;
			
-------------------------------------------------------------------------------------------------------
-- Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.
CREATE OR REPLACE PROCEDURE update_rental_rates_for_low_rentals()
LANGUAGE plpgsql
AS $$
DECLARE
    film_rec RECORD;
    rental_count INT;

    film_cursor CURSOR FOR
        SELECT film_id, title, rental_rate
        FROM film;
BEGIN
    OPEN film_cursor;

    LOOP
        FETCH film_cursor INTO film_rec;
        EXIT WHEN NOT FOUND;

        -- Count how many rentals this film has
        SELECT COUNT(*) INTO rental_count
        FROM inventory i
        JOIN rental r ON i.inventory_id = r.inventory_id
        WHERE i.film_id = film_rec.film_id;

        -- If fewer than 5 rentals, increase rental rate by $1
        IF rental_count < 5 THEN
            UPDATE film
            SET rental_rate = rental_rate + 1
            WHERE film_id = film_rec.film_id;

            RAISE NOTICE 'Updated % (ID: %) - New rental rate: %',
                film_rec.title, film_rec.film_id, film_rec.rental_rate + 1;
        END IF;
    END LOOP;

    CLOSE film_cursor;
END;
$$;

CALL update_rental_rates_for_low_rentals();

-------------------------------------------------------------------------------------------------------

-- Create a function using a cursor that collects titles of all films from a particular category.

CREATE OR REPLACE FUNCTION get_film_titles_by_category(cat_name TEXT)
RETURNS TABLE (film_title TEXT)
LANGUAGE plpgsql
AS $$
DECLARE
    film_rec RECORD;
    film_cursor CURSOR FOR
        SELECT f.title
        FROM film f
        JOIN film_category fc ON f.film_id = fc.film_id
        JOIN category c ON fc.category_id = c.category_id
        WHERE c.name = cat_name;
BEGIN
    OPEN film_cursor;

    LOOP
        FETCH film_cursor INTO film_rec;
        EXIT WHEN NOT FOUND;

        -- Return each film title
        film_title := film_rec.title;
		-- film_length := film_rec.length;
        -- film_rating := film_rec.rating;
        RETURN NEXT;
    END LOOP;

    CLOSE film_cursor;
END;
$$;

SELECT * FROM get_film_titles_by_category('Comedy');

select * from category

----------------------------------------------------------------------------------------------------------------------
-- Loop through all stores and count how many distinct films are available in each store using a cursor.
-- select * from store
CREATE OR REPLACE Procedure DistinctFilms()
LANGUAGE plpgsql 
AS $$
DECLARE
	store_rec RECORD;
	film_count int;
	store_cursor cursor for
		select store_id from store;
BEGIN
	open store_cursor;

	LOOP
		FETCH store_cursor into store_rec;
		exit when not found ;

		select DISTINCT COUNT(i.film_id) into film_count
		from inventory i
		where i.store_id=store_rec.store_id;

		 RAISE NOTICE 'Store ID: %, Distinct Films: %',
            store_rec.store_id, film_count;
	end loop;

	close store_cursor;
end;
$$;

CALL DistinctFilms()


		
