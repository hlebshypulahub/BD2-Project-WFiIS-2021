create table Driver (id int IDENTITY(1,1) PRIMARY KEY, email MyEmail, motion_equation QuadraticFunction,
color RGBColor, plate CarPlate, licence Licence, violation Violation);

insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('alancarry@gmail.com', '4.3,-0.05,7', '(153, 190, 227)', 'WA6642E', 'B,05/05/2005', 'Przekroczenie prędkości, 11/11/2020 01:22:54,500.1, Nie');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('ivan.urgant@yandex.ru', '17.55,3.2,0.06', '(249,20,20)', 'KRA12FL', 'D,12/03/2013', 'Prowadzenie pojazdu bez uprawnienia, 07/05/2021 14:55:07,129, Nie');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('karolziolo@onet.pl', '2.3,-0.66,13.7', '(18,224,19)', 'SLA13H4', 'C1,03/27/2019', 'Uszkodzenie drogi publicznej, 06/12/2021 19:33:15,60.5, Nie');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('lukaling@yt.net', '23.3,1.4,18.42', '(124,4,23)', 'LU1612C', 'B,10/13/1985', 'Tamowanie lub utrudnianie ruchu, 06/13/2021 15:13:22,180, Tak');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('karagach@agh.pl', '14.4,0.004,-0.6', '(64,54,133)', 'PO4PG54', 'AM,06/15/2020', 'Prowadzenie pojazdu w stanie po użyciu alkoholu, 12/03/2021 04:22:14,330, Nie');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('piesek@dom.ru', '23.3,1.4,18.42', '(24,176,83)', 'SK683FA', 'B,11/12/2007', 'Prowadzenie nieoświetlonego pojazdu, 03/27/2020 12:00:59,109, Tak');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('alamakota@pol.sat', '14.33,-1.2,-2.2', '(12,0,255)', 'FZ8918K', 'C,04/01/2005', 'Samowolne uszkadzanie znaków, 10/13/2020 10:19:45,89, Nie');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('s1mple@play.pl', '13.4,3,2', '(255,255,8)', 'KWI427A', 'B1,03/27/2020', 'Prowadzenie pojazdu bez wymaganych dokumentów, 02/28/2021 23:01:16,140, Nie');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('katememe@gmail.com', '1,-2,-4.333', '(1,1,1)', 'WN5622L', 'B,04/12/1999', 'Nieudzielenie pomocy oferze wypadku, 03/09/2021 13:31:14,950.5, Tak');
insert into Driver (email, motion_equation, color, plate, licence, violation)
values ('pablo.xavi@qamo.es', '0.001,12.2,-3', '(255,12,233)', 'LU2007H', 'D,05/11/2004', 'Niestosowanie się do znaku lub polecenia w ruchu drogowym, 05/27/2020 19:17:44,320, Nie');

--select id, email.ToString() as email, licence.ToString() as licence, color.ToString() as color,
--plate.ToString() as plate, motion_equation.ToString() as motion_equation, violation.ToString() as violation from Driver;

--select id, email.ToString() as email, licence.ToString() as licence, color.ToString() as color,
--plate.ToString() as plate, motion_equation.ToString() as motion_equation, violation.ToString() as violation from Driver where id = 11;


