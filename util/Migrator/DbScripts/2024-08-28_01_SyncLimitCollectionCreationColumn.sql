-- Sync existing data
UPDATE [dbo].[Organization]
SET [LimitCollectionCreation] = [LimitCollectionCreationDeletion]
GO
