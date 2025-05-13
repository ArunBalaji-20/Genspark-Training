-- Transactions 
--------------------------------------------------------------------------------------------------------
--Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.
DO $$
DECLARE
    new_customer_id INT;
    new_rental_id INT;
BEGIN
    -- Start transaction
    BEGIN

        INSERT INTO customer (store_id, first_name, last_name, address_id, create_date)
        VALUES (1, 'ram', 'k', 5, CURRENT_TIMESTAMP)
        RETURNING customer_id INTO new_customer_id;

        INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
        VALUES (CURRENT_TIMESTAMP, 1, new_customer_id, 1)
        RETURNING rental_id INTO new_rental_id;

        INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
        VALUES (new_customer_id, 1, new_rental_id, 4.99, CURRENT_TIMESTAMP);

    EXCEPTION WHEN OTHERS THEN
        ROLLBACK;
        RAISE NOTICE 'Transaction failed';
    END;
END;
$$ LANGUAGE plpgsql;

abort

select * from customer order by customer_id desc;

--------------------------------------------------------------------------------------------------------
--Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.
DO $$
BEGIN
    -- Start transaction
    
      
        INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
        VALUES (611, 1,1 , 4.99, CURRENT_TIMESTAMP);

		 INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
        VALUES (612, 1,1 , 4.99, CURRENT_TIMESTAMP); --invalid id 612

    EXCEPTION WHEN OTHERS THEN
        ROLLBACK;
        RAISE NOTICE 'Transaction failed';
   
END;
$$ LANGUAGE plpgsql;

select * from rental
select * from payment where customer_id=611
--------------------------------------------------------------------------------------------------------
-- Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.
Begin;
	update payment set amount=10 where payment_id = 32105;

	SAVEPOINT payment_one;

	update payment set amount=11 where payment_id = 32108;

	SAVEPOINT payment_two;

	update payment set amount=12 where payment_id=32105;

	ROLLBACK TO SAVEPOINT payment_one;
end;
select * from payment where payment_id in (32105,32108)


--------------------------------------------------------------------------------------------------------
--Perform a transaction that transfers inventory from one store to another (delete + insert) safely.

DO $$
Declare 
	film_id_val int;
begin
	--check
	select film_id into film_id_val
	from inventory
	where inventory_id=2 and store_id=1;

	if not found then
		raise notice 'item not found check inv id and store id';
	end if;

	--updating
	update inventory set store_id=2,last_update=now() where inventory_id=2 and store_id=1;

	raise notice 'values modified';

	Exception when others then
		raise notice 'transaction failed...';
		rollback;
end;
$$ language plpgsql

select * from inventory where inventory_id=2 ;

select * from store
--------------------------------------------------------------------------------------------------------
-- Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity.

DO $$
Begin

	delete from payment where customer_id=2;
	raise notice 'customer id 2 is deleted from payment table';

	delete from rental where customer_id=2;
	raise notice 'customer id 2 is deleted from rental table';
	
	delete from customer where customer_id=2;

	raise notice 'customer id 2 is deleted from customer table';

	Exception when others then
		raise notice 'transaction failed:';
		rollback;
end;
$$ language plpgsql


select * from customer
--------------------------------------------------------------------------------------------------------