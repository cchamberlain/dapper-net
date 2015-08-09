namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasWhere<out TSyntax> where TSyntax : ISqlSyntax {
        ISqlWhere WhereRaw { get; }
        TSyntax Where(string clause, object template);
        TSyntax Where(ISqlConditional clause);
        TSyntax Where(ISqlWhere clause);
    }

}