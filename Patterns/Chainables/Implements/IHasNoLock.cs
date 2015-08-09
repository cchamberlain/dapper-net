namespace Dapper.Net.Patterns.Chainables.Implements {

    public interface IHasNoLock<out TSyntax> where TSyntax : ISqlSyntax {
        bool NoLockRaw { get; }
        TSyntax NoLock();
    }

}