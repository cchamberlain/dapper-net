namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasConditional<out TSyntax> where TSyntax : ISqlSyntax {
        ISqlConditional ConditionalRaw { get; }
        DynamicParameters Param { get; }
        TSyntax And(string clause, object param);
        TSyntax And(ISqlConditional clause);
        TSyntax Or(string clause, object param);
        TSyntax Or(ISqlConditional clause);
    }

}