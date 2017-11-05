select row_number () over (order by u.Level1TypeSortBy, u.Level1Name, u.Level2TypeSortBy, u.Level2NameSortBy) id, u.Level1Type, u.Level1Name, isnull(u.Level2Type, '') Level2Type, isnull(u.Level2Name, '') Level2Name, u.Level1TypeSortBy, u.Level2TypeSortBy, u.Level2NameSortBy
into #names
from
(
	select 'Table' Level1Type, t.name Level1Name, l2.Level2Type, l2.Level2Name, 1 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
	from sys.tables t
	left join
	(
		select 'Column' Level2Type, c.name Level2Name, 2 Level2TypeSortBy, row_number() over (order by c.column_id) Level2NameSortby, t.name Level1Name
		from sys.columns c
		inner join sys.tables t on t.object_id = c.object_id

		union all

		select 'Key' Level2Type, object_name(o.object_id) Level2Name, 3 Level2TypeSortBy, row_number() over (order by case when o.type = 'PK' then 1 when o.type = 'F' then 2 when o.type = 'UQ' then 3 end, object_name(object_id)) Level2NameSortby, object_name(parent_object_id) Level1Name
		from sys.objects o
		where o.type in ('F', 'PK', 'UQ')

		union all

		select 'Constraint' Level2Type, object_name(o.object_id) Level2Name, 4 Level2TypeSortBy, row_number() over (order by case when o.type = 'C' then 1 when o.type = 'D' then 2 end, object_name(object_id)) Level2NameSortby, object_name(parent_object_id) Level1Name
		from sys.objects o
		where o.type in ('D', 'C')

		union all

		select 'Trigger' Level2Type, o.name Level2Name, 5 Level2TypeSortBy, row_number() over (order by o.name) Level2NameSortby, t.name Level1Name
		from sys.triggers o
		inner join sys.tables t on t.object_id = o.parent_id

		union all

		select 'Index' Level2Type, i.name Level2Name, 6 Level2TypeSortBy, row_number() over (order by i.name) Level2NameSortby, t.name Level1Name
		from sys.indexes i
		inner join sys.tables t on t.object_id = i.object_id
		where i.index_id > 0
	) l2 on l2.Level1Name = t.name
	where schema_name(t.schema_id) = '{0}'

	union all

	select 'View' Level1Type, v.name Level1Name, l2.Level2Type, l2.Level2Name, 7 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
	from sys.views v
	left join
	(
		select 'Column' Level2Type, c.name Level2Name, 8 Level2TypeSortBy, row_number() over (order by c.column_id) Level2NameSortby, v.name Level1Name
		from sys.columns c
		inner join sys.views v on v.object_id = c.object_id

		union all

		select 'Trigger' Level2Type, o.name Level2Name, 9 Level2TypeSortBy, row_number() over (order by o.name) Level2NameSortby, v.name Level1Name
		from sys.triggers o
		inner join sys.views v on v.object_id = o.parent_id

		union all

		select 'Index' Level2Type, i.name Level2Name, 10 Level2TypeSortBy, row_number() over (order by i.name) Level2NameSortby, v.name Level1Name
		from sys.indexes i
		inner join sys.views v on v.object_id = i.object_id
		where i.index_id > 0
	) l2 on l2.Level1Name = v.name
	where schema_name(v.schema_id) = '{0}'

	union all

	select 'Procedure' Level1Type, p.name Level1Name, l2.Level2Type, l2.Level2Name, 11 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
	from sys.procedures p
	left join
	(
		select 'Parameter' Level2Type, a.name Level2Name, 12 Level2TypeSortBy, row_number() over (order by a.parameter_id) Level2NameSortby, p.name Level1Name
		from sys.procedures p
		inner join sys.parameters a on a.object_id = p.object_id
	) l2 on l2.Level1Name = p.name
	where schema_name(p.schema_id) = '{0}'

	union all

	select 'Table-valued Function' Level1Type, o.name Level1Name, l2.Level2Type, l2.Level2Name, 13 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
	from sys.objects o
	left join
	(
		select 'Parameter' Level2Type, a.name Level2Name, 14 Level2TypeSortBy, row_number() over (order by a.parameter_id) Level2NameSortby, o.name Level1Name
		from sys.objects o
		inner join sys.parameters a on a.object_id = o.object_id
		where o.type in ('TF', 'IF')
	) l2 on l2.Level1Name = o.name
	where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}'

	union all

	select 'Scalar-valued Function' Level1Type, o.name Level1Name, l2.Level2Type, l2.Level2Name, 15 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
	from sys.objects o
	left join
	(
		select 'Parameter' Level2Type, a.name Level2Name, 16 Level2TypeSortBy, row_number() over (order by a.parameter_id) Level2NameSortby, o.name Level1Name
		from sys.objects o
		inner join sys.parameters a on a.object_id = o.object_id
		where o.type = 'FN' and a.name != ''		
	) l2 on l2.Level1Name = o.name
	where o.type = 'FN' and schema_name(o.schema_id) = '{0}'

	union all

	-- Level1 only
	select l1.Level1Type, l1.Level1Name, '' Level2Type, '' Level2Name, l1.Level1TypeSortBy, -1 Level2TypeSortBy, -1 Level2NameSortBy
	from
	(
		select 'Table' Level1Type, t.name Level1Name, 1 Level1TypeSortBy
		from sys.tables t
		where schema_name(t.schema_id) = '{0}'

		union all

		select 'View' Level1Type, v.name Level1Name, 7 Level1TypeSortBy
		from sys.views v
		where schema_name(v.schema_id) = '{0}'

		union all

		select 'Procedure' Level1Type, p.name Level1Name, 11 Level1TypeSortBy
		from sys.procedures p
		where schema_name(p.schema_id) = '{0}'

		union all

		select 'Table-valued Function' Level1Type, o.name Level1Name, 13 Level1TypeSortBy
		from sys.objects o
		where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}'

		union all

		select 'Scalar-valued Function' Level1Type, o.name Level1Name, 15 Level1TypeSortBy
		from sys.objects o
		where o.type = 'FN' and schema_name(o.schema_id) = '{0}'
	) l1
) u

select u.Level1Type, u.Level1Name, u.Level2Type, u.Level2Name, u.Description, u.DatabaseFieldName, u.Level1TypeSortBy, u.LevelSortBy, u.Level2TypeSortBy, u.Level2NameSortBy
into #descriptions
from
(
	-- All Level1
	select u1.Level1Type, u1.Level1Name, u1.Level2Type, u1.Level2Name, u1.Level1Description Description, u1.Level1DatabaseFieldName DatabaseFieldName, 1 LevelSortBy, u1.Level1TypeSortBy, 0 Level2TypeSortBy, 0 Level2NameSortBy
	from
	(
		select 'Table' Level1Type, t.name Level1Name, '' Level2Type, '' Level2Name, e.value Level1Description, e.name Level1DatabaseFieldName, 1 Level1TypeSortBy
		from sys.tables t
		inner join sys.extended_properties e on e.major_id = t.object_id and minor_id = 0 and e.class = 1
		where schema_name(t.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'View' Level1Type, v.name Level1Name, '' Level2Type, '' Level2Name, e.value Level1Description, e.name Level1DatabaseFieldName, 7 Level1TypeSortBy
		from sys.views v
		inner join sys.extended_properties e on e.major_id = v.object_id and minor_id = 0 and e.class = 1
		where schema_name(v.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'Procedure' Level1Type, p.name Level1Name, '' Level2Type, '' Level2Name, e.value Level1Description, e.name Level1DatabaseFieldName, 11 Level1TypeSortBy
		from sys.procedures p
		inner join sys.extended_properties e on e.major_id = p.object_id and e.class = 1
		where schema_name(p.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'Table-valued Function' Level1Type, o.name Level1Name, '' Level2Type, '' Level2Name, e.value Level1Description, e.name Level1DatabaseFieldName, 13 Level1TypeSortBy
		from sys.objects o
		inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
		where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'

		union all

		select 'Scalar-valued Function' Level1Type, o.name Level1Name, '' Level2Type, '' Level2Name, e.value Level1Description, e.name Level1DatabaseFieldName, 15 Level1TypeSortBy
		from sys.objects o
		inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
		where o.type = 'FN' and schema_name(o.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support'
	) u1

	union all

	-- All Level2
	select u2.Level1Type, u2.Level1Name, u2.Level2Type, u2.Level2Name, u2.Level2Description Description, u2.Level2DatabaseFieldName DatabaseFieldName, 2 LevelSortBy, u2.Level1TypeSortBy, u2.Level2TypeSortBy, u2.Level2NameSortBy
	from
	(
		select 'Table' Level1Type, t.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2Description, l2.Level2DatabaseFieldName, 1 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
		from sys.tables t
		inner join
		(
			select 'Column' Level2Type, c.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 2 Level2TypeSortBy, row_number() over (order by c.column_id) Level2NameSortby, t.name Level1Name
			from sys.columns c
			inner join sys.tables t on t.object_id = c.object_id
			inner join sys.extended_properties e on e.major_id = c.object_id and e.minor_id = c.column_id and e.class = 1
			where schema_name(t.schema_id) = '{0}'

			union all

			select 'Key' Level2Type, object_name(o.object_id) Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 3 Level2TypeSortBy, row_number() over (order by case when o.type = 'PK' then 1 when o.type = 'F' then 2 when o.type = 'UQ' then 3 end, object_name(object_id)) Level2NameSortby, object_name(parent_object_id) Level1Name
			from sys.objects o
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(o.schema_id) = '{0}'
			and o.type in ('F', 'PK', 'UQ')

			union all

			select 'Constraint' Level2Type, object_name(o.object_id) Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 4 Level2TypeSortBy, row_number() over (order by case when o.type = 'C' then 1 when o.type = 'D' then 2 end, object_name(object_id)) Level2NameSortby, object_name(parent_object_id) Level1Name
			from sys.objects o
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(o.schema_id) = '{0}' 
			and o.type in ('D', 'C')

			union all

			select 'Trigger' Level2Type, o.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 5 Level2TypeSortBy, row_number() over (order by o.name) Level2NameSortby, t.name Level1Name
			from sys.triggers o
			inner join sys.tables t on t.object_id = o.parent_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(t.schema_id) = '{0}'

			union all

			select 'Index' Level2Type, i.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 6 Level2TypeSortBy, row_number() over (order by i.name) Level2NameSortby, t.name Level1Name
			from sys.indexes i
			inner join sys.tables t on t.object_id = i.object_id
			inner join sys.extended_properties e on e.major_id = i.object_id and e.minor_id = i.index_id and e.class = 7
			where schema_name(t.schema_id) = '{0}'
			and i.index_id > 0
		) l2 on l2.Level1Name = t.name
		where schema_name(t.schema_id) = '{0}'

		union all

		select 'View' Level1Type, v.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2Description, l2.Level2DatabaseFieldName, 7 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
		from sys.views v
		inner join
		(
			select 'Column' Level2Type, c.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 8 Level2TypeSortBy, row_number() over (order by c.column_id) Level2NameSortby, v.name Level1Name
			from sys.columns c
			inner join sys.views v on v.object_id = c.object_id
			inner join sys.extended_properties e on e.major_id = c.object_id and e.minor_id = c.column_id and e.class = 1
			where schema_name(v.schema_id) = '{0}'

			union all

			select 'Trigger' Level2Type, o.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 9 Level2TypeSortBy, row_number() over (order by o.name) Level2NameSortby, v.name Level1Name
			from sys.triggers o
			inner join sys.views v on v.object_id = o.parent_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.class = 1
			where schema_name(v.schema_id) = '{0}'

			union all

			select 'Index' Level2Type, i.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 10 Level2TypeSortBy, row_number() over (order by i.name) Level2NameSortby, v.name Level1Name
			from sys.indexes i
			inner join sys.views v on v.object_id = i.object_id
			inner join sys.extended_properties e on e.major_id = i.object_id and e.minor_id = i.index_id and e.class = 7
			where schema_name(v.schema_id) = '{0}'
			and i.index_id > 0
		) l2 on l2.Level1Name = v.name
		where schema_name(v.schema_id) = '{0}'

		union all

		select 'Procedure' Level1Type, p.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2Description, l2.Level2DatabaseFieldName, 11 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
		from sys.procedures p
		inner join
		(
			select 'Parameter' Level2Type, a.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 12 Level2TypeSortBy, row_number() over (order by a.parameter_id) Level2NameSortby, p.name Level1Name
			from sys.procedures p
			inner join sys.parameters a on a.object_id = p.object_id
			inner join sys.extended_properties e on e.major_id = p.object_id and e.minor_id = a.parameter_id and e.class = 2
			where schema_name(p.schema_id) = '{0}'
		) l2 on l2.Level1Name = p.name
		where schema_name(p.schema_id) = '{0}'

		union all

		select 'Table-valued Function' Level1Type, o.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2Description, l2.Level2DatabaseFieldName, 13 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
		from sys.objects o
		inner join
		(
			select 'Parameter' Level2Type, a.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 14 Level2TypeSortBy, row_number() over (order by a.parameter_id) Level2NameSortby, o.name Level1Name
			from sys.objects o
			inner join sys.parameters a on a.object_id = o.object_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.minor_id = a.parameter_id and e.class = 2
			where schema_name(o.schema_id) = '{0}'
			and o.type in ('TF', 'IF')
		) l2 on l2.Level1Name = o.name
		where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}'

		union all

		select 'Scalar-valued Function' Level1Type, o.name Level1Name, l2.Level2Type, l2.Level2Name, l2.Level2Description, l2.Level2DatabaseFieldName, 15 Level1TypeSortBy, l2.Level2TypeSortBy, l2.Level2NameSortBy
		from sys.objects o
		inner join
		(
			select 'Parameter' Level2Type, a.name Level2Name, e.value Level2Description, e.name Level2DatabaseFieldName, 16 Level2TypeSortBy, row_number() over (order by a.parameter_id) Level2NameSortby, o.name Level1Name
			from sys.objects o
			inner join sys.parameters a on a.object_id = o.object_id
			inner join sys.extended_properties e on e.major_id = o.object_id and e.minor_id = a.parameter_id and e.class = 2
			where schema_name(o.schema_id) = '{0}'
			and o.type = 'FN' and a.name != ''
		) l2 on l2.Level1Name = o.name
		where o.type = 'FN' and schema_name(o.schema_id) = '{0}'
	) u2
) u
where 1 = {1}
and convert(varchar(max), u.Description) like '{3}'{4}{5}{7}

select n.id, n.Level1Type, n.Level1Name, n.Level2Type, n.Level2Name, convert(bit, isnull(md.id, 0)) | convert(bit, isnull(mn.id, 0)) Match, n.Level1TypeSortBy, n.Level2TypeSortBy, n.Level2NameSortBy
into #result
from #names n
left join
(
	select distinct n.id
	from #names n
	inner join #descriptions d on d.Level1Type = n.Level1Type and d.Level1Name = n.Level1Name and d.Level2Type = n.Level2Type and d.Level2Name = n.Level2Name
) md on md.id = n.id
left join
(
	select n.id
	from #names n
	where 1 = {2}
	and (n.Level1Name like '{3}'{4} and n.Level2Name = '' or n.Level2Name like '{3}'{4}){6}
) mn on mn.id = n.id

select r.id, r.Level1Type, r.Level1Name, r.Level2Type, r.Level2Name, r.Match
from #result r
order by r.Level1TypeSortBy, r.Level1Name, r.Level2TypeSortBy, r.Level2NameSortBy

select count(*) MatchCount
from #result r
where r.Match = 1

drop table #result
drop table #names
drop table #descriptions
