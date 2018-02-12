SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[configitem](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[application] [nvarchar](100) NOT NULL CONSTRAINT [DF_configitem_application]  DEFAULT (N'*'),
	[name] [nvarchar](100) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[machine] [nvarchar](100) NOT NULL CONSTRAINT [DF_configitem_machine]  DEFAULT (N''),
	[updated] [datetime] NOT NULL CONSTRAINT [DF_configitem_updated]  DEFAULT (getdate()),
 CONSTRAINT [PK_configitem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [unique_app_name_machine] UNIQUE NONCLUSTERED 
(
	[application] ASC,
	[name] ASC,
	[machine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO