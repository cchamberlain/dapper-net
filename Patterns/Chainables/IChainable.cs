using System;

namespace Dapper.Net.Patterns.Chainables {

    public interface IChainable<out T>
    {
        T Chain(Action action);
        T Get();
    }

}