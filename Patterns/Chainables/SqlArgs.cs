namespace Dapper.Net.Patterns.Chainables {
    public class SqlArgs : Args
    {
        private readonly Args _args;

        public SqlArgs(Args args)
        {
            _args = args;
        }

    }
}