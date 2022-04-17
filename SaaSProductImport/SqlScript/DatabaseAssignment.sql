--# SQL Test Assignment

--Attached is a mysqldump of a database to be used during the test.

--Below are the questions for this test. Please enter a full, complete, working SQL statement under each question. We do not want the answer to the question. We want the SQL command to derive the answer. We will copy/paste these commands to test the validity of the answer.
--**Example:**

-- Please return at least first_name and last_name

SELECT first_name, last_name FROM users;


------

--**— Test Starts Here —**

--1. Select users whose id is either 3,2 or 4
-- Please return at least: all user fields
select * from users where id in (2,3,4)

--2. Count how many basic and premium listings each active user has
-- Please return at least: first_name, last_name, basic, premium
select usr.first_name,usr.last_name, 
sum(case when lst.status=2 then 1 else 0 end) 'Basic',
sum(case when lst.status=3 then 1 else 0 end) 'Premium'
from users usr
inner join listings lst
on usr.id=lst.user_id and usr.status=2
group by usr.first_name,usr.last_name

--3. Show the same count as before but only if they have at least ONE premium listing
-- Please return at least: first_name, last_name, basic, premium
select usr.first_name,usr.last_name, 
sum(case when lst.status=2 then 1 else 0 end) 'Basic',
sum(case when lst.status=3 then 1 else 0 end) 'Premium'
from users usr
inner join listings lst
on usr.id=lst.user_id and usr.status=2
group by usr.first_name,usr.last_name
having sum(case when lst.status=3 then 1 else 0 end)>0

--4. How much revenue has each active vendor made in 2013
-- Please return at least: first_name, last_name, currency, revenue
select usr.first_name,usr.last_name,clk.currency,sum(price) revenue from users usr
inner join listings lst on lst.user_id=usr.id
and usr.status=2
inner join clicks clk on clk.listing_id=lst.id
and year(clk.created)=2013
group by usr.first_name,usr.last_name,clk.currency

--5. Insert a new click for listing id 3, at $4.00
-- Find out the id of this new click. Please return at least: id
insert into clicks (listing_id,price)
Values(3,4.00)
select @@IDENTITY

--6. Show listings that have not received a click in 2013
-- Please return at least: listing_name
select lst.name
from listings lst 
left join clicks clk on clk.listing_id=lst.id
group by lst.id,lst.name 
having Sum(case when Year(clk.created)=2013 then 1 else 0 end)=0


--7. For each year show number of listings clicked and number of vendors who owned these listings
-- Please return at least: date, total_listings_clicked, total_vendors_affected
select Year(clk.created) 'Date',count(Distinct lst.id) 'total_listings_clicked'
,count(Distinct usr.id) 'total_vendors_affected' 
from listings lst
inner join clicks clk on clk.listing_id=lst.id
left join users usr on usr.id=lst.user_id
group by Year(clk.created)

--8. Return a comma separated string of listing names for all active vendors
-- Please return at least: first_name, last_name, listing_names
select usr.first_name, usr.last_name, 
STUFF((SELECT ',' + lst.Name
         FROM listings lst
  where lst.user_id=usr.id
         FOR XML PATH('')
         ), 1, 1, '') listing_names
from users usr
where usr.status=2