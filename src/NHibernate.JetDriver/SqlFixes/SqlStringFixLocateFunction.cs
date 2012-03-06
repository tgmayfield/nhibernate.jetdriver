using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
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
        private static Regex _regexLocate = new Regex(
                 "locate\\s*\\((.+?)\\)",
             RegexOptions.IgnoreCase
             | RegexOptions.Singleline
             | RegexOptions.CultureInvariant
             );

        public override string FixSql(string sql)
        {
            if (!_regexLocate.IsMatch(sql))
            {
                return sql;
            }

            var matches = _regexLocate.Matches(sql);

            string sqlLocate;
            string sqlInstr;

            foreach (Match match in matches)
            {
                sqlLocate = match.Value;
                var paramExp = match.Groups[1].Value;
                var args = paramExp.Split(',');
                sqlInstr = "( instr(" + args[2] + "+1," + args[1] + "," + args[0] + ")-1 ) ";
                sql = sql.Replace(match.Value, sqlInstr);
            }

            return sql;

        }
    }
}