namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasDistinct<out TSyntax> where TSyntax : ISqlSyntax {
        bool DistinctRaw { get; }
        TSyntax Distinct();
    }

}