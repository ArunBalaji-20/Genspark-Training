-- Triggers
---------------------------------------------------------------------------------
-- Create a trigger to prevent inserting payments of zero or negative amount.
Create or replace function prev_zero_pay()
returns trigger
as $$
begin
	if NEW.amount <0 THEN	
		RAISE EXCEPTION  'payment amount is less than 0';
	END if;
	return NEW;
end;
$$ language plpgsql;

create trigger trg_prev_zero_pay
before insert
on payment
for each row 
execute function prev_zero_pay()

select * from payment where amount < 0;

insert into payment values(341,1,1,1,-5,NOW())
---------------------------------------------------------------------------------
-- Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.
create or replace function film_last_update()
returns trigger
as $$
	begin

		if NEW.title is distinct from OLD.title OR NEW.rental_rate IS DISTINCT FROM OLD.rental_rate THEN
			NEW.last_update:=NOW();
		end if;
		return new;
	end;
$$ language plpgsql;

create trigger trg_film_update
before update 
on film
for each row
execute function film_last_update()


select * from film where film_id=9

update film set title='devilllll' where film_id=9
---------------------------------------------------------------------------------
-- Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week.
create table rental_log
(film_id int,title varchar(20),rental_date timestamp);


select * from payment
select * from inventory

create or replace function fun_rental_log()
returns trigger
as $$
DECLARE
    film_id_val INT;
    film_title TEXT;
    rental_count INT;
    week_start DATE := date_trunc('week', NEW.rental_date)::DATE;
    week_end   DATE := week_start + INTERVAL '7 days';
	begin
		SELECT f.film_id, f.title
	    INTO film_id_val, film_title
	    FROM inventory i
	    JOIN film f ON i.film_id = f.film_id
	    WHERE i.inventory_id = NEW.inventory_id;

		SELECT COUNT(*) INTO rental_count
		FROM rental r
		JOIN inventory i ON r.inventory_id = i.inventory_id
		WHERE i.film_id = film_id_val
		AND r.rental_date >= week_start
        AND r.rental_date < week_end;

		if rental_count > 3 then
			 INSERT INTO rental_log (film_id, title, rental_date)
        	VALUES (film_id_val, film_title, NEW.rental_date);
		END IF;

        RETURN NEW;
	end;
$$ language plpgsql;

CREATE TRIGGER trg_rental_log
AFTER INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION fun_rental_log();

INSERT INTO rental (inventory_id, rental_date,customer_id,staff_id) VALUES (2, NOW(),1,1);

SELECT * FROM rental_log;
select * from rental order by rental_date desc

select * from film order by last_update desc
