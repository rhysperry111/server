-- Add Column
IF COL_LENGTH('[dbo].[Organization]', 'LimitCollectionCreation') IS NULL
BEGIN
    ALTER TABLE
        [dbo].[Organization]
    ADD
        [LimitCollectionCreation] BIT NOT NULL CONSTRAINT [DF_Organization_LimitCollectionCreation] DEFAULT (0)
END
GO
