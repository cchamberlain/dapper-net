namespace Dapper.Net.Patterns.Chainables
{
    public class SqlArgsMap
    {
        public SqlArgs Columns { get; }
        public SqlArgs Values { get; }

        public SqlArgsMap(ArgsMap argsMap)
        {
            Columns = new SqlArgs(argsMap.Columns);
            Values = new SqlArgs(Values);
        }
    }
}
