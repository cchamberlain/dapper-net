namespace Dapper.Net.Patterns.Chainables {

    public abstract class SqlClause<T> : ChainableSql<T>, ISqlClause where T : ISqlClause {
        public abstract string ClauseRaw { get; }
        public virtual T Clause => (T)(ISqlClause)this;
    }

}