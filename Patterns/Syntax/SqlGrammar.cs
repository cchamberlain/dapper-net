using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Dapper.Net.Extensions;
using Dapper.Net.Patterns.Chainables;
using S = Dapper.Net.Patterns.Syntax.SqlTokens;

namespace Dapper.Net.Patterns.Syntax {

    public static class SqlGrammar<T> where T : ISqlSyntax {
        public static string Top(int? top) => top != null ? $"{S.Top}({top})" : null;
        public static string Distinct(bool isDistinct) => isDistinct ? S.Distinct : null;
        public static string NoLock(bool isNoLock) => isNoLock ? $"{S.With} ({S.NoLock})" : null;
        public static string Join(SqlJoinDirection direction) => direction.GetDescription();
        public static string On(ISqlWhere onRaw) => onRaw != null ? $"{S.On} {Clause(onRaw)}" : null;
        public static string Clause(ISqlClause clause) => clause?.ToSql();

        public static string And(T first, T second) => Paren(first, S.And, second);
        public static string Or(T first, T second) => Paren(first, S.Or, second);
        public static string EqualTo(T first, T second) => Paren(first, S.EqualTo, second);
        public static string LessThan(T first, T second) => Paren(first, S.LessThan, second);
        public static string LessThanEqualTo(T first, T second) => Paren(first, S.LessThanEqualTo, second);
        public static string GreaterThan(T first, T second) => Paren(first, S.GreaterThan, second);
        public static string GreaterThanEqualTo(T first, T second) => Paren(first, S.GreaterThanEqualTo, second);

        private static string Paren(T first, string oper, T second) => $"({first.ToSql()} {oper} {second.ToSql()})";

        public static string Comma(IEnumerable<T> items) {
            var q = new Queue<T>(items);
            if (!q.Any()) return null;
            var sb = new StringBuilder(q.Dequeue().ToSql());
            while (q.Any()) {
                sb.Append($",{q.Dequeue().ToSql()}");
            }
            return sb.ToString();
        }

    }

    public enum SqlJoinDirection {
        [Description(S.Inner + " " + S.Join)]
        Inner,

        [Description(S.Left + " " + S.Join)]
        Left,

        [Description(S.Right + " " + S.Join)]
        Right,

        [Description(S.Outer + " " + S.Join)]
        Outer
    }

}