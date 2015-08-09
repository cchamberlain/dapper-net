namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasGroupBy<out TSyntax> where TSyntax : ISqlSyntax {
        string GroupByRaw { get; }
        TSyntax GroupBy(string clause);
    }

}