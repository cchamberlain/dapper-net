namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasValues<out TSyntax> where TSyntax : ISqlSyntax {
        string ValuesRaw { get; }
        TSyntax Values(string clause);
    }

}