
-- 1. Create a stored procedure to encrypt a given text
-- Task: Write a stored procedure sp_encrypt_text that takes a plain text input (e.g., email or mobile number) and returns an encrypted version using PostgreSQL's pgcrypto extension.
 -- Use pgp_sym_encrypt(text, key) from pgcrypto.
CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE OR REPLACE FUNCTION fn_encrypt_text(mobile TEXT)
RETURNS BYTEA
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN pgp_sym_encrypt(mobile, 'ab1789');
END;
$$;

CREATE TABLE encryptedtbl (
    id SERIAL PRIMARY KEY,
    encrypted BYTEA
);


select encode(fn_encrypt_text('902529'),'base64')
select fn_encrypt_text('902529') 

INSERT INTO encryptedtbl (encrypted)
SELECT fn_encrypt_text('902529');

select * from encryptedtbl

--stored procedure
create or replace procedure proc_fn_encrypt_text( IN mobile TEXT, OUT encrypted BYTEA)
LANGUAGE plpgsql
AS $$
BEGIN
    encrypted := pgp_sym_encrypt(mobile, 'ab1789');
END;
$$;

DO $$
DECLARE
    encrypted_value BYTEA;
BEGIN
    CALL proc_fn_encrypt_text('1234567890', encrypted_value);
    RAISE NOTICE 'Encrypted value: %', encode(encrypted_value, 'hex');
END $$;
-----------------------------------------------------------------------------------------------------------------------
-- 2. Create a stored procedure to compare two encrypted texts
-- Task: Write a procedure sp_compare_encrypted that takes two encrypted values and checks if they decrypt to the same plain text.



CREATE OR REPLACE FUNCTION fn_decrypt_text(encrypted BYTEA)
RETURNS TEXT
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN pgp_sym_decrypt(encrypted, 'ab1789'::TEXT);
END;
$$;

CREATE OR REPLACE FUNCTION check_two_encs(text1 BYTEA, text2 BYTEA)
RETURNS TEXT
LANGUAGE plpgsql
AS $$
DECLARE
    decrypted1 TEXT;
    decrypted2 TEXT;
BEGIN
    decrypted1 := fn_decrypt_text(text1);
    decrypted2 := fn_decrypt_text(text2);

    IF decrypted1 = decrypted2 THEN
        RETURN 'Yes';
    ELSE
        RETURN 'No';
    END IF;
END;
$$;

SELECT check_two_encs(
    (SELECT encrypted FROM encryptedtbl WHERE id = 3),
    (SELECT encrypted FROM encryptedtbl WHERE id = 2)
);


select fn_decrypt_text((select encrypted from encryptedtbl where id=1 ));

---stored procedure
create or replace procedure proc_decrypt_text(IN encrypted BYTEA, OUT decrypted TEXT)
LANGUAGE plpgsql
AS $$
BEGIN
    decrypted := pgp_sym_decrypt(encrypted, 'ab1789'::TEXT);
END;
$$;

create or replace procedure proc_check_two_encs(IN text1 BYTEA,IN text2 BYTEA,OUT result TEXT)
LANGUAGE plpgsql
AS $$
DECLARE
    decrypted1 TEXT;
    decrypted2 TEXT;
BEGIN
    CALL proc_decrypt_text(text1, decrypted1);

    CALL proc_decrypt_text(text2, decrypted2);

    IF decrypted1 = decrypted2 THEN
        result := 'Yes';
    ELSE
        result := 'No';
    END IF;
END;
$$;

DO $$
DECLARE
    enc1 BYTEA;
    enc2 BYTEA;
    comparison_result TEXT;
BEGIN
	enc1 := pgp_sym_encrypt('test1', 'ab1789');
    enc2 := pgp_sym_encrypt('test1', 'ab1789');  

    CALL proc_check_two_encs(enc1, enc2, comparison_result);

	RAISE NOTICE 'Are the decrypted values equal? %', comparison_result;
END $$;
-----------------------------------------------------------------------------------------------------------------------
--  3. Create a stored procedure to partially mask a given text
-- Task: Write a procedure sp_mask_text that:

-- Shows only the first 2 and last 2 characters of the input string

-- Masks the rest with *

-- E.g., input: 'john.doe@example.com' → output: 'jo***************om'

create table emails(id serial primary key , email_id varchar(30))
insert into emails(email_id)
values('aaakndla@gmail.com')

select * from emails

create or replace function mask_emails(email_id varchar(30))
returns TEXT
LANGUAGE plpgsql
as
$$
DECLARE
    masked_email TEXT;
begin
	masked_email:=concat( substring(email_id,1,2),
	repeat('*', length(email_id) - 4),
	substring(email_id,length(email_id)-1,length(email_id))) ;
	
	RETURN masked_email;
end;
$$;

SELECT mask_emails('aasaoe@example.com');
	

select concat( substring(email_id,1,2),repeat('*', length(email_id) - 4),substring(email_id,length(email_id)-1,length(email_id))) 
as ids from emails

--stored procedure
CREATE OR REPLACE PROCEDURE proc_mask_emails(IN email_id VARCHAR(30),OUT masked_email TEXT)
LANGUAGE plpgsql
AS
$$
BEGIN
    masked_email := CONCAT(
        SUBSTRING(email_id, 1, 2),
        REPEAT('*', LENGTH(email_id) - 4),
        SUBSTRING(email_id, LENGTH(email_id) - 1, LENGTH(email_id))
    );
END;
$$;

DO $$
DECLARE
    email VARCHAR(30) := 'john@gmail.com';
    masked TEXT;
BEGIN
    CALL proc_mask_emails(email, masked);
    RAISE NOTICE 'Masked email: %', masked;
END $$;


-----------------------------------------------------------------------------------------------------------------------
-- 4. Create a procedure to insert into customer with encrypted email and masked name
-- Task:

-- Call sp_encrypt_text for email

-- Call sp_mask_text for first_name

-- Insert masked and encrypted values into the customer table

-- Use any valid address_id and store_id to satisfy FK constraints.
create or replace procedure insert_masked_data(store_id int,first_name text,last_name text,email text,address_id int)
as
$$
declare
	masked_fname text;
	masked_lname text;
	masked_email text;
begin
	masked_fname:=mask_emails(first_name);
	masked_lname:=mask_emails(last_name);
	masked_email:= fn_encrypt_text(email);
	insert into customer(store_id,first_name,last_name,email,address_id)
	values(store_id,masked_fname,masked_lname,masked_email,address_id);

	raise notice 'masked data inserted';
end;
$$
LANGUAGE plpgsql

call insert_masked_data(1,'testinga','testingb','kumardasas@g.cpm',2)

select * from customer order by customer_id desc
insert into customer(store_id,first_name,last_name,email,address_id)
values(1,'testinga','testingb','kumardasas@g.cpm',2)

delete from customer where customer_id=621
ALTER TABLE customer
ALTER COLUMN email TYPE VARCHAR(255);
-----------------------------------------------------------------------------------------------------------------------
-- 5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
-- Task:
-- Write sp_read_customer_masked() that:

-- Loops through all rows

-- Decrypts email

-- Displays customer_id, masked first name, and decrypted email

create or replace procedure display_mask_data()
as
$$
declare
	rec record;
	unmask_email text;
	display_data_cur cursor for
		select customer_id,first_name,email 
		from customer
		order by customer_id desc
		limit 2 ;
begin
	open display_data_cur;
	loop
		fetch display_data_cur into rec;
		exit when not found;
	
		unmask_email:=fn_decrypt_text(rec.email::BYTEA);
	
		raise notice 'customer_id:% ,first_name:%,email:%',rec.customer_id,rec.first_name,unmask_email;
	end loop;
	close display_data_cur;
end;
$$
LANGUAGE plpgsql

call display_mask_data()
	

