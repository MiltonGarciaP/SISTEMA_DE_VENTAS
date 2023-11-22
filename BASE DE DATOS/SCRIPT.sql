create database sistema

use sistema

create table rol 
(
idROL int primary key identity (1,1),
nombre varchar(50),
fechaRegistro datetime default getdate()
)

 create table menu 
 (
 idMenu int primary key identity (1,1),
 nombre varchar (50),
 icono varchar (50),
 url varchar (50)
 )

 create table MenuRol
 (
 idMenuRol int primary key identity (1,1),
 idMenu int references Menu(idMenu),
 idRol int references Rol(idRol)
 )

 create table Usuario 
 (
 idUsuario int primary key identity (1,1),
 nombreCompleto varchar(100),
 correo varchar (40),
 idRol int references Rol (idRol),
 clave varchar (40),
 esActivo bit default 1,
 fechaRegistro datetime default getdate()

 )

 create table Categoria
 (
 idCategoria int primary key identity (1,1),
 nombre varchar (50),
 esActivo bit default 1,
 fechaRegistro datetime default getdate()
 )

 create table Producto 
 (
 idProducto int primary key identity (1,1),
 nombre varchar (100),
 idCategoria int references categoria(idCategoria),
 stock int , 
 precio decimal (10,2),
 esActivo bit default 1,
 fechaRegistro datetime default getdate()
 )

 create table NumeroDocumento(
 idNumeroDocumento int primary key identity (1,1),
 ultimo_numero int not null,
 fechaRegistro datetime default getdate(),
 )

 create table venta 
 (
 idVenta int primary key identity (1,1),
 numeroDocumento varchar (40),
 tipoPago varchar(50),
 total decimal (10 , 2),
 fechaRegistro datetime default getdate()
 )

 create table DetalleVenta 
 (
 idDetalleVenta int primary key identity (1,1),
 idVenta int references venta(idVenta),
 idProducto int references Producto (idProducto),
 cantidad int,
 precio decimal (10,2),
 total decimal (10,2)
 )

 insert into rol(nombre) values ('Administrador'),
                                ('Empleado'),
								('Supervisor')

insert into Categoria (nombre, esActivo) values
('Laptops', 1),
('Monitores', 1),
('Teclados', 1),
('Auriculares', 1),
('Memorias', 1),
('Accesorios', 1)

insert into Menu(nombre,icono,url) values
('DashBoard','dashboard','/pages/dashboard'),
('Usuarios','group','/pages/usuarios'),
('Productos','collections_bookmark','/pages/productos'),
('Venta','currency_exchange','/pages/venta'),
('Historial Ventas','edit_note','/pages/historial_venta'),
('Reportes','receipt','/pages/reportes')

go

--menus para administrador
insert into MenuRol(idMenu,idRol) values
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1)

go

--menus para empleado
insert into MenuRol(idMenu,idRol) values
(4,2),
(5,2)

go

--menus para supervisor
insert into MenuRol(idMenu,idRol) values
(3,3),
(4,3),
(5,3),
(6,3)

go

insert into numerodocumento(ultimo_Numero,fechaRegistro) values
(0,getdate())
