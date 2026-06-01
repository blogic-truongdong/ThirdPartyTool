/* =============================================================================
   BLogicSupportToolScriptLogs
   -----------------------------------------------------------------------------
   Audit table for every INSERT / UPDATE / DELETE script the BLogicPOS7 Support
   Tool runs (refund, void, cancel, close, add-tip, service-charge, finish, ...).

   Run this against the BLOGICPOS7 database.
   The script is idempotent — safe to run multiple times.
   ============================================================================= */

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[BLogicSupportToolScriptLogs]') AND type = N'U'
)
BEGIN
    CREATE TABLE [dbo].[BLogicSupportToolScriptLogs]
    (
        [ScriptLogID]   BIGINT IDENTITY(1, 1) NOT NULL,
        [DateCreated]   DATETIME      NOT NULL CONSTRAINT [DF_BLogicSupportToolScriptLogs_DateCreated] DEFAULT (GETDATE()),
        [DatabaseName]  VARCHAR(128)  NULL,    -- BLOGICPOS7 | MERCHANT
        [ScriptText]    NVARCHAR(MAX) NOT NULL,-- the SQL that was executed
        [RowsAffected]  INT           NULL,
        [IsSuccess]     BIT           NULL,
        [ErrorMessage]  NVARCHAR(MAX) NULL,
        CONSTRAINT [PK_BLogicSupportToolScriptLogs] PRIMARY KEY CLUSTERED ([ScriptLogID] ASC)
    );
END
GO

-- Lookups by date (most recent first).
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_BLogicSupportToolScriptLogs_DateCreated'
      AND object_id = OBJECT_ID(N'[dbo].[BLogicSupportToolScriptLogs]')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_BLogicSupportToolScriptLogs_DateCreated]
        ON [dbo].[BLogicSupportToolScriptLogs] ([DateCreated] DESC);
END
GO
