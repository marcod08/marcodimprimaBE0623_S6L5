# Query per creare le tabelle
## Tabella Admin
USE [Hotel]
GO

/****** Object:  Table [dbo].[Admin]    Script Date: 08/03/2024 19:35:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](12) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
## Tabelle Camera
USE [Hotel]
GO

/****** Object:  Table [dbo].[Camera]    Script Date: 08/03/2024 19:38:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Camera](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descr] [nvarchar](50) NOT NULL,
	[Tipologia] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Camera] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
## Tabelle Cliente

USE [Hotel]
GO

/****** Object:  Table [dbo].[Cliente]    Script Date: 08/03/2024 19:38:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cognome] [nvarchar](50) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[CodFisc] [nvarchar](16) NOT NULL,
	[Città] [nvarchar](50) NOT NULL,
	[Provincia] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
## Tabelle Prenotazione
USE [Hotel]
GO

/****** Object:  Table [dbo].[Prenotazione]    Script Date: 08/03/2024 19:39:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Prenotazione](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[IdCamera] [int] NOT NULL,
	[DataPrenotazione] [date] NOT NULL,
	[Anno] [nvarchar](4) NOT NULL,
	[SoggiornoInizio] [date] NOT NULL,
	[SoggiornoFine] [date] NOT NULL,
	[Caparra] [money] NOT NULL,
	[Tariffa] [money] NOT NULL,
	[TipoSoggiorno] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Prenotazione] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Prenotazione]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazione_Camera] FOREIGN KEY([IdCamera])
REFERENCES [dbo].[Camera] ([Id])
GO

ALTER TABLE [dbo].[Prenotazione] CHECK CONSTRAINT [FK_Prenotazione_Camera]
GO

ALTER TABLE [dbo].[Prenotazione]  WITH CHECK ADD  CONSTRAINT [FK_Prenotazione_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([Id])
GO

ALTER TABLE [dbo].[Prenotazione] CHECK CONSTRAINT [FK_Prenotazione_Cliente]
GO

## Tabelle Servizio

USE [Hotel]
GO

/****** Object:  Table [dbo].[Servizio]    Script Date: 08/03/2024 19:42:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Servizio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPrenotazione] [int] NOT NULL,
	[Descrizione] [nvarchar](50) NOT NULL,
	[Data] [date] NOT NULL,
	[Quantità] [int] NOT NULL,
	[Prezzo] [money] NOT NULL,
 CONSTRAINT [PK_Servizio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Servizio]  WITH CHECK ADD  CONSTRAINT [FK_Servizio_Prenotazione] FOREIGN KEY([IdPrenotazione])
REFERENCES [dbo].[Prenotazione] ([Id])
GO

ALTER TABLE [dbo].[Servizio] CHECK CONSTRAINT [FK_Servizio_Prenotazione]
GO

