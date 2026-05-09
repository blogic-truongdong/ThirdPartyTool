IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE name = 'SaleReceiptThirdPartyMappings')
BEGIN
    CREATE TABLE SaleReceiptThirdPartyMappings
    (
        SaleReceiptID uniqueidentifier PRIMARY KEY,
        SaleReceiptID_7Shifts uniqueidentifier NULL,
    );
END
GO