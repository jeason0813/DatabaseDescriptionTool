select s.id
from
(
	select row_number () over (order by u.Level1TypeSortBy, u.Level1Name, u.Level2TypeSortBy, u.Level2NameSortBy) id, u.Level1Type, u.Level1Name, isnull(u.Level2Type, '') Level2Type, isnull(u.Level2Name, '') Level2Name
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
) s
where s.Level1Type = '{1}' and s.Level1Name = '{2}' and s.Level2Type = '{3}' and s.Level2Name = '{4}'
