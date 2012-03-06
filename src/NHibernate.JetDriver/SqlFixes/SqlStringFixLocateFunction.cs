using System;
using System.Text.RegularExpressions;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Change "locate" to "instr" </summary>
    /// <remarks>
    ///    	SQL:LOCATE( stringToFind, stringToSearch  [, start] )
    ///     ACCESS: Instr ( [start], stringToSearch, stringToFind)
    ///     ex test: CharIndexFunction
    /// </remarks>
    public class SqlStringFixLocateFunction : SqlStringFix
    {
        private static readonly Regex RegexLocate = new Regex(
                 "locate\\s*\\((.+?)\\)",
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
                var paramExp = match.Groups[1].Value;
                var args = paramExp.Split(',');
                string sqlInstr = "( instr(" + args[2] + "+1," + args[1] + "," + args[0] + ")-1 ) ";
                sql = sql.Replace(match.Value, sqlInstr);
            }

            return sql;

        }
    }
}