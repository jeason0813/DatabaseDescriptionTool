select u.Level1Type, u.Level1Name, u.Level2Type, u.Level2Name, u.DatabaseFieldName
from
(
	-- All Level1
	select u1.Level1Type, u1.Level1Name, u1.Level2Type, u1.Level2Name, u1.Level1DatabaseFieldName DatabaseFieldName
	from
	(
		select 'Table' Level1Type, t.name Level1Name, '' Level2Type, '' Level2Name, e.name Level1DatabaseFieldName
		from sys.tables t
		inner join sys.extended_properties e on e.major_id = t.object_id and minor_id = 0 and e.class = 1
		where schema_name(t.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'View' Level1Type, v.name Level1Name, '' Level2Type, '' Level2Name, e.name Level1DatabaseFieldName
		from sys.views v
		inner join sys.extended_properties e on e.major_id = v.object_id and minor_id = 0 and e.class = 1
		where schema_name(v.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'Procedure' Level1Type, p.name Level1Name, '' Level2Type, '' Level2Name, e.name Level1DatabaseFieldName
		from sys.procedures p
		inner join sys.extended_properties e on e.major_id = p.object_id and e.class = 1
		where schema_name(p.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'Table-valued Function' Level1Type, o.name Level1Name, '' Level2Type, '' Level2Name, e.name Level1DatabaseFieldName
		from sys.objects o
		inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
		where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'Scalar-valued Function' Level1Type, o.name Level1Name, '' Level2Type, '' Level2Name, e.name Level1DatabaseFieldName
		from sys.objects o
		inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
		where o.type = 'FN' and schema_name(o.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'
	) u1

	union all

	-- All Level2
	select u2.Level1Type, u2.Level1Name, u2.Level2Type, u2.Level2Name, u2.Level2DatabaseFieldName DatabaseFieldName
	from
	(
		select 'Table' Level1Type, t.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2DatabaseFieldName
		from sys.tables t
		inner join
		(
			select 'Column' Level2Type, c.name Level2Name, e.name Level2DatabaseFieldName, t.name Level1Name
			from sys.columns c
			inner join sys.tables t on t.object_id = c.object_id
			inner join sys.extended_properties e on e.major_id = c.object_id and e.minor_id = c.column_id and e.class = 1
			where schema_name(t.schema_id) = '{0}'

			union all

			select 'Key' Level2Type, object_name(o.object_id) Level2Name, e.name Level2DatabaseFieldName, object_name(parent_object_id) Level1Name
			from sys.objects o
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(o.schema_id) = '{0}'
			and o.type in ('F', 'PK', 'UQ')

			union all

			select 'Constraint' Level2Type, object_name(o.object_id) Level2Name, e.name Level2DatabaseFieldName, object_name(parent_object_id) Level1Name
			from sys.objects o
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(o.schema_id) = '{0}' 
			and o.type in ('D', 'C')

			union all

			select 'Trigger' Level2Type, o.name Level2Name, e.name Level2DatabaseFieldName, t.name Level1Name
			from sys.triggers o
			inner join sys.tables t on t.object_id = o.parent_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(t.schema_id) = '{0}'

			union all

			select 'Index' Level2Type, i.name Level2Name, e.name Level2DatabaseFieldName, t.name Level1Name
			from sys.indexes i
			inner join sys.tables t on t.object_id = i.object_id
			inner join sys.extended_properties e on e.major_id = i.object_id and e.minor_id = i.index_id and e.class = 7
			where schema_name(t.schema_id) = '{0}'
			and i.index_id > 0
		) l2 on l2.Level1Name = t.name
		where schema_name(t.schema_id) = '{0}'

		union all

		select 'View' Level1Type, v.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2DatabaseFieldName
		from sys.views v
		inner join
		(
			select 'Column' Level2Type, c.name Level2Name, e.name Level2DatabaseFieldName, v.name Level1Name
			from sys.columns c
			inner join sys.views v on v.object_id = c.object_id
			inner join sys.extended_properties e on e.major_id = c.object_id and e.minor_id = c.column_id and e.class = 1
			where schema_name(v.schema_id) = '{0}'

			union all

			select 'Trigger' Level2Type, o.name Level2Name, e.name Level2DatabaseFieldName, v.name Level1Name
			from sys.triggers o
			inner join sys.views v on v.object_id = o.parent_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(v.schema_id) = '{0}'

			union all

			select 'Index' Level2Type, i.name Level2Name, e.name Level2DatabaseFieldName, v.name Level1Name
			from sys.indexes i
			inner join sys.views v on v.object_id = i.object_id
			inner join sys.extended_properties e on e.major_id = i.object_id and e.minor_id = i.index_id and e.class = 7
			where schema_name(v.schema_id) = '{0}'
			and i.index_id > 0
		) l2 on l2.Level1Name = v.name
		where schema_name(v.schema_id) = '{0}'

		union all

		select 'Procedure' Level1Type, p.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2DatabaseFieldName
		from sys.procedures p
		inner join
		(
			select 'Parameter' Level2Type, a.name Level2Name, e.name Level2DatabaseFieldName, p.name Level1Name
			from sys.procedures p
			inner join sys.parameters a on a.object_id = p.object_id
			inner join sys.extended_properties e on e.major_id = p.object_id and e.minor_id = a.parameter_id and e.class = 2
			where schema_name(p.schema_id) = '{0}'
		) l2 on l2.Level1Name = p.name
		where schema_name(p.schema_id) = '{0}'

		union all

		select 'Table-valued Function' Level1Type, o.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2DatabaseFieldName
		from sys.objects o
		inner join
		(
			select 'Parameter' Level2Type, a.name Level2Name, e.name Level2DatabaseFieldName, o.name Level1Name
			from sys.objects o
			inner join sys.parameters a on a.object_id = o.object_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.minor_id = a.parameter_id and e.class = 2
			where schema_name(o.schema_id) = '{0}'
			and o.type in ('TF', 'IF')
		) l2 on l2.Level1Name = o.name
		where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}'

		union all

		select 'Scalar-valued Function' Level1Type, o.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2DatabaseFieldName
		from sys.objects o
		inner join
		(
			select 'Parameter' Level2Type, a.name Level2Name, e.name Level2DatabaseFieldName, o.name Level1Name
			from sys.objects o
			inner join sys.parameters a on a.object_id = o.object_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.minor_id = a.parameter_id and e.class = 2
			where schema_name(o.schema_id) = '{0}'
			and o.type = 'FN' and a.name != ''
		) l2 on l2.Level1Name = o.name
		where o.type = 'FN' and schema_name(o.schema_id) = '{0}'
	) u2
) u
