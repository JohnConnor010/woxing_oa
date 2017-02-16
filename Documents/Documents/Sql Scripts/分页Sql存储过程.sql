
CREATE PROCEDURE [dbo].[SYST_pGetScopeRows] 
  @Sql nvarchar(2000),
  @Top int,
  @Order nvarchar(500),
  @nFrom int,
  @nTo int
AS
BEGIN
  declare @NewSql nvarchar(3000),@NewSqlStat nvarchar(3000),@TopStr nvarchar(100);
  set @NewSql=@Sql;
  set @TopStr='';
  set @NewSql=ltrim(rtrim(@NewSql));
  if(@Top<>0)set @TopStr=' Top '+Cast(@Top as varchar)+' ';
  if (lower(left(@NewSql,6))='select' and charindex('order by',lower(@NewSql))=0)
  Begin
     set @NewSqlStat='select count(*) as RowsCount from ('+@NewSql+') as C;'
     set @NewSql='select '+@TopStr+' row_number() over ('+@Order+') as RowNum,'+right(@NewSql,len(@NewSql)-6);
     set @NewSql='select * from ('+@NewSql+') as T where T.RowNum between '+Cast(@nFrom as varchar)+' and '+Cast(@nTo as varchar)+' '+@Order+';';
     exec(@NewSql+@NewSqlStat);
     --Print @NewSql+@NewSqlStat;
    
  End
  else
     select 'Not_Select_Sql_statement!'
END
GO
CREATE PROCEDURE [dbo].[SYST_pGetSingleRow] 
  @Sql nvarchar(2000),
  @Top int,
  @Order nvarchar(500),
  @nPos int
AS
BEGIN
  exec dbo.SYST_pGetScopeRows @Sql,@Top,@Order,@nPos,@nPos;
END
GO
CREATE PROCEDURE [dbo].[SYST_pGetPageRows] 
  @Sql nvarchar(2000),
  @Top int,
  @Order nvarchar(500),
  @nPageSize int,
  @nPageID int
AS
BEGIN
  declare @nFrom int,@nTo int;
  set @nFrom=(@nPageID-1)*@nPageSize+1;
  set @nTo=@nPageID*@nPageSize;
  exec dbo.SYST_pGetScopeRows @Sql,@Top,@Order,@nFrom,@nTo;
END

GO


