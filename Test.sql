-------------------------------------------- MyEmail --------------------------------------------
create table testMyEmail ( MyEmailID int IDENTITY(1,1) PRIMARY KEY, email dbo.MyEmail);
insert into testMyEmail(email) values ('alancarry@gmail.com');
insert into testMyEmail(email) values ('dpy@yandex.ru');
insert into testMyEmail(email) values ('alancarry@tut.by');
select MyEmailID, email.ToString() as email from testMyEmail;

insert into testMyEmail(email) values ('lalalaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@tut.by');
insert into testMyEmail(email) values ('lululu@tutby');
insert into testMyEmail(email) values ('lololo#tut.by');

go
declare @MyEmailValue1 MyEmail;
select @MyEmailValue1 = email from dbo.testMyEmail where MyEmailID = 1;
declare @MyEmailValue2 dbo.MyEmail;
select @MyEmailValue2 = email from dbo.testMyEmail where MyEmailID = 2;
select MyEmail::[hasSameUser](@MyEmailValue1, @MyEmailValue2) as hasSameUser;
go

select MyEmailID, email.ToString() as email, email.isSameUser('alancarry@op.com') as isSameUser
from dbo.testMyEmail;

select email.ToString() as email from testMyEmail where MyEmailID = 2;
update dbo.testMyEmail set email.changeHost('ms.com') where MyEmailID = 2;
select email.ToString() as email from testMyEmail where MyEmailID = 2;
update dbo.testMyEmail set email.Username = 'changedHaha' where MyEmailID = 2;
select email.ToString() as email from testMyEmail where MyEmailID = 2;

drop table testMyEmail;






-------------------------------------------- QuadraticFunction --------------------------------------------
drop table testQuadraticFunction;

create table testQuadraticFunction ( QuadFunID int IDENTITY(1,1) PRIMARY KEY, QuadFun dbo.QuadraticFunction, MaxMin text );
insert into testQuadraticFunction(QuadFun) values ('1,3,3');
insert into testQuadraticFunction(QuadFun) values ('4.3,-0.05,7');
insert into testQuadraticFunction(QuadFun) values ('-2.5,5,6');
insert into testQuadraticFunction(QuadFun) values ('-2.5,5,6');
insert into testQuadraticFunction(QuadFun) values ('-2.5,5,6');
select QuadFunID, QuadFun.ToString() as QuadFun, MaxMin from testQuadraticFunction;

insert into testQuadraticFunction(QuadFun) values ('1,a,3'); 
insert into testQuadraticFunction(QuadFun) values ('1,3');

select QuadFunID, QuadFun.ToString() as QuadFun, QuadraticFunction::[isEquals](QuadFun, '1,3,3') as isEquals from dbo.testQuadraticFunction;

update dbo.testQuadraticFunction set MaxMin = QuadFun.countMaxMin();
select QuadFunID, QuadFun.ToString() as QuadFun, MaxMin from testQuadraticFunction;

drop table testQuadraticFunction;




-------------------------------------------- RGBColor --------------------------------------------


create table testRGBColor ( ColorID int IDENTITY(1,1) PRIMARY KEY, Color dbo.RGBColor );
insert into testRGBColor(Color) values ('(153, 190, 227)');
insert into testRGBColor(Color) values ('(22, 85, 244 )');
insert into testRGBColor(Color) values ('(22, 85,244)');
insert into testRGBColor(Color) values ('(255, 255,  255 )');
insert into testRGBColor(Color) values ('( 0,0,  0 )');
select ColorID, Color.ToString() as Color from dbo.testRGBColor;

insert into testRGBColor(Color) values ('( -1,0,  22 )');
insert into testRGBColor(Color) values ('( 48,274,175 )');


select ColorID, Color.ToString() as Color, RGBColor::[isEquals]('(22,85,244)', Color) as isEquals from dbo.testRGBColor;

update testRGBColor set Color = RGBColor::[add]('(50,75,150)', '(50,75,150)') where ColorID = 1;
select ColorID, Color.ToString() as Color from dbo.testRGBColor;

select tab1.Color.ToString() as Color1, tab2.Color.ToString() as Color2,
RGBColor::[isEquals](tab1.Color, tab2.Color) as areEquals
from dbo.testRGBColor tab1
left outer join dbo.testRGBColor tab2 on tab1.ColorID <> tab2.ColorID;

select ColorID, Color.ToString() from dbo.testRGBColor
where Color.G between 50 and 200;

select ColorID, Color.ToString() as Color from dbo.testRGBColor;
update dbo.testRGBColor set Color.R = 255 where Color.R < 30;
select ColorID, Color.ToString() as Color from dbo.testRGBColor;
drop table testRGBColor;






-------------------------------------------- CarPlate --------------------------------------------

create table testCarPalte ( PlateID int IDENTITY(1,1) PRIMARY KEY, Plate dbo.CarPlate );
insert into testCarPalte(Plate) values ('WA6642E');
insert into testCarPalte(Plate) values ('WI027HJ');
insert into testCarPalte(Plate) values ('KRAKRA');
insert into testCarPalte(Plate) values ('ERA75TM');
insert into testCarPalte(Plate) values ('SB8903R');
insert into testCarPalte(Plate) values ('KRA7TM');
select PlateID, Plate.ToString() as Plate from dbo.testCarPalte;

insert into testCarPalte(Plate) values ('KR AKRA');
insert into testCarPalte(Plate) values ('KRA12345');

select PlateID, Plate.ToString() as Plate, Plate.fromSameProvince('KZ123WW') as fromSameProvince
from dbo.testCarPalte;

select PlateID, Plate.ToString() as Plate
from dbo.testCarPalte where Plate.Province = 'Mazowieckie';

select PlateID, Plate.ToString() as Plate, Plate.Province as Province from dbo.testCarPalte;
drop table testCarPalte;




-------------------------------------------- Licence --------------------------------------------
create table testLicence ( LicenceID int IDENTITY(1,1) PRIMARY KEY, DriverLicence dbo.Licence );
insert into testLicence(DriverLicence) values ('B,05/05/2005');
insert into testLicence(DriverLicence) values ('AM,12/11/2018');
insert into testLicence(DriverLicence) values ('D,03/25/2013');
insert into testLicence(DriverLicence) values ('C1+E,10/16/1998');
select LicenceID, DriverLicence.ToString() as Licence from dbo.testLicence;

insert into testLicence(DriverLicence) values ('D,25/03/2013');
insert into testLicence(DriverLicence) values ('D,2503/2013');
insert into testLicence(DriverLicence) values ('12345678,03/25/2013');

update dbo.testLicence set DriverLicence.Date = '10/10/2001' where LicenceID = 1;
drop table testLicence;






-------------------------------------------- Violations --------------------------------------------


create table testViolation ( ViolationID int IDENTITY(1,1) PRIMARY KEY, Violtn dbo.Violation );
insert into testViolation(Violtn) values ('Przekroczony limit prędkości, 11/11/2020 01:22:54,500.1, Nie');
select ViolationID, Violtn.ToString() as Violation from dbo.testViolation;

select ViolationID, Violtn.ToString() as Violation from dbo.testViolation where ViolationID = 1;
update testViolation set Violtn.ToPay = Violtn.surcharge(50) where ViolationID = 1;
select ViolationID, Violtn.ToString() as Violation from dbo.testViolation where ViolationID = 1;

select ViolationID, Violtn.ToString() as Violation from dbo.testViolation where ViolationID = 1;
update testViolation set Violtn.IsPaid = 'true' where ViolationID = 1;
select ViolationID, Violtn.ToString() as Violation from dbo.testViolation where ViolationID = 1;
drop table testViolation;