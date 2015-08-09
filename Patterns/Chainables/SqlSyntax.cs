using System;
using Dapper.Net.Patterns.Chainables.Implements;
using Dapper.Net.Patterns.Exceptions;
using Dapper.Net.Patterns.Syntax;
using S = Dapper.Net.Patterns.Syntax.SqlTokens;
using G = Dapper.Net.Patterns.Syntax.SqlGrammar<Dapper.Net.Patterns.Chainables.ISqlSyntax>;

namespace Dapper.Net.Patterns.Chainables {

    public class SqlDelete : SqlStatement<ISqlDelete>, ISqlDelete {

        public ISqlFrom FromRaw { get; private set; }
        public ISqlWhere WhereRaw { get; private set; }
        public ISqlDelete Where(string clause, object template) {
            throw new NotImplementedException();
        }

        public ISqlDelete Where(ISqlConditional clause) {
            throw new NotImplementedException();
        }

        public ISqlDelete From(string clause) => From(new SqlFrom(clause));
        public ISqlDelete From(ISqlFrom clause) => Chain(() => FromRaw = clause);

        public ISqlDelete Where(ISqlWhere clause) {
            throw new NotImplementedException();
        }

        public override string ToSql() {
            throw new NotImplementedException();
        }
    }

    public class SqlInsert : SqlStatement<ISqlInsert>, ISqlInsert {
        public SqlInsert(string table) {
            TableRaw = table;
        }

        public override string ToSql() {
            throw new NotImplementedException();
        }

        public string ColumnsRaw { get; }
        public ISqlInsert Columns(string clause) {
            throw new NotImplementedException();
        }

        public string ValuesRaw { get; }
        public ISqlInsert Values(string clause) {
            throw new NotImplementedException();
        }
    }


    public interface ISqlConditional : ISqlClause, IHasConditional<ISqlConditional> {}

    public class SqlConditional : SqlClause<ISqlConditional>, ISqlConditional {
        public override string ToSql() => ClauseRaw;
        public override sealed string ClauseRaw { get; protected set; }
        public ISqlConditional ConditionalRaw => this;
        public DynamicParameters Param { get; }

        public SqlConditional(string clause, object param = null) {
            ClauseRaw = clause;
            Param = new DynamicParameters(param);
        }

        public ISqlConditional And(string clause, object param = null) => Amend(S.And, clause, param);
        public ISqlConditional And(ISqlConditional clause) => And(clause.ClauseRaw, clause.Param);

        public ISqlConditional Or(string clause, object param = null) => Amend(S.Or, clause, param);
        public ISqlConditional Or(ISqlConditional clause) {
            return Or(clause.ClauseRaw, clause.Param);
        }

        private ISqlConditional Amend(string token, string clause, object param = null) {
            ClauseRaw = $"({ClauseRaw} {token} {clause})";
            Param.AddDynamicParams(param);
            return this;
        }
    }

    public interface ISqlSelect :
        ISqlStatement,
        IHasTop<ISqlSelect>,
        IHasDistinct<ISqlSelect>,
        IHasFrom<ISqlSelect>,
        IHasNoLock<ISqlSelect>,
        IHasWhere<ISqlSelect>,
        IHasGroupBy<ISqlSelect>,
        IHasOrderBy<ISqlSelect> { }



public class SqlSelect : SqlStatement<ISqlSelect>, ISqlSelect {

        public SqlSelect(string columns) {
            ColumnsRaw = columns;
        }

        public string ColumnsRaw { get; }
        public int? TopRaw { get; private set; }
        public bool DistinctRaw { get; private set; }
        public ISqlFrom FromRaw { get; private set; }
        public bool NoLockRaw { get; private set; }
        public ISqlWhere WhereRaw { get; private set; }

        public ISqlSelect Top(int? top) => Chain(() => TopRaw = top);
        public ISqlSelect Distinct() => Chain(() => DistinctRaw = true);
        public ISqlSelect From(string clause) => From(new SqlFrom(clause));
        public ISqlSelect From(ISqlFrom clause) => Chain(() => FromRaw = clause);
        public ISqlSelect NoLock() => Chain(() => NoLockRaw = true);
        public ISqlSelect Where(string clause, object template = null) => Where(new SqlWhere(clause, template));

    public ISqlSelect Where(ISqlConditional clause) {
        throw new NotImplementedException();
    }

    public ISqlSelect Where(ISqlWhere clause) => Chain(() => WhereRaw = clause);
    public string GroupByRaw { get; }
    public ISqlSelect GroupBy(string groupBy) => Chain(() => GroupByRaw.Add(groupBy));
    public string OrderByRaw { get; }
    public ISqlSelect OrderBy(string orderBy) => Chain(() => OrderByRaw.Add(orderBy));

        public override string ToSql() => $"{S.Select} {G.Top(TopRaw)} {G.Distinct(DistinctRaw)} {ColumnsRaw} {G.Clause(FromRaw)} {G.Clause(WhereRaw)}".TrimSqueeze();
    }

    public interface ISqlUpdate : ISqlStatement,
        IHasTop<ISqlUpdate>,
        IHasSet<ISqlUpdate>,
        IHasFrom<ISqlUpdate>,
        IHasWhere<ISqlUpdate> {}

    public class SqlUpdate : SqlStatement<ISqlUpdate>, ISqlUpdate {
        public override string ToSql() => $"{S.Update} {G.Top(TopRaw)} {TableRaw} {G.Clause(SetRaw)} {G.Clause(FromRaw)} {G.Clause(WhereRaw)}".TrimSqueeze();
        public string TableRaw { get; }
        public int? TopRaw { get; set; }
        public ISqlSet SetRaw { get; private set; }
        public ISqlFrom FromRaw { get; private set; }
        public ISqlWhere WhereRaw { get; private set; }

        public SqlUpdate(string table) {
            TableRaw = table;
        }

        public ISqlUpdate Top(int? top) => Chain(() => TopRaw = top);
        public ISqlUpdate Set(string clause) => Set(new SqlSet(clause));
        public ISqlUpdate Set(ISqlSet clause) => Chain(() => SetRaw = clause);
        public ISqlUpdate From(string clause) => From(new SqlFrom(clause));
        public ISqlUpdate From(ISqlFrom clause) => Chain(() => FromRaw = clause);
        public ISqlUpdate Where(string clause, object template = null) => Where(new SqlConditional(clause, template));
        public ISqlUpdate Where(ISqlConditional clause) => Where(new SqlWhere(clause));
        public ISqlUpdate Where(ISqlWhere clause) => Chain(() => WhereRaw = clause);
    }

    public interface ISqlInsert : ISqlStatement,
        IHasColumns<ISqlInsert>,
        IHasValues<ISqlInsert> {}

    public interface ISqlDelete : ISqlStatement,
        IHasFrom<ISqlDelete>,
        IHasWhere<ISqlDelete> {}

    // Stretch
    public interface ISqlMerge : ISqlStatement { }
    public interface ISqlBulkInsert : ISqlStatement { }
    public interface ISqlReadText : ISqlStatement { }
    public interface ISqlUpdateText : ISqlStatement { }
    public interface ISqlWriteText : ISqlStatement { }



    public interface ISqlFrom : ISqlClause
    {
        string Table { get; }
        string Alias { get; }
        bool NoLockRaw { get; }
        ISqlJoin JoinRaw { get; }
    }

    // Immediate
    public interface IJoinable<out T> where T : ISqlFrom
    {
        T NoLock();
        T InnerJoin(string clause);
        T LeftJoin(string clause);
        T RightJoin(string clause);
        T OuterJoin(string clause);
        T Join(ISqlFrom clause, SqlJoinDirection direction);
    }

    public class SqlFrom : SqlClause<ISqlFrom>, ISqlFrom, IJoinable<ISqlFrom> {
        public override string ClauseRaw { get; }
        public string Table { get; private set; }
        public string Alias { get; private set; }
        public bool NoLockRaw { get; private set; }
        public ISqlJoin JoinRaw { get; private set; }

        public SqlFrom(string clause) {
            ClauseRaw = clause;
            Parse();
        }

        private void Parse() {
            var split = ClauseRaw.TrimSqueeze().Split(' ');
            if (split.Length < 1 || split.Length > 2) throw new SqlPatternException(SqlPatternException.SqlPatternExceptionType.CouldNotParseFromClause);
            Table = split[0];
            Alias = split.Length == 2 ? split[1] : Table;
        }

        public ISqlFrom NoLock() => Chain(() => NoLockRaw = true);

        public ISqlFrom InnerJoin(string clause) => New(clause, SqlJoinDirection.Inner);
        public ISqlFrom LeftJoin(string clause) => New(clause, SqlJoinDirection.Left);
        public ISqlFrom RightJoin(string clause) => New(clause, SqlJoinDirection.Right);
        public ISqlFrom OuterJoin(string clause) => New(clause, SqlJoinDirection.Outer);

        private ISqlFrom New(string clause, SqlJoinDirection direction) => Join(new SqlFrom(clause), direction);
        public ISqlFrom Join(ISqlFrom clause, SqlJoinDirection direction) => Chain(() => JoinRaw = new SqlJoin(clause, direction));

        public override string ToSql() => $"{S.From} {Table} [{Alias}] {G.Clause(JoinRaw)}".TrimSqueeze();
    }

    public interface ISqlJoin : ISqlFrom {
        SqlJoinDirection Direction { get; }
        ISqlFrom FromRaw { get; }
        ISqlWhere OnRaw { get; }

        ISqlJoin On(string clause, object template);
        ISqlJoin On(ISqlWhere clause);
    }

    public class SqlJoin : SqlClause<ISqlJoin>, ISqlJoin, IJoinable<ISqlJoin> {

        public ISqlFrom FromRaw { get; }
        public SqlJoinDirection Direction { get; }
        public override string ClauseRaw => FromRaw.ClauseRaw;
        public string Table => FromRaw.Table;
        public string Alias => FromRaw.Alias;
        public bool NoLockRaw { get; private set; }
        public ISqlWhere OnRaw { get; private set; }
        public ISqlJoin JoinRaw { get; private set; }

        public SqlJoin(ISqlFrom clause, SqlJoinDirection direction = SqlJoinDirection.Inner) {
            FromRaw = clause;
            Direction = direction;
            NoLockRaw = FromRaw.NoLockRaw;
        }

        public ISqlJoin NoLock() => Chain(() => NoLockRaw = true);
        public ISqlJoin On(string clause, object template) => On(new SqlWhere(clause, template));
        public ISqlJoin On(ISqlWhere clause) => Chain(() => OnRaw = clause);
        public ISqlJoin InnerJoin(string clause) => New(clause, SqlJoinDirection.Inner);
        public ISqlJoin LeftJoin(string clause) => New(clause, SqlJoinDirection.Left);
        public ISqlJoin RightJoin(string clause) => New(clause, SqlJoinDirection.Right);
        public ISqlJoin OuterJoin(string clause) => New(clause, SqlJoinDirection.Outer);

        private ISqlJoin New(string clause, SqlJoinDirection direction) => Join(new SqlFrom(clause), direction);
        public ISqlJoin Join(ISqlFrom clause, SqlJoinDirection direction) => Chain(() => JoinRaw = new SqlJoin(clause, direction));

        public override string ToSql() => $"{G.Join(Direction)} {Table} [{Alias}] {G.On(OnRaw)} {G.Clause(JoinRaw)}".TrimSqueeze();
    }

    public interface ISqlOn : ISqlClause { 
    }
    public class SqlOn : SqlClause<ISqlOn>, ISqlOn {
        public ISqlWhere WhereRaw { get; }
        public override string ClauseRaw => ToSql();

        public SqlOn(ISqlWhere clause) {
            WhereRaw = clause;
        }

        public override string ToSql() => $"{S.On} {G.Clause(WhereRaw)}";
    }

    public interface ISqlSet : ISqlClause { 
    }

    public class SqlSet : SqlClause<ISqlSet>, ISqlSet
    {
        public override string ClauseRaw { get; }

        public SqlSet(string clause) {
            ClauseRaw = clause;
        }

        public override string ToSql() => ClauseRaw.TrimSqueeze();
    }


    public interface ISqlWhere : ISqlClause, IHasConditional<ISqlWhere> {}

    public interface ISqlTop : ISqlClause { }
    public interface ISqlSearchCondition : ISqlClause { }
    public interface ISqlHints : ISqlClause { }

    // Stretch
    public interface ISqlOption : ISqlClause { }
    public interface ISqlOutput : ISqlClause { }
    public interface ISqlTableValueConstructor : ISqlClause { }
    public interface ISqlWith : ISqlClause { }

}
