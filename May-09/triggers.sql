-- 1)	Write a trigger that logs whenever a new customer is inserted.
CREATE TABLE customer_log (
    log_id       SERIAL PRIMARY KEY,
    customer_id  INT,
    full_name    TEXT,
    email        TEXT,
    inserted_at  TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION log_new_customer_to_table()
RETURNS trigger AS $$
BEGIN
    INSERT INTO customer_log (customer_id, full_name, email)
    VALUES (
        NEW.customer_id,
        CONCAT(NEW.first_name , ' ' , NEW.last_name),
        NEW.email
    );
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log_new_customer
AFTER INSERT ON customer
FOR EACH ROW
EXECUTE FUNCTION log_new_customer_to_table();

INSERT INTO customer (
    store_id, first_name, last_name, email, address_id,
    activebool, create_date, last_update, active
) VALUES (
    1, 'ram', 'kumar', 'ramk@gmail.com', 5,
    TRUE, CURRENT_DATE, CURRENT_DATE, 1
);

SELECT * FROM customer_log ORDER BY log_id DESC;

--------------------------------------------------------------------------------------------------
--2 Create a trigger that prevents inserting a payment of amount 0.
CREATE OR REPLACE FUNCTION prev_zero_payment()
RETURNS trigger AS $$
BEGIN
    IF NEW.amount = 0 THEN
        RAISE EXCEPTION 'Payment amount cannot be 0';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_prevent_zero_payment
BEFORE INSERT ON payment
FOR EACH ROW
EXECUTE FUNCTION prev_zero_payment();

INSERT INTO payment (
    customer_id, staff_id, rental_id, amount, payment_date
) VALUES (
    1, 1, 100, 0.00, CURRENT_TIMESTAMP
);

-----------------------------------------------------------------------
-- 3) Set up a trigger to automatically set last_update on the film table before update.
CREATE OR REPLACE FUNCTION set_last_update()
RETURNS trigger AS $$
BEGIN
    NEW.last_update := NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_set_last_update
BEFORE UPDATE ON film
FOR EACH ROW
EXECUTE FUNCTION set_last_update();

SELECT film_id, title, last_update FROM film WHERE film_id = 1;

UPDATE film
SET title = 'Updated Title'
WHERE film_id = 1;

SELECT film_id, title, last_update FROM film WHERE film_id = 1;

--------------------------------------------------------------------

-- Create a trigger to log changes in the inventory table (insert/delete).
CREATE TABLE inventory_log (
    log_id       SERIAL PRIMARY KEY,
    operation    TEXT,
    inventory_id INT,
    film_id      INT,
    store_id     INT,
    log_time     TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION log_inventory_changes()
RETURNS trigger AS $$
BEGIN
        INSERT INTO inventory_log (operation, inventory_id, film_id, store_id)
        VALUES ('INSERT', NEW.inventory_id, NEW.film_id, NEW.store_id);
        RETURN NEW;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_inventory_insert
AFTER INSERT ON inventory
FOR EACH ROW
EXECUTE FUNCTION log_inventory_changes();



INSERT INTO inventory (film_id, store_id)
VALUES (1, 1);



SELECT * FROM inventory_log ORDER BY log_id DESC;




-------------------------------------------------------------------------

-- Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.

CREATE OR REPLACE FUNCTION check_customer_balance()
RETURNS trigger AS $$
DECLARE
    total_paid NUMERIC;
BEGIN
    SELECT COALESCE(SUM(amount), 0)
    INTO total_paid
    FROM payment
    WHERE customer_id = NEW.customer_id;

    IF total_paid < 50 THEN
        RAISE EXCEPTION 'Rental denied: Customer % owes more than $50', NEW.customer_id;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_prevent_rental_if_owing
BEFORE INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION check_customer_balance();


INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
VALUES (NOW(), 1, 28, 1);  

select * from payment where customer_id=28
select * from rental where customer_id=28

