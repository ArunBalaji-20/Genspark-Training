   -- * `student_id (PK)`, `name`, `email`, `phone`
Create table students
(student_id serial primary key,
Name varchar(40) NOT NULL,
email varchar(50),
phone int unique);

insert into students(Name,email,phone)
values('bob','bob@gmail.com',2123873)

select * from students

create index idx_student_id on students(student_id);
create index idx_student_email on students(email);

--------------------------------------------------------------------------------
-- 2. **courses**
--    * `course_id (PK)`, `course_name`, `category`, `duration_days`

Create table courses
(course_id serial primary key,
course_name varchar(40) NOT NULL,
category varchar(50),
duration_days int);

insert into courses(course_name,category,duration_days)
values('java','programming',25)

select * from courses

create index idx_course_id on courses(course_id);
--------------------------------------------------------------------------------
-- 3. **trainers**

--    * `trainer_id (PK)`, `trainer_name`, `expertise`

Create table trainers
(trainer_id serial primary key,
trainer_name varchar(40) NOT NULL,
expertise varchar(50));

insert into trainers(trainer_name,expertise)
values('sharma','cloud')

select * from trainers

--------------------------------------------------------------------------------
-- 4. **enrollments**

--    * `enrollment_id (PK)`, `student_id (FK)`, `course_id (FK)`, `enroll_date`

Create table enrollments
(enrollment_id serial primary key,
student_id int ,
course_id int ,
enroll_date date,
foreign key(student_id) references students(student_id),
foreign key(course_id) references courses(course_id)
);

insert into enrollments(student_id,course_id,enroll_date)
values(6,2,now())

select * from enrollments


--------------------------------------------------------------------------------
-- 5. **certificates**

--    * `certificate_id (PK)`, `enrollment_id (FK)`, `issue_date`, `serial_no`

Create table certificates
(certificate_id serial primary key,
enrollment_id int ,
serial_no varchar(12) unique NOT NULL ,
issue_date date,
foreign key(enrollment_id) references enrollments(enrollment_id)
);

insert into certificates(enrollment_id,serial_no,issue_date)
values(2,'AB10103',now())

select * from certificates
--------------------------------------------------------------------------------
-- 6. **course\_trainers** (Many-to-Many if needed)

--    * `course_id`, `trainer_id`

create table course_trainers(course_id int,
trainer_id int,
foreign key(course_id) references courses(course_id),
foreign key(trainer_id) references trainers(trainer_id)
);

insert into course_trainers(course_id,trainer_id)
values(3,4);

select * from course_trainers

------------------------------------------------------------------------------------------------------------
-- Phase 3: SQL Joins Practice

-- Write queries to:

-- 1. List students and the courses they enrolled in
	select s.student_id , s.Name , c.course_id,c.course_name from students s
	join enrollments e on
	e.student_id = s.student_id
	join courses c on
	c.course_id=e.course_id
	order by s.student_id;
	
-- 2. Show students who received certificates with trainer names
		select s.Name ,c.serial_no,c.issue_date,t.trainer_name from certificates c
		join enrollments e on
		c.enrollment_id=e.enrollment_id
		join students s on
		s.student_id=e.student_id
		join course_trainers ct on
		e.course_id=ct.trainer_id
		join trainers t on
		t.trainer_id=ct.trainer_id;
	
-- 3. Count number of students per course
	select e.course_id,c.course_name,count(e.student_id) as Number_of_students 
	from enrollments e
	join courses c on
	c.course_id=e.course_id
	group by e.course_id,c.course_name
	order by e.course_id
	
------------------------------------------------------------------------------------------------------------
-- Phase 4: Functions & Stored Procedures

-- Function:

-- Create `get_certified_students(course_id INT)`
-- → Returns a list of students who completed the given course and received certificates.
		create or replace function get_certified_students(co_id INT)
		RETURNS TABLE (student_name varchar,course_name varchar,certificate_serial_no varchar)
		as
		$$
		begin
			return query
				select s.name,c.course_name,ct.serial_no 
				from enrollments e
				join certificates ct on
				e.enrollment_id = ct.enrollment_id
				join courses c on
				c.course_id=e.course_id
				join students s on
				s.student_id=e.student_id
				WHERE c.course_id =co_id;
		end;
		$$ LANGUAGE plpgsql;
		select * from get_certified_students(2)
		
-- Stored Procedure:

-- Create `sp_enroll_student(p_student_id, p_course_id)`
-- → Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).
	create or replace procedure sp_enroll_student(Pstudent_id int,Pcourse_id int,Pserial_no Text,Penroll_date date,Pis_completed boolean)
	as
	$$
	Declare
		new_enroll_id int;
	begin
		insert into enrollments(student_id,course_id,enroll_date)
		values(Pstudent_id,Pcourse_id,Penroll_date)
		returning enrollment_id INTO new_enroll_id;

		if Pis_completed then
			insert into certificates(enrollment_id,serial_no,issue_date)
			values(new_enroll_id,Pserial_no::varchar,now());
		end if;
	end;
	$$ language plpgsql;

	call sp_enroll_student(5 ,3 ,'CD10201',CURRENT_DATE,False)
	
	select * from certificates
	select * from enrollments
------------------------------------------------------------------------------------------------------------
-- Phase 5: Cursor

-- Use a cursor to:

-- * Loop through all students in a course
-- * Print name and email of those who do not yet have certificates
DO $$
Declare
	rec Record;
	NoCert_cur cursor for
		SELECT s.name, s.email,c.enrollment_id,e.enrollment_id as eenroll
		FROM enrollments e
		LEFT JOIN students s ON s.student_id = e.student_id
		LEFT JOIN certificates c ON c.enrollment_id = e.enrollment_id
		WHERE c.enrollment_id IS NULL;
begin
	open NoCert_cur ;

	Loop
		fetch NoCert_cur into rec;
		exit when not found;

		Raise Notice 'student Name:%,Student email:%',rec.name,rec.email;
	end loop;
	CLOSE NoCert_cur;
end;
$$ LANGUAGE plpgsql;		
	
	

-----------------------------------
-- Phase 6: Security & Roles

-- 1. Create a `readonly_user` role:

--    * Can run `SELECT` on `students`, `courses`, and `certificates`
--    * Cannot `INSERT`, `UPDATE`, or `DELETE`

		create role readonly_user;
		Grant select on students, courses, certificates to readonly_user;

		insert into students(Name,email,phone)
		values('test','test@gmail.com',212322873)
		SELECT current_database();
		SELECT current_user;
		SET ROLE readonly_user;
		SET ROLE postgres;

-- 2. Create a `data_entry_user` role:

--    * Can `INSERT` into `students`, `enrollments`
--    * Cannot modify certificates directly
		CREATE ROLE data_entry_user;
		GRANT INSERT on students,enrollments to data_entry_user;
		SET ROLE data_entry_user;

		
--------------------------------------

-- Phase 7: Transactions & Atomicity

-- Write a transaction block that:

-- * Enrolls a student
-- * Issues a certificate
-- * Fails if certificate generation fails (rollback)

-- ```sql
-- BEGIN;
-- -- insert into enrollments
-- -- insert into certificates
-- -- COMMIT or ROLLBACK on error
-- ```
		Do $$
		Declare
			new_enroll_id int;
		Begin
			insert into enrollments(student_id,course_id,enroll_date)
			values(5,2,now())
			returning enrollment_id into new_enroll_id;
			
			insert into certificates(enrollment_id,serial_no,issue_date)
			values(new_enroll_id,'AB10105',now());

			Raise Notice 'New student enrolled and certified';

			Exception When others then
				Rollback;
				raise notice 'Transaction failed:%',sqlerrm;
		end;
		$$ language plpgsql;

		select * from enrollments
		select * from certificates
---