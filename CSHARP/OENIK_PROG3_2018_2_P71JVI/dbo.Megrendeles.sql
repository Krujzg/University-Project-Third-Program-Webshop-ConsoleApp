CREATE TABLE [dbo].[Megrendeles] (
    [RendelesID]      INT  NOT NULL,
    [Leadasi_idopont] DATE NULL,
    [Hatarido]        DATE NULL,
    [Teljesitesi_ido] INT  NULL,
    [DB_szam]         INT  NULL,
    [VasarloID]       INT  NOT NULL,
    [RuhaID]          INT  NOT NULL,
    CONSTRAINT [PK_RendelesID] PRIMARY KEY CLUSTERED ([RendelesID] ASC),
    CONSTRAINT [FK_MegrendelesXMegrendelo] FOREIGN KEY ([VasarloID]) REFERENCES [dbo].[Megrendelo] ([VasarloID]),
    
);

