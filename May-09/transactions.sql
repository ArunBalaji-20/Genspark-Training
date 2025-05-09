-- Transaction-Based Questions (5)
------------------------------------------------------------------------------------------
-- Write a transaction that inserts a customer and an initial rental in one atomic operation.

DO $$
DECLARE
    new_customer_id INT;
BEGIN
    BEGIN  -- Start transaction block

        -- Insert customer
        INSERT INTO customer (store_id, first_name, last_name, email, address_id, create_date)
        VALUES (1, 'John', 'Doe', 'john.doe@example.com', 5, CURRENT_DATE)
        RETURNING customer_id INTO new_customer_id;

        -- Insert rental
        INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
        VALUES (CURRENT_TIMESTAMP, 10, new_customer_id, 1);
    END;
END;
$$;

select * from customer
select * from rental
------------------------------------------------------------------------------------------
-- Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.
BEGIN;

UPDATE film SET title = 'Academy testingggg' WHERE film_id = 1;

INSERT INTO inventory (film_id, store_id)
VALUES (1, 1);

ROLLBACK;
select * from film where film_id=1
	

------------------------------------------------------------------------------------------
-- Create a transaction that transfers an inventory item from one store to another.

BEGIN;

UPDATE inventory
SET store_id = 2 
WHERE inventory_id = 1 
  AND store_id = 1;        

COMMIT;

select * from inventory where inventory_id=1
------------------------------------------------------------------------------------------
-- Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.
BEGIN;

UPDATE payment
SET amount = amount + 5
WHERE payment_id = 17503; --before executing amount=12.99 , after 17.99

SAVEPOINT after_first_update;

UPDATE payment
SET amount = amount + 10 --after exec it should be 22.99 but we rollback to savepoint so 17.99
WHERE payment_id = 17503;

ROLLBACK TO SAVEPOINT after_first_update;

COMMIT;

select * from payment where payment_id=17503;

------------------------------------------------------------------------------------------
-- Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
-- Procedure: get_overdue_rentals() that selects relevant columns.

BEGIN;

DELETE FROM payment
WHERE customer_id = 123;

DELETE FROM rental
WHERE customer_id = 123;

DELETE FROM customer
WHERE customer_id = 123;

COMMIT;

select * from customer where customer_id=123;

------------------------------------------------------------------------------------------




