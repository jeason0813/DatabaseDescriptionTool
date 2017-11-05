declare @sql nvarchar(max)

select @sql = 'select distinct t.name
from
(
' + stuff((select '

	union all

	select s.name collate database_default name
	from ' + quotename(d.name) + '.sys.schemas s'
from sys.databases d
where d.name not in ('master', 'tempdb', 'model', 'msdb')
and d.state_desc = 'ONLINE'
for xml path(''), type).value('.', 'nvarchar(max)'), 1, 18, '')

set @sql = @sql + '
) t
order by t.name
'

exec (@sql)
