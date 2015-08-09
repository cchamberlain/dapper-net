namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasFrom<out TSyntax> where TSyntax : ISqlSyntax {
        ISqlFrom FromRaw { get; }
        TSyntax From(string clause);
        TSyntax From(ISqlFrom clause);
    }

}