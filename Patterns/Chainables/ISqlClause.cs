namespace Dapper.Net.Patterns.Chainables {

    /// <summary>
    /// Top level interface to represent SQL clauses
    /// </summary>
    public interface ISqlClause : ISqlSyntax {
        string ClauseRaw { get; }
    }

}