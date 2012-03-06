using System;
using System.Text.RegularExpressions;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Change "upper" to "ucase" and "lower" to "lcase"
    ///     example test : ExtraLazy   Get 
    ///  </summary>
    public class SqlStringFixUpperLowerFunction : SqlStringFix
    {
        private static readonly Regex RegexLocate = new Regex(
            "(upper|lower)\\s*\\((.+?)\\)",
            RegexOptions.IgnoreCase
                | RegexOptions.Singleline
                    | RegexOptions.CultureInvariant
            );

        public override string FixSql(string sql)
        {
            if (!RegexLocate.IsMatch(sql))
            {
                return sql;
            }

            var matches = RegexLocate.Matches(sql);

            foreach (Match match in matches)
            {
                var sqlToReplace = match.Value;
                sqlToReplace = sqlToReplace.Replace("upper", "ucase");
                sqlToReplace = sqlToReplace.Replace("lower", "lcase");
                sql = sql.Replace(match.Value, sqlToReplace);
            }

            return sql;

        }


    }
}