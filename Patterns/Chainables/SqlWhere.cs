using S = Dapper.Net.Patterns.Syntax.SqlTokens;

namespace Dapper.Net.Patterns.Chainables {

    public class SqlWhere : SqlClause<ISqlWhere>, ISqlWhere { 
        public override string ToSql() => $"{S.Where} {ConditionalRaw.ToSql()}";
        public ISqlConditional ConditionalRaw { get; private set; }
        public DynamicParameters Param { get; }

        public override string ClauseRaw { get; }
        public override ISqlWhere Clause { get; protected set; }

        public SqlWhere(ISqlConditional clause) {
            ConditionalRaw = clause;
        }

        public ISqlWhere And(string clause, object param) => Chain(() => ConditionalRaw = ConditionalRaw.And(clause, param));
        public ISqlWhere And(ISqlConditional clause) => Chain(() => ConditionalRaw.And(clause));

        public ISqlWhere Or(string clause, object param) => Chain(() => ConditionalRaw = ConditionalRaw.Or(clause, param));
        public ISqlWhere Or(ISqlConditional clause) => Chain(() => ConditionalRaw.Or(clause));
    }
}