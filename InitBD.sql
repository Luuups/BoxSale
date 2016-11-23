--Drop database MYBOXSALE2
CREATE DATABASE MYBOXSALE

GO

USE MYBOXSALE

GO

CREATE TABLE EMPRESA
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(50) NOT NULL,
FechaCreacion DATETIME,
Direccion VARCHAR(300),
Telefono VARCHAR(12),
Activo BIT DEFAULT 1
)


GO

CREATE TABLE UNIDADMEDIDA
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(50) NOT NULL,
Abreviado VARCHAR(5),
Activo BIT DEFAULT 1
)

GO

CREATE TABLE PROVEEDOR
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(200) NOT NULL,
Direccion VARCHAR(300),
Telefono VARCHAR(12),
ContactoProveedor VARCHAR(30),
Activo BIT DEFAULT 1
)

GO

CREATE TABLE CATEGORIA
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(50),
Activo BIT DEFAULT 1
)

GO

CREATE TABLE PRODUCTO
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(50) NOT NULL,
PrecioMostrador FLOAT DEFAULT 0,
PrecioCompra FLOAT DEFAULt 0,
Existencia FLOAT,
StockMin FLOAT DEFAULT 0,
Imagen VARCHAR(150),
Activo BIT DEFAULT 1,
UnidadId INT,
ProveedorId INT,
CategoriaId INT,
FOREIGN KEY(UnidadId) REFERENCES UNIDADMEDIDA,
FOREIGN KEY(ProveedorId) REFERENCES PROVEEDOR,
FOREIGN KEY(CategoriaId) REFERENCES CATEGORIA
)

GO

CREATE TABLE USUARIO
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(30) NOT NULL,
Apellidos VARCHAR(30) NOT NULL,
NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
[Password] varchar(50) NOT NULL,
Telefono VARCHAR(12),
Direccion VARCHAR(300),
FechaAlta DATETIME,
EmpresaId INT,
Activo BIT DEFAULT 1,
FOREIGN KEY(EmpresaId) REFERENCES EMPRESA
)

GO

CREATE TABLE ROLES
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(50),
Activo BIT DEFAULT 1
)

GO

CREATE TABLE USUARIOROLES
(
Id INT PRIMARY KEY IDENTITY,
UserId INT,
RolId INT,
FOREIGN KEY(UserId) REFERENCES USUARIO,
FOREIGN KEY(RolId) REFERENCES ROLES,
Activo BIT DEFAULT 1
)

GO

CREATE TABLE TIPOPAGO
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(50),
Activo BIT DEFAULT 1
)


GO

CREATE TABLE TIPOPROMOCION
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(100),
Activo BIT DEFAULT 0
)


GO

CREATE TABLE PROMOCION
(
Id INT PRIMARY KEY IDENTITY,
Nombre VARCHAR(150),
Descripcion VARCHAR(MAX),
TipoId INT,
ProductoId INT,
Activo BIT DEFAULT 1,
FOREIGN KEY(TipoId) REFERENCES TIPOPROMOCION,
FOREIGN KEY(ProductoId) REFERENCES PRODUCTO
)


GO

CREATE TABLE VENTA
(
Id INT PRIMARY KEY IDENTITY,
FechaVenta DATETIME,
SubTotal FLOAT,
PorcentajeInteres FLOAT,
Interes FLOAT,
Total FLOAT,
Cambio FLOAT, 
UsuarioId INT,
Activo BIT DEFAULT 1,
FOREIGN KEY(UsuarioId) REFERENCES USUARIO
)

GO

CREATE TABLE DETALLEVENTA
(
Id INT PRIMARY KEY IDENTITY,
VentaId INT,
ProductoId INT,
Cantidad INT,
Subtotal FLOAT,
Activo BIT DEFAULT 1,
FOREIGN KEY(VentaId) REFERENCES VENTA,
FOREIGN KEY(ProductoId) REFERENCES PRODUCTO
)



GO

CREATE TABLE VENTAPROMOCION
(
Id INT PRIMARY KEY IDENTITY,
VentaId INT,
PromocionId INT
)


GO

CREATE TABLE PAGO
(
Id INT PRIMARY KEY IDENTITY,
VentaId INT,
TipoId INT,
Monto FLOAT,
Activo BIT DEFAULT 1,
FOREIGN KEY(VentaId) REFERENCES VENTA
)
