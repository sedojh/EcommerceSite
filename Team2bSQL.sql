
create table Customers
(Id nvarchar(50) not null primary key, 
Name nvarchar(50) not null,
Username nvarchar(50) not null,
Password nvarchar(50) not null, 
Email nvarchar(50) not null, 
Address nvarchar(50) not null,
Postalcode int not null,
Phone int not null);

create table Products 
(Id nvarchar(50) not null primary key,
Productname nvarchar(50) not null, 
Description nvarchar(max) not null,
Price float not null);

create table Orders 
(Id nvarchar(50) not null primary key, 
CustomerId nvarchar(50) not null,
Totalprice float not null,
Orderdate nvarchar(50) not null,
Coupon nvarchar(50) null,
DiscountedPrice float null);

create table OrderQuantity
(OrderId nvarchar(50) not null,
ProductId nvarchar(50) not null,
CustomerId nvarchar(50) not null,
Quantity int not null);


create table OrderDetails
(CustomerId nvarchar(50) not null,
ProductId nvarchar(50) not null,
OrderId nvarchar(50) not null,
Activationcode nvarchar(50) not null, 
Price float not null);

create table Coupon
(Coupon nvarchar(50) not null primary key,
Discount float not null);



insert into Products
values ('p00001','Apex Legends','Apex Legends is a battle royale game developed by Respawn Entertainment and published by Electronic Arts. It was released for Microsoft Windows, PlayStation 4 and Xbox One on February 4, 2019, without any prior announcement or marketing.',
19.99);
insert into Products
values ('p00002','League of Legends','League of Legends is a multiplayer online battle arena video game developed and published by Riot Games for Microsoft Windows and macOS. The game is supported by microtransactions, and was inspired by the Warcraft III: The Frozen Throne mod, Defense of the Ancients.',
22.5);
insert into Products
values ('p00003','Grand Theft Auto V','Grand Theft Auto V is an action-adventure video game developed by Rockstar North and published by Rockstar Games. It was released in September 2013 for PlayStation 3 and Xbox 360, in November 2014 for PlayStation 4 and Xbox One, and in April 2015 for Microsoft Windows.',
25.72);
insert into Products
values ('p00004','The Elder Scrolls V: Skyrim','The Elder Scrolls V: Skyrim is an action role-playing video game developed by Bethesda Game Studios and published by Bethesda Softworks.',
29.99);
insert into Products
values ('p00005','Counter-Strike: Global Offensive','Counter-Strike: Global Offensive is a multiplayer first-person shooter video game developed by Hidden Path Entertainment and Valve Corporation.',
19.49);
insert into Products
values ('p00006','Far Cry 5','Far Cry 5 is a first-person shooter video game developed by Ubisoft Montreal and Ubisoft Toronto and published by Ubisoft for Microsoft Windows, PlayStation 4 and Xbox One. It is the standalone successor to the 2014 video game Far Cry 4, and the fifth main installment in the Far Cry series.',
23.78);
insert into Products
values('p00007','FIFA 2020','FIFA 20 is a football simulation video game published by Electronic Arts as part of the FIFA series. It is the 27th installment in the FIFA series, and was released on 27 September 2019 for Microsoft Windows, PlayStation 4, Xbox One, and Nintendo Switch.',
15.99);
insert into Products
values('p00008','Call of Duty: Black Ops 4','Call of Duty: Black Ops 4 is a multiplayer first-person shooter developed by Treyarch and published by Activision. It was released worldwide for Microsoft Windows, PlayStation 4, and Xbox One on October 12, 2018.',
18.57);
insert into Products
values('p00009','Mortal Kombat 11','Mortal Kombat 11 is a fighting video game developed by NetherRealm Studios and published by Warner Bros. Interactive Entertainment.',
22.64);
insert into Products
values('p00010','Resident Evil 2','Resident Evil 2 is a survival horror game developed and published by Capcom. Players control police officer Leon S. Kennedy and college student Claire Redfield as they attempt to escape from Raccoon City during a zombie apocalypse.',
11.24);
insert into Products
values('p00011','Dirt Rally 2.0','Dirt Rally 2.0 is a racing video game developed and published by Codemasters for Microsoft Windows, PlayStation 4 and Xbox One. It was released on February 26, 2019. The game is the thirteenth title in the Colin McRae Rally series and the seventh title to carry the Dirt name.',
14.23);
insert into Products
values('p00012','Dark Souls III','Dark Souls III is an action role-playing video game developed by FromSoftware and published by Bandai Namco Entertainment for PlayStation 4, Xbox One, and Microsoft Windows. An entry in the Souls series, Dark Souls III was released in Japan in March 2016 and worldwide in April 2016.',
8.43);
insert into Products
values('p00013','Asphalt 9: Legends','Asphalt 9: Legends is a racing video game developed by Gameloft Barcelona and published by Gameloft. Released on July 25, 2018, it is the ninth main installment in the Asphalt series.',
14.88);

insert into Coupon
values('DISC5','0.95'),('DISC10','0.9')






