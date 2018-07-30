
ALTER TABLE  [dbo].[MN_ACCOUNT_TYPE]
ADD [type] [SMALLINT] NOT NULL
CONSTRAINT DF_MN_ACCOUNT_TYPE_type DEFAULT (0)
WITH VALUES
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'0: tiền mặt, 1: thẻ ghi nợ, 2: thẻ tín dụng, 3: Khoản vay, 4: Khoản cho vay, 5: Sổ tiết kiệm' , 
@level0type=N'SCHEMA',
@level0name=N'dbo', 
@level1type=N'TABLE',
@level1name=N'MN_ACCOUNT_TYPE', 
@level2type=N'COLUMN',
@level2name=N'type'
GO
