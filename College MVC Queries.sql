create database College
create table CollegeData(Id int Identity(1,1), Name varchar(max), Type varchar(max));
create table DepartmentData(Id int Identity(1,1), Name varchar(max));

--sp for getting details of all colleges
GO
create or alter procedure Getallcollege
AS
BEGIN
SELECT * from CollegeData
END
EXEC Getallcollege

--sp for getting details of college by Id
create or alter procedure Getsinglecollege 1
@Id int
AS
BEGIN
SELECT *from CollegeData where Id=@Id
END
ALTER TABLE Medicine ADD unique (MedicineName);

--sp for updating details of college data table
GO
create or alter procedure Updatecollege
@Id int, 
@Name varchar(max)
AS
BEGIN
update CollegeData set Name=@Name where Id=@Id
END
EXEC Updatecollege 1 ,'LPU'
ALTER TABLE registrations ADD unique (email)

