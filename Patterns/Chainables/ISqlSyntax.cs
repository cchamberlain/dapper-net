namespace Dapper.Net.Patterns.Chainables {

    /// <summary>
    /// Represents a chainable sql building block
    /// </summary>
    public interface ISqlSyntax {
        string ToSql();
    }

}