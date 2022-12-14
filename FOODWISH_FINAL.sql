USE [master]
GO
/****** Object:  Database [foodwish]    Script Date: 6/11/2021 16:03:41 ******/
CREATE DATABASE [foodwish]
GO
ALTER DATABASE [foodwish] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [foodwish].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [foodwish] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [foodwish] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [foodwish] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [foodwish] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [foodwish] SET ARITHABORT OFF 
GO
ALTER DATABASE [foodwish] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [foodwish] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [foodwish] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [foodwish] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [foodwish] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [foodwish] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [foodwish] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [foodwish] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [foodwish] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [foodwish] SET  ENABLE_BROKER 
GO
ALTER DATABASE [foodwish] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [foodwish] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [foodwish] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [foodwish] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [foodwish] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [foodwish] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [foodwish] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [foodwish] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [foodwish] SET  MULTI_USER 
GO
ALTER DATABASE [foodwish] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [foodwish] SET DB_CHAINING OFF 
GO
ALTER DATABASE [foodwish] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [foodwish] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [foodwish] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [foodwish] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [foodwish] SET QUERY_STORE = OFF
GO
USE [foodwish]
GO
/****** Object:  UserDefinedFunction [dbo].[FNENCONTRARUSUARIO]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FNENCONTRARUSUARIO](
@nombre_usuario VARCHAR (50),
@contrasena VARCHAR (50))
RETURNS INT
AS
BEGIN
DECLARE @ENCONTRADO INT=0
SELECT
@ENCONTRADO=COUNT(id_usuario)
FROM Usuario
WHERE nombre_usuario=@nombre_usuario
AND CONVERT (VARCHAR, DECRYPTBYPASSPHRASE ('POE2021!',contrasena))=@contrasena
RETURN (@ENCONTRADO)
END
GO
/****** Object:  UserDefinedFunction [dbo].[FNENCONTRARUSUARIO2]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREAR FUNCION PARA BUSCAR USUARIOS
CREATE FUNCTION [dbo].[FNENCONTRARUSUARIO2](
	@nombre_usuario VARCHAR(50)
	
) 
RETURNS INT
AS
BEGIN
	--DECLARAR VARIABLE  LOCAL
	DECLARE @ENCONTRADO INT = 0;

	--CREAR LA CONSULTA PARA ENCONTRAR USUARIO
	SELECT 
		@ENCONTRADO = COUNT(*) 
	FROM Usuario
	WHERE
		nombre_usuario = @nombre_usuario
		
	RETURN (@ENCONTRADO);
END
GO
/****** Object:  UserDefinedFunction [dbo].[FNENCONTRARUSUARIO3]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREAR FUNCION PARA BUSCAR USUARIOS
CREATE FUNCTION [dbo].[FNENCONTRARUSUARIO3](
	@respuesta_usu VARCHAR(50)
) 
RETURNS INT
AS
BEGIN
	--DECLARAR VARIABLE  LOCAL
	DECLARE @ENCONTRADO INT = 0;

	--CREAR LA CONSULTA PARA ENCONTRAR USUARIO
	SELECT 
		@ENCONTRADO = COUNT(id_usuario) 
	FROM Usuario
	WHERE
		respuesta_usu= @respuesta_usu 
				
	RETURN (@ENCONTRADO)
END

GO
/****** Object:  Table [dbo].[Comida]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comida](
	[id_comida] [int] IDENTITY(1,1) NOT NULL,
	[nom_comida] [varchar](25) NOT NULL,
	[decrip_comida] [varchar](255) NOT NULL,
	[tipo_comida] [int] NOT NULL,
	[precio_comid] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Comida] PRIMARY KEY CLUSTERED 
(
	[id_comida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[id_carrito] [int] IDENTITY(1,1) NOT NULL,
	[comida_car] [int] NOT NULL,
	[usuario_car] [int] NOT NULL,
	[cantidad_car] [int] NOT NULL,
	[precio_car] [decimal](10, 2) NULL,
	[fact_detfact] [int] NOT NULL,
 CONSTRAINT [PK_Carrito] PRIMARY KEY CLUSTERED 
(
	[id_carrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_FacturaCliente]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[V_FacturaCliente]
as
select m.nom_comida, c.comida_car, c.cantidad_car, c.precio_car,c.fact_detfact from Carrito as c INNER JOIN Comida as m ON c.comida_car = m.id_comida
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[metod_fact] [int] NOT NULL,
	[usuario_fact] [int] NOT NULL,
	[total_fact] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metodo_pago]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metodo_pago](
	[id_metodo] [int] IDENTITY(1,1) NOT NULL,
	[nom_metodo] [varchar](25) NULL,
 CONSTRAINT [PK_Metodo] PRIMARY KEY CLUSTERED 
(
	[id_metodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_usuario] [varchar](50) NOT NULL,
	[tipo_usu] [int] NOT NULL,
	[id_pregunta] [int] NOT NULL,
	[respuesta_usu] [varchar](255) NOT NULL,
	[contrasena] [varbinary](1000) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_FacturaReports]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[V_FacturaReports]
as
select f.id_factura, m.nom_metodo, u.nombre_usuario, f.total_fact from Factura as f INNER JOIN Metodo_pago as m ON f.metod_fact = m.id_metodo INNER JOIN Usuario as u ON f.usuario_fact = u.id_usuario
GO
/****** Object:  View [dbo].[V_Ventas]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[V_Ventas]
as
select m.nom_comida, u.nombre_usuario, c.cantidad_car, c.precio_car, c.fact_detfact from Carrito as c INNER JOIN Comida as m ON c.comida_car = m.id_comida INNER JOIN Usuario as u ON c.usuario_car = u.id_usuario where usuario_car = usuario_car
GO
/****** Object:  Table [dbo].[Tipo_comida]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_comida](
	[id_tipocomida] [int] IDENTITY(1,1) NOT NULL,
	[nom_tipocomida] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Tipo_comida] PRIMARY KEY CLUSTERED 
(
	[id_tipocomida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Platos]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[Platos]
as
	select id_comida, nom_comida, decrip_comida, t.nom_tipocomida, precio_comid from Comida AS c INNER JOIN Tipo_comida AS t ON c.tipo_comida=t.id_tipocomida where c.tipo_comida = 1
GO
/****** Object:  View [dbo].[Bebidas]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[Bebidas]
as
	select id_comida, nom_comida, decrip_comida, t.nom_tipocomida, precio_comid from Comida AS c INNER JOIN Tipo_comida AS t ON c.tipo_comida=t.id_tipocomida WHERE c.tipo_comida = 5
GO
/****** Object:  View [dbo].[Complementos]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE view [dbo].[Complementos]
as
	select id_comida, nom_comida, decrip_comida, t.nom_tipocomida, precio_comid from Comida AS c INNER JOIN Tipo_comida AS t ON c.tipo_comida=t.id_tipocomida where c.tipo_comida = 2
GO
/****** Object:  View [dbo].[Postres]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[Postres]
as
	select id_comida, nom_comida, decrip_comida, t.nom_tipocomida, precio_comid from Comida AS c INNER JOIN Tipo_comida AS t ON c.tipo_comida=t.id_tipocomida where c.tipo_comida = 4
GO
/****** Object:  View [dbo].[V_carrito]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[V_carrito]
as
select c.id_carrito, m.nom_comida, c.comida_car, c.usuario_car, c.cantidad_car, c.precio_car, c.fact_detfact from Carrito as c INNER JOIN Comida as m ON c.comida_car = m.id_comida where usuario_car = usuario_car
GO
/****** Object:  Table [dbo].[Estado_mesa]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado_mesa](
	[id_estadomesa] [int] IDENTITY(1,1) NOT NULL,
	[nom_estadomesa] [varchar](25) NULL,
 CONSTRAINT [PK_Estado_mesa] PRIMARY KEY CLUSTERED 
(
	[id_estadomesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesa]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesa](
	[id_mesa] [int] IDENTITY(1,1) NOT NULL,
	[numero_mesa] [int] NOT NULL,
	[idestado_mesa] [int] NOT NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_Mesa] PRIMARY KEY CLUSTERED 
(
	[id_mesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_Mesa]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[V_Mesa]
as
select m.id_mesa, m.numero_mesa, e.nom_estadomesa, u.nombre_usuario from Mesa as m LEFT JOIN Usuario as u ON m.id_usuario = u.id_usuario INNER JOIN Estado_mesa as e ON m.idestado_mesa = e.id_estadomesa
GO
/****** Object:  Table [dbo].[Ingredientes]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredientes](
	[id_ingredientes] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[stock] [int] NOT NULL,
	[fk_proveedor] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_ingredientes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preguntas]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preguntas](
	[id_pregunta] [int] IDENTITY(1,1) NOT NULL,
	[pregunta] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Preguntas] PRIMARY KEY CLUSTERED 
(
	[id_pregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[id_proveedor] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[direccion] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receta]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receta](
	[id_receta] [int] NOT NULL,
	[nombre_receta] [varchar](50) NOT NULL,
	[tiempo_receta] [decimal](5, 2) NOT NULL,
	[costo_produccion] [decimal](6, 2) NOT NULL,
	[fk_ingredientes] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_receta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_usuario]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_usuario](
	[id_tipousu] [int] IDENTITY(1,1) NOT NULL,
	[nombre_tipousu] [varchar](25) NULL,
 CONSTRAINT [PK_Tipo_usuario] PRIMARY KEY CLUSTERED 
(
	[id_tipousu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Carrito] ON 

INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (1, 4, 16, 1, CAST(5.99 AS Decimal(10, 2)), 1)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (2, 6, 16, 1, CAST(6.99 AS Decimal(10, 2)), 2)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (3, 4, 16, 2, CAST(5.99 AS Decimal(10, 2)), 3)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (4, 4, 16, 1, CAST(5.99 AS Decimal(10, 2)), 4)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (5, 4, 16, 1, CAST(5.99 AS Decimal(10, 2)), 5)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (6, 4, 16, 2, CAST(5.99 AS Decimal(10, 2)), 6)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (7, 10, 16, 2, CAST(2.00 AS Decimal(10, 2)), 7)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (8, 8, 16, 2, CAST(1.00 AS Decimal(10, 2)), 8)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (9, 4, 16, 2, CAST(5.99 AS Decimal(10, 2)), 9)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (10, 4, 16, 2, CAST(5.99 AS Decimal(10, 2)), 10)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (11, 6, 16, 2, CAST(6.99 AS Decimal(10, 2)), 10)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (12, 13, 16, 2, CAST(1.00 AS Decimal(10, 2)), 10)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (13, 10, 16, 1, CAST(2.00 AS Decimal(10, 2)), 10)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (15, 9, 16, 1, CAST(1.00 AS Decimal(10, 2)), 10)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (16, 6, 16, 2, CAST(6.99 AS Decimal(10, 2)), 11)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (17, 9, 16, 2, CAST(1.00 AS Decimal(10, 2)), 11)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (18, 4, 17, 3, CAST(5.99 AS Decimal(10, 2)), 12)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (19, 12, 17, 2, CAST(1.00 AS Decimal(10, 2)), 12)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (20, 8, 17, 1, CAST(1.00 AS Decimal(10, 2)), 12)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (21, 10, 17, 2, CAST(2.00 AS Decimal(10, 2)), 12)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (22, 13, 17, 1, CAST(1.00 AS Decimal(10, 2)), 12)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (24, 6, 17, 5, CAST(6.99 AS Decimal(10, 2)), 13)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (25, 11, 17, 7, CAST(1.00 AS Decimal(10, 2)), 13)
INSERT [dbo].[Carrito] ([id_carrito], [comida_car], [usuario_car], [cantidad_car], [precio_car], [fact_detfact]) VALUES (26, 7, 17, 3, CAST(1.50 AS Decimal(10, 2)), 14)
SET IDENTITY_INSERT [dbo].[Carrito] OFF
GO
SET IDENTITY_INSERT [dbo].[Comida] ON 

INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (4, N'Pasta', N'Una rica comida italiana', 1, CAST(5.99 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (6, N'Hamburguesa', N'Hamburguesa estilo americana de 1 carne con queso.', 1, CAST(6.99 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (7, N'Hot Dog', N'Hot-Dog estilo americano', 1, CAST(1.50 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (8, N'Coca-Cola', N'Bebida de origen estadounidense de cola', 5, CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (9, N'7up', N'Bebida carbonatada sabor limon', 5, CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (10, N'Cupcake', N'pastelito sabor chocolate', 4, CAST(2.00 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (11, N'Herradura', N'pan dulce en forma de herradura', 4, CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (12, N'Papas fritas', N'papas freidas en aceite estilo francesas', 2, CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (13, N'Nachos', N'nachos estilo Tex-Mex', 2, CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Comida] ([id_comida], [nom_comida], [decrip_comida], [tipo_comida], [precio_comid]) VALUES (16, N'Baileys', N' Licor de Crema, hecho de una mezcla de crema de leche fresca, whisky irlandés, vainilla, cacao puro y azúcar.', 7, CAST(25.99 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Comida] OFF
GO
SET IDENTITY_INSERT [dbo].[Estado_mesa] ON 

INSERT [dbo].[Estado_mesa] ([id_estadomesa], [nom_estadomesa]) VALUES (1, N'VACIO')
INSERT [dbo].[Estado_mesa] ([id_estadomesa], [nom_estadomesa]) VALUES (2, N'OCUPADO')
SET IDENTITY_INSERT [dbo].[Estado_mesa] OFF
GO
SET IDENTITY_INSERT [dbo].[Factura] ON 

INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (1, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (2, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (3, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (4, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (5, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (6, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (7, 1, 16, CAST(4.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (8, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (9, 1, 16, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (10, 1, 16, CAST(30.96 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (11, 1, 16, CAST(15.98 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (12, 2, 17, CAST(25.97 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (13, 2, 17, CAST(41.95 AS Decimal(10, 2)))
INSERT [dbo].[Factura] ([id_factura], [metod_fact], [usuario_fact], [total_fact]) VALUES (14, 4, 17, CAST(4.50 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Factura] OFF
GO
SET IDENTITY_INSERT [dbo].[Mesa] ON 

INSERT [dbo].[Mesa] ([id_mesa], [numero_mesa], [idestado_mesa], [id_usuario]) VALUES (1, 1, 1, NULL)
INSERT [dbo].[Mesa] ([id_mesa], [numero_mesa], [idestado_mesa], [id_usuario]) VALUES (3, 2, 1, NULL)
SET IDENTITY_INSERT [dbo].[Mesa] OFF
GO
SET IDENTITY_INSERT [dbo].[Metodo_pago] ON 

INSERT [dbo].[Metodo_pago] ([id_metodo], [nom_metodo]) VALUES (1, N'Efectivo')
INSERT [dbo].[Metodo_pago] ([id_metodo], [nom_metodo]) VALUES (2, N'Tarjeta de credito')
INSERT [dbo].[Metodo_pago] ([id_metodo], [nom_metodo]) VALUES (4, N'Tarjeta debito')
INSERT [dbo].[Metodo_pago] ([id_metodo], [nom_metodo]) VALUES (5, N'Cupon Gratis')
INSERT [dbo].[Metodo_pago] ([id_metodo], [nom_metodo]) VALUES (6, N'Bitcoin Wallet')
SET IDENTITY_INSERT [dbo].[Metodo_pago] OFF
GO
SET IDENTITY_INSERT [dbo].[Preguntas] ON 

INSERT [dbo].[Preguntas] ([id_pregunta], [pregunta]) VALUES (1, N'¿Como se llama tu perro?')
INSERT [dbo].[Preguntas] ([id_pregunta], [pregunta]) VALUES (2, N'¿Como se llama tu padre?')
INSERT [dbo].[Preguntas] ([id_pregunta], [pregunta]) VALUES (3, N'¿Cual fue tu primer carro?')
INSERT [dbo].[Preguntas] ([id_pregunta], [pregunta]) VALUES (4, N'¿En que pais naciste?')
SET IDENTITY_INSERT [dbo].[Preguntas] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_comida] ON 

INSERT [dbo].[Tipo_comida] ([id_tipocomida], [nom_tipocomida]) VALUES (1, N'Plato')
INSERT [dbo].[Tipo_comida] ([id_tipocomida], [nom_tipocomida]) VALUES (2, N'Complemento')
INSERT [dbo].[Tipo_comida] ([id_tipocomida], [nom_tipocomida]) VALUES (4, N'Postre')
INSERT [dbo].[Tipo_comida] ([id_tipocomida], [nom_tipocomida]) VALUES (5, N'Bebida')
INSERT [dbo].[Tipo_comida] ([id_tipocomida], [nom_tipocomida]) VALUES (7, N'Bebida Alcohólica')
SET IDENTITY_INSERT [dbo].[Tipo_comida] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_usuario] ON 

INSERT [dbo].[Tipo_usuario] ([id_tipousu], [nombre_tipousu]) VALUES (1, N'Admin')
INSERT [dbo].[Tipo_usuario] ([id_tipousu], [nombre_tipousu]) VALUES (2, N'cliente')
SET IDENTITY_INSERT [dbo].[Tipo_usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id_usuario], [nombre_usuario], [tipo_usu], [id_pregunta], [respuesta_usu], [contrasena]) VALUES (13, N'Admin', 1, 1, N'bruno', 0x02000000706FDD1FDDB7DCAC2302D077F6833264B95B4967995DF414F86C8C42E56B48C9)
INSERT [dbo].[Usuario] ([id_usuario], [nombre_usuario], [tipo_usu], [id_pregunta], [respuesta_usu], [contrasena]) VALUES (14, N'Bryan', 2, 1, N'doggi', 0x02000000C66037E24932C98DF7E7ECFE6AE9F8F1A807FD98B541931CA657920718BEC14B)
INSERT [dbo].[Usuario] ([id_usuario], [nombre_usuario], [tipo_usu], [id_pregunta], [respuesta_usu], [contrasena]) VALUES (16, N'Diego', 2, 4, N'El Salvador', 0x02000000EF61DB4E429B3C8A869460F9BF4E33B54BAB4BC80DE5E2422D2C77BC7816F6F1)
INSERT [dbo].[Usuario] ([id_usuario], [nombre_usuario], [tipo_usu], [id_pregunta], [respuesta_usu], [contrasena]) VALUES (17, N'Usuario2', 2, 1, N'lana', 0x02000000932DAB3ED9CE2BC6631ED35A8AACF244DBFAB58941E813F78CF955C2BA1C5173)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Comida] FOREIGN KEY([comida_car])
REFERENCES [dbo].[Comida] ([id_comida])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Comida]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Factura] FOREIGN KEY([fact_detfact])
REFERENCES [dbo].[Factura] ([id_factura])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Factura]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Usuario] FOREIGN KEY([usuario_car])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Usuario]
GO
ALTER TABLE [dbo].[Comida]  WITH CHECK ADD  CONSTRAINT [FK_Comida_Tipo_comida] FOREIGN KEY([tipo_comida])
REFERENCES [dbo].[Tipo_comida] ([id_tipocomida])
GO
ALTER TABLE [dbo].[Comida] CHECK CONSTRAINT [FK_Comida_Tipo_comida]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Metodo_pago] FOREIGN KEY([metod_fact])
REFERENCES [dbo].[Metodo_pago] ([id_metodo])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Metodo_pago]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Usuario] FOREIGN KEY([usuario_fact])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Usuario]
GO
ALTER TABLE [dbo].[Ingredientes]  WITH CHECK ADD FOREIGN KEY([fk_proveedor])
REFERENCES [dbo].[Proveedor] ([id_proveedor])
GO
ALTER TABLE [dbo].[Mesa]  WITH CHECK ADD  CONSTRAINT [FK_Mesa_Estado_mesa] FOREIGN KEY([idestado_mesa])
REFERENCES [dbo].[Estado_mesa] ([id_estadomesa])
GO
ALTER TABLE [dbo].[Mesa] CHECK CONSTRAINT [FK_Mesa_Estado_mesa]
GO
ALTER TABLE [dbo].[Mesa]  WITH CHECK ADD  CONSTRAINT [FK_Mesa_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Mesa] CHECK CONSTRAINT [FK_Mesa_Usuario]
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD FOREIGN KEY([fk_ingredientes])
REFERENCES [dbo].[Ingredientes] ([id_ingredientes])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Preguntas] FOREIGN KEY([id_pregunta])
REFERENCES [dbo].[Preguntas] ([id_pregunta])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Preguntas]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Tipo_usuario] FOREIGN KEY([tipo_usu])
REFERENCES [dbo].[Tipo_usuario] ([id_tipousu])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Tipo_usuario]
GO
/****** Object:  StoredProcedure [dbo].[sp_buscarComida]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_buscarComida]
@NOMBRE varchar
as
select * from Comida where nom_comida like ''+@NOMBRE+'%'
GO
/****** Object:  StoredProcedure [dbo].[sp_buscarUsuario]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_buscarUsuario]
@NOMBRE varchar(50)
as
select * from Usuario where nombre_usuario like ''+@NOMBRE+'%'
GO
/****** Object:  StoredProcedure [dbo].[sp_eliminarComida]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[sp_eliminarComida] 
@ID int
as
delete from Comida where id_comida = @ID
GO
/****** Object:  StoredProcedure [dbo].[sp_eliminarUsuarios]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_eliminarUsuarios] 
@ID int
as
delete from Usuario where id_usuario = @ID
GO
/****** Object:  StoredProcedure [dbo].[sp_insertarComida]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_insertarComida]
@NOMBRE varchar(50),
@DESCRIP varchar(250),
@TIPO int,
@PRECIO decimal(10,2)
as
insert into Comida values(@NOMBRE,@DESCRIP,@TIPO,@PRECIO)
GO
/****** Object:  StoredProcedure [dbo].[sp_llenarComboComida]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[sp_llenarComboComida]
as
select * from Tipo_comida
GO
/****** Object:  StoredProcedure [dbo].[SP_MASVENDIDO]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_MASVENDIDO]
as
SELECT nom_comida, SUM(cantidad_car) as num_repeticiones FROM V_carrito u 
    GROUP BY nom_comida 
    HAVING COUNT(cantidad_car)>=1 order by num_repeticiones DESC
GO
/****** Object:  StoredProcedure [dbo].[sp_modificarComida]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_modificarComida]
@ID int,
@NOMBRE varchar(50),
@DESCRIP varchar(250),
@TIPO int,
@PRECIO decimal(10,2)
as
Update Comida set nom_comida=@NOMBRE, decrip_comida=@DESCRIP, 
tipo_comida=@TIPO, precio_comid=@PRECIO where id_comida = @ID
GO
/****** Object:  StoredProcedure [dbo].[sp_modificarUsuario]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_modificarUsuario]
@ID int,
@NOMBRE varchar(50)
as
Update Usuario set 
nombre_usuario = @NOMBRE
where id_usuario = @ID
GO
/****** Object:  StoredProcedure [dbo].[sp_mostrarTablas]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_mostrarTablas]
@tabla varchar(50)
as
execute('select * from '+@tabla+'')
GO
/****** Object:  StoredProcedure [dbo].[sp_mostrarUsuarios]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_mostrarUsuarios]  
AS
BEGIN
select
    id_usuario,
    nombre_usuario,
    tipo_usu,
    id_pregunta,
    respuesta_usu,
    CONVERT(VARCHAR,DECRYPTBYPASSPHRASE('Itca123!',contrasena)) contrasena
FROM Usuario

END
GO
/****** Object:  StoredProcedure [dbo].[SP_VENTASTOTALES]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_VENTASTOTALES]
as
select  b.nom_metodo, c.total_fact, c.id_factura,(select nombre_usuario from Usuario where id_usuario = c.usuario_fact ) as NombreUsuario
from Factura c INNER JOIN Metodo_pago b
on  c.metod_fact = b.id_metodo
GO
/****** Object:  StoredProcedure [dbo].[SPFACTURASELECCIONADA]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SPFACTURASELECCIONADA]
@idusuario int,
@idfactura int,
@idmetodo int
as

if @idusuario = 0 AND @idusuario = 0 AND @idmetodo = 0

select b.fact_detfact, d.nom_metodo, c.nom_comida, b.cantidad_car, b.precio_car,(select nombre_usuario from Usuario where id_usuario = b.usuario_car) as NombreUsuario, a.total_fact
from Carrito b  INNER JOIN Comida c 
ON b.comida_car = c.id_comida INNER JOIN Factura a
ON b.fact_detfact = a.id_factura INNER JOIN Metodo_pago d
on  a.metod_fact = d.id_metodo 


else if @idmetodo <> 0

select b.fact_detfact, d.nom_metodo, c.nom_comida, b.cantidad_car, b.precio_car,(select nombre_usuario from Usuario where id_usuario = b.usuario_car) as NombreUsuario, a.total_fact
from Carrito b  INNER JOIN Comida c 
ON b.comida_car = c.id_comida INNER JOIN Factura a
ON b.fact_detfact = a.id_factura INNER JOIN Metodo_pago d
on  a.metod_fact = d.id_metodo 
where a.metod_fact = @idmetodo

else 

select b.fact_detfact, d.nom_metodo, c.nom_comida, b.cantidad_car, b.precio_car,(select nombre_usuario from Usuario where id_usuario = @idusuario) as NombreUsuario, a.total_fact
from Carrito b  INNER JOIN Comida c 
ON b.comida_car = c.id_comida INNER JOIN Factura a
ON b.fact_detfact = a.id_factura INNER JOIN Metodo_pago d
on  a.metod_fact = d.id_metodo
where b.fact_detfact = @idfactura
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARUSUARIO]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPINSERTARUSUARIO]
	@NOMBRE VARCHAR(50),
	@ID_PREGUNTA INT,
	@RESPUESTA_USU VARCHAR(100),
	@CONTRASENA VARCHAR (100)
AS
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARUSUARIOS]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--CREAR PROCEDIMIENTO PARA INSERCION
CREATE PROCEDURE [dbo].[SPINSERTARUSUARIOS]
	@NOMBRE VARCHAR(50),
	@ID_PREGUNTA INT,
	@RESPUESTA_USU VARCHAR(50),
	@CONTRASENA VARCHAR (100),
	@TIPO_USU INT
AS
BEGIN
--DECLARACION DE VARIABLE LOCALES
DECLARE @ERROR INT = 0;
DECLARE @ID INT=0;

--VALIDANDO QUE LOS VALORES DE LOS CAMPOS ESTEN COMPLETOS
IF(((SELECT LEN(@NOMBRE)) = 0)OR(SELECT LEN(@ID_PREGUNTA))=0 OR (SELECT LEN(@RESPUESTA_USU))=0 OR (SELECT LEN(@CONTRASENA))=0 OR (SELECT LEN(@TIPO_USU))=0)
BEGIN
 SET @ERROR = 1;
 END
 END

IF(@ERROR=0)
BEGIN
--PROCESO DE INSERCION
INSERT INTO Usuario(nombre_usuario,id_pregunta,respuesta_usu,tipo_usu,contrasena)
VALUES(@NOMBRE,@ID_PREGUNTA,@RESPUESTA_USU,@TIPO_USU,ENCRYPTBYPASSPHRASE('POE2021!',@CONTRASENA))
SET @ID = SCOPE_IDENTITY();
SELECT @ID
END




GO
/****** Object:  StoredProcedure [dbo].[SPMODIFICARCONTRASENA]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--PROCEDIMIENTO PARA MODIFICAR 
CREATE PROCEDURE [dbo].[SPMODIFICARCONTRASENA]	
@NOMBRE VARCHAR(50),
	@CONTRA VARCHAR(100)
AS
BEGIN
--DECLARACION DE VARIABLE LOCALES
DECLARE @ERROR INT = 0;

--VALIDANDO QUE LOS VALORES DE LOS CAMPOS ESTEN COMPLETOS
IF((SELECT LEN(@CONTRA))=0)
BEGIN
 SET @ERROR = 1;
END

IF(@ERROR=0)
BEGIN
	--PROCESO DE MODIFICACION
	UPDATE Usuario
		SET
		contrasena = ENCRYPTBYPASSPHRASE('POE2021!',@CONTRA)
	WHERE
		nombre_usuario = @NOMBRE
END

END
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARUSUARIOS]    Script Date: 6/11/2021 16:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SPMOSTRARUSUARIOS]
AS
BEGIN
select
    id_usuario,
    nombre_usuario,
    tipo_usu,
    id_pregunta,
    respuesta_usu,
    CONVERT(VARCHAR,DECRYPTBYPASSPHRASE('POE2021!',contrasena)) contrasena
FROM Usuario

END
GO
USE [master]
GO
ALTER DATABASE [foodwish] SET  READ_WRITE 
GO
