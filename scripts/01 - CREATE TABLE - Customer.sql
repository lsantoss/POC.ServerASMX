USE [ServerASMX] 

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Customer') 
BEGIN
    CREATE TABLE [dbo].[Customer] (
        [Id] [bigint] IDENTITY(1,1) NOT NULL,
        [Name] [nvarchar](100) NOT NULL,
        [Birth] [smalldatetime] NOT NULL,
        [Gender] [tinyint] NOT NULL,
        [CashBalance] [decimal](18, 2) NOT NULL,
        [Active] [tinyint] NOT NULL,
        [CreationDate] [smalldatetime] NOT NULL,
        [ChangeDate] [smalldatetime] NULL,
        CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
    (
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END