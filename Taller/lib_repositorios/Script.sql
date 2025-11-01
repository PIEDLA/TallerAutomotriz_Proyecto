CREATE DATABASE db_taller;
GO
USE db_taller;
GO

-- Tabla: Clientes
CREATE TABLE [Clientes] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [Apellido] NVARCHAR(50) NOT NULL,
    [Telefono] NVARCHAR(15) NOT NULL UNIQUE,
    [Correo] NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabla: Vehiculos
CREATE TABLE [Vehiculos] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_cliente] INT NOT NULL REFERENCES [Clientes] ([Id]),
    [Placa] NVARCHAR(10) NOT NULL UNIQUE,
    [Marca] NVARCHAR(30) NOT NULL,
    [Modelo] NVARCHAR(30) NOT NULL
);

-- Tabla: Servicios
CREATE TABLE [Servicios] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre_servicio] NVARCHAR(50) NOT NULL,
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Precio] DECIMAL(10,2) NOT NULL,
    [Duracion_aprox] NVARCHAR(50) NOT NULL,
);

-- Tabla: Sedes
CREATE TABLE [Sedes] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre_sede] NVARCHAR(50) NOT NULL,
    [Direccion] NVARCHAR(100) NOT NULL UNIQUE,
    [Telefono] NVARCHAR(15) NOT NULL UNIQUE,
    [Ciudad] NVARCHAR(50) NOT NULL
);

-- Tabla: Reservas
CREATE TABLE [Reservas] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_cliente] INT NOT NULL REFERENCES [Clientes] ([Id]),
    [Id_sede] INT NOT NULL REFERENCES [Sedes] ([Id]),
    [Fecha_reserva] DATE NOT NULL,
    [Estado] NVARCHAR(20) NOT NULL,
);

CREATE TABLE [Reserva_Servicio] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [PrecioServicio] DECIMAL(10,2) NOT NULL,
	[Servicio] INT NOT NULL REFERENCES [Servicios] ([Id]),
	[Reserva] INT NOT NULL REFERENCES [Reservas] ([Id])
);

-- Tabla: Empleados
CREATE TABLE [Empleados] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_sede] INT NOT NULL REFERENCES [Sedes] ([Id]),
    [Nombre] NVARCHAR(50) NOT NULL,
    [Apellido] NVARCHAR(50) NOT NULL,
    [Cargo] NVARCHAR(30) NOT NULL,
    [Telefono] NVARCHAR(15) NOT NULL UNIQUE,
);

-- Tabla: Diagnosticos
CREATE TABLE [Diagnosticos] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_vehiculo] INT NOT NULL REFERENCES [Vehiculos] ([Id]),
    [Id_empleado] INT NOT NULL REFERENCES [Empleados] ([Id]),
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Fecha] DATE NOT NULL,
);

-- Tabla: Reparaciones
CREATE TABLE [Reparaciones] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_diagnostico] INT NOT NULL REFERENCES [Diagnosticos] ([Id]),
    [Descripcion_trabajo] NVARCHAR(MAX) NOT NULL,
    [Costo_estimado] DECIMAL(10,2) NOT NULL,
    [Fecha_inicio] DATE NOT NULL,
);

-- Tabla: Facturas
CREATE TABLE [Facturas] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_cliente] INT NOT NULL REFERENCES [Clientes] ([Id]),
    [Id_reparacion] INT NOT NULL REFERENCES [Reparaciones] ([Id]),
    [Fecha_emision] DATE NOT NULL,
    [Total] DECIMAL(10,2) NOT NULL,
);

-- Tabla: Pagos
CREATE TABLE [Pagos] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_factura] INT NOT NULL REFERENCES [Facturas] ([Id]),
    [Monto_total] DECIMAL(10,2) NOT NULL,
    [Fecha_pago] DATE NOT NULL,
    [Estado] NVARCHAR(20) NOT NULL,
);

-- Tabla: Detalles_Pago
CREATE TABLE [Detalles_Pago] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Id_pago] INT NOT NULL REFERENCES [Pagos] ([Id]),
    [Metodo_pago] NVARCHAR(20) NOT NULL,
    [Monto] DECIMAL(10,2) NOT NULL,
    [Fecha_pago] DATE NOT NULL,
);

-- Tabla: Herramientas
CREATE TABLE [Herramientas] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [Tipo] NVARCHAR(30) NOT NULL,
    [Estado] NVARCHAR(20) NOT NULL,
    [Ubicacion] NVARCHAR(50) NOT NULL,
);

-- Tabla: Proveedores
CREATE TABLE [Proveedores] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL,
    [Telefono] NVARCHAR(15) NOT NULL UNIQUE,
    [Correo] NVARCHAR(50) NOT NULL UNIQUE,
    [Direccion] NVARCHAR(100) NOT NULL UNIQUE,
);

-- Tabla: Repuestos
CREATE TABLE [Repuestos] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_proveedor] INT NOT NULL REFERENCES [Proveedores] ([Id]),
    [Nombre_repuesto] NVARCHAR(50) NOT NULL,
    [Marca] NVARCHAR(30) NOT NULL,
    [Precio] DECIMAL(10,2) NOT NULL,
    [Stock] INT NOT NULL,
);

-- Tabla: Productos
CREATE TABLE [Productos] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre_producto] NVARCHAR(50) NOT NULL,
    [Precio] DECIMAL(10,2) NOT NULL,
    [Categoria] NVARCHAR(30) NOT NULL,
    [Stock] INT NOT NULL,
);

CREATE TABLE [Detalles_Servicio] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [PrecioServicio] DECIMAL(10,2) NOT NULL,
	[Servicio] INT NOT NULL REFERENCES [Servicios] ([Id]),
	[Factura] INT NOT NULL REFERENCES [Facturas] ([Id]),
);

CREATE TABLE [Detalles_Producto] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [PrecioProducto] DECIMAL(10,2) NOT NULL,
	[Producto] INT NOT NULL REFERENCES [Productos] ([Id]),
	[Factura] INT NOT NULL REFERENCES [Facturas] ([Id]),
);

CREATE TABLE [Detalles_Repuesto] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [PrecioRepuesto] DECIMAL(10,2) NOT NULL,
	[Repuesto] INT NOT NULL REFERENCES [Repuestos] ([Id]),
	[Factura] INT NOT NULL REFERENCES [Facturas] ([Id]),
);

-- Tabla intermedia: Reparacion_Herramienta
CREATE TABLE [Reparacion_Herramienta] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Id_reparacion] INT NOT NULL REFERENCES [Reparaciones] ([Id]),
    [Id_herramienta] INT NOT NULL REFERENCES [Herramientas] ([Id]),
);

CREATE TABLE [Usuarios] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Contraseña] NVARCHAR(250) NOT NULL,
    [Funcion] INT NOT NULL REFERENCES [Funciones] ([Id])
);

CREATE TABLE [Funciones] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(25) NOT NULL UNIQUE,
    [Permisos] NVARCHAR(MAX) NOT NULL
);

CREATE TABLE [Auditorias] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Usuario] INT NOT NULL REFERENCES [Usuarios] ([Id]),
    [Cambio] NVARCHAR(120) NOT NULL,
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Fecha] DATETIME NOT NULL
);

GO

-- Clientes
INSERT INTO Clientes (Nombre, Apellido, Telefono, Correo) VALUES
('Juan', 'Perez', '3001111111', 'juan.perez@mail.com'),
('Mar a', 'Gomez', '3002222222', 'maria.gomez@mail.com'),
('Carlos', 'Rodriguez', '3003333333', 'carlos.rodriguez@mail.com'),
('Ana', 'Martinez', '3004444444', 'ana.martinez@mail.com'),
('Luis', 'Hernendez', '3005555555', 'luis.hernandez@mail.com');


-- Vehiculos
INSERT INTO Vehiculos (Id_cliente, Placa, Marca, Modelo) VALUES
(1, 'ABC123', 'Toyota', 'Corolla'),
(2, 'DEF456', 'Mazda', 'CX-5'),
(3, 'GHI789', 'Chevrolet', 'Spark'),
(4, 'JKL012', 'Ford', 'Fiesta'),
(5, 'MNO345', 'Nissan', 'Sentra');


-- Servicios
INSERT INTO Servicios (Nombre_servicio, Descripcion, Precio, Duracion_aprox) VALUES
('Cambio de aceite', 'Incluye cambio de filtro', 150000, '30 minutos'),
('Alineacion', 'Alineacion de las 4 ruedas', 120000, '40 minutos'),
('Balanceo', 'Balanceo de neumaticos', 100000, '35 minutos'),
('Revision general', 'Chequeo completo del vehiculo', 200000, '1 hora'),
('Cambio de frenos', 'Pastillas delanteras y traseras', 250000, '50 minutos');


-- Sedes
INSERT INTO Sedes (Nombre_sede, Direccion, Telefono, Ciudad) VALUES
('Taller Centro', 'Cra 10 #20-30', '6011111111', 'Bogota'),
('Taller Norte', 'Av 19 #120-45', '6012222222', 'Bogota'),
('Taller Sur', 'Cl 80 #50-15', '6013333333', 'Cali'),
('Taller Occidente', 'Av Boyac  #70-50', '6014444444', 'Medellin'),
('Taller Oriente', 'Cl 50 #30-25', '6015555555', 'Barranquilla');


-- Reservas
INSERT INTO Reservas (Id_cliente, Id_sede, Fecha_reserva, Estado) VALUES
(1, 1, '2025-09-20', 'Pendiente'),
(2, 2, '2025-09-21', 'Confirmada'),
(3, 3, '2025-09-22', 'Pendiente'),
(4, 4, '2025-09-23', 'Cancelada'),
(5, 5, '2025-09-24', 'Confirmada');


-- Reserva_Servicio
INSERT INTO Reserva_Servicio (PrecioServicio ,Servicio, Reserva) VALUES
(150000.0,1, 1),
(120000.0, 2, 2),
(100000.0, 3, 3),
(200000.0, 4, 4),
(250000.0, 5, 5);


-- Empleados
INSERT INTO Empleados (Id_sede, Nombre, Apellido, Cargo, Telefono) VALUES
(1, 'Pedro', 'Suarez', 'Mecanico', '3101111111'),
(2, 'Laura', 'Torres', 'Recepcionista', '3102222222'),
(3, 'Andres', 'Mora', 'Mecanico', '3103333333'),
(4, 'Camila', 'Rios', 'Administrador', '3104444444'),
(5, 'Felipe', 'Vargas', 'Mecanico', '3105555555');


-- Diagnosticos
INSERT INTO Diagnosticos (Id_vehiculo, Id_empleado, Descripcion, Fecha) VALUES
(1, 1, 'Revision de frenos delanteros', GETDATE()),
(2, 3, 'Cambio de bateria recomendado', GETDATE()),
(3, 5, 'Motor con ruido inusual', GETDATE()),
(4, 1, 'Neumaticos desgastados', GETDATE()),
(5, 3, 'Chequeo de suspension', GETDATE());


-- Reparaciones
INSERT INTO Reparaciones (Id_diagnostico, Descripcion_trabajo, Costo_estimado, Fecha_inicio) VALUES
(1, 'Cambio de pastillas de freno', 300000, GETDATE()),
(2, 'Instalacion de bateria nueva', 400000, GETDATE()),
(3, 'Ajuste de motor', 500000, GETDATE()),
(4, 'Cambio de llantas delanteras', 600000, GETDATE()),
(5, 'Cambio de amortiguadores', 700000, GETDATE());


-- Facturas
INSERT INTO Facturas (Id_cliente, Id_reparacion, Fecha_emision, Total) VALUES
(1, 1, GETDATE(), 350000),
(2, 2, GETDATE(), 450000),
(3, 3, GETDATE(), 550000),
(4, 4, GETDATE(), 650000),
(5, 5, GETDATE(), 750000);


-- Pagos
INSERT INTO Pagos (Id_factura, Monto_total, Fecha_pago, Estado) VALUES
(1, 350000, GETDATE(), 'Pagado'),
(2, 450000, GETDATE(), 'Pendiente'),
(3, 550000, GETDATE(), 'Pagado'),
(4, 650000, GETDATE(), 'Pagado'),
(5, 750000, GETDATE(), 'Pendiente');


-- Detalles_Pago
INSERT INTO Detalles_Pago (Id_pago, Metodo_pago, Monto, Fecha_pago) VALUES
(1, 'Tarjeta', 350000, GETDATE()),
(2, 'Efectivo', 200000, GETDATE()),
(2, 'Tarjeta', 250000, GETDATE()),
(3, 'Transferencia', 550000, GETDATE()),
(5, 'Efectivo', 750000, GETDATE());


-- Herramientas
INSERT INTO Herramientas (Nombre, Tipo, Estado, Ubicacion) VALUES
('Llave inglesa', 'Manual', 'Disponible', 'Bodega A'),
('Gato hidraulico', 'Mecanico', 'En uso', 'Area reparaciones'),
('Compresor de aire', 'Electrico', 'Disponible', 'Bodega B'),
('Taladro', 'Electrico', 'En reparacion', 'Bodega C'),
('Esc ner OBD2', 'Electronico', 'Disponible', 'Oficina mecanicos');


-- Proveedores
INSERT INTO Proveedores (Nombre, Telefono, Correo, Direccion) VALUES
('Repuestos Express', '6016666666', 'ventas@repuestosx.com', 'Cl 10 #25-50'),
('Autopartes Bogota', '6017777777', 'info@autopartesbogota.com', 'Cra 15 #45-20'),
('Motores y mas', '6018888888', 'contacto@motoresymas.com', 'Av 68 #70-30'),
('Frenos Seguros', '6019999999', 'soporte@frenosseguros.com', 'Cl 100 #15-10'),
('Accesorios Cars', '6011010101', 'ventas@accesorioscars.com', 'Cra 7 #50-25');


-- Repuestos
INSERT INTO Repuestos (Id_proveedor, Nombre_repuesto, Marca, Precio, Stock) VALUES
(1, 'Pastillas de freno', 'Brembo', 200000, 50),
(2, 'Bateria 12V', 'Bosch', 400000, 30),
(3, 'Amortiguador', 'Monroe', 300000, 40),
(4, 'Filtro de aire', 'Mann', 80000, 100),
(5, 'Aceite 10W40', 'Castrol', 120000, 200);


-- Productos
INSERT INTO Productos (Nombre_producto, Precio, Categoria, Stock) VALUES
('Shampoo para autos', 30000, 'Cuidado', 100),
('Limpia parabrisas', 15000, 'Cuidado', 200),
('Aditivo para gasolina', 50000, 'Mantenimiento', 50),
('Ambientador', 10000, 'Accesorios', 300),
('Cera liquida', 40000, 'Cuidado', 80);


-- Detalles_Servicio
INSERT INTO Detalles_Servicio (PrecioServicio, Servicio, Factura) VALUES
(150000.0, 1, 1),
(120000.0, 2, 2),
(100000.0, 3, 3),
(200000.0, 4, 4),
(250000.0, 5, 5);


-- Detalles_Producto
INSERT INTO Detalles_Producto (PrecioProducto, Producto, Factura) VALUES
(30000.0, 1, 1),
(15000.0, 2, 2),
(50000.00, 3, 3),
(10000.0, 4, 4),
(40000.0, 5, 5);


-- Detalles_Repuesto
INSERT INTO Detalles_Repuesto (PrecioRepuesto, Repuesto, Factura) VALUES
(300000.0, 1, 1),
(400000.0, 2, 2),
(500000.0, 3, 3),
(600000.0, 4, 4),
(700000.0, 5, 5);


-- Reparacion_Herramienta
INSERT INTO Reparacion_Herramienta (Id_reparacion, Id_herramienta) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

INSERT INTO Usuarios(Nombre, Contraseña, Funcion) VALUES
('Prueba', '123lI', 1);

INSERT INTO Funciones(Nombre, Permisos) VALUES
('Admin', '{ñ}'),
('Mecanico', '{}'),
('Recepcionista', '{}');

GO