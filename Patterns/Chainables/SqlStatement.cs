using System.Collections.Generic;

namespace Dapper.Net.Patterns.Chainables {

    public abstract class SqlStatement<T> : ChainableSql<T>, ISqlStatement where T : ISqlStatement {
        private readonly ISqlParser _parser;
        public object Param { get; }

        protected SqlStatement(ISqlParser parser = null) {
            _parser = parser ?? new SqlParser();
        }

        public IEnumerable<string> GetErrors(string sql = null) => _parser.ParseErrors(sql ?? ToSql());

        public static ISqlSelect Select(string columns) => new SqlSelect(columns);
        public static ISqlUpdate Update(string table) => new SqlUpdate(table);
        public static ISqlInsert Insert(string table) => new SqlInsert(table);
        public static ISqlDelete Delete() => new SqlDelete();
    }

}