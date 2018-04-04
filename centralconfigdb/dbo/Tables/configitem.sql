CREATE TABLE [dbo].[configitem] (
    [id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [application] NVARCHAR (100) CONSTRAINT [DF_configitem_application] DEFAULT (N'*') NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [value]       NVARCHAR (MAX) NOT NULL,
    [machine]     NVARCHAR (100) CONSTRAINT [DF_configitem_machine] DEFAULT (N'') NOT NULL,
    [updated]     DATETIME       CONSTRAINT [DF_configitem_updated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_configitem] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [unique_app_name_machine] UNIQUE NONCLUSTERED ([application] ASC, [name] ASC, [machine] ASC)
);

