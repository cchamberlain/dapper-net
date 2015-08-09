namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasColumns<out TSyntax> where TSyntax : ISqlSyntax {
        string ColumnsRaw { get; }
        TSyntax Columns(string clause);
    }

}