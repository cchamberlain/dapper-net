namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasSet<out TSyntax> where TSyntax : ISqlSyntax {
        ISqlSet SetRaw { get; }
        TSyntax Set(string clause);
        TSyntax Set(ISqlSet clause);
    }

}