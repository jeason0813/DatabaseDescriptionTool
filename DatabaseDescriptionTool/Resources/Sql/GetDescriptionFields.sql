select e.name, count(e.name) [count]
from sys.extended_properties e
where isnull(e.name, '') != 'microsoft_database_tools_support' and e.value != ''
group by e.name
order by e.name
