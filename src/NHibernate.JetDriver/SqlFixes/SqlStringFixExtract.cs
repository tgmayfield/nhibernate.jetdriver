using System;
using System.Text.RegularExpressions;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Change "extract(month from " to "month(" 
    ///          ex test: AggregateWithBeforeYearFunction 
    /// </summary>
    public class SqlStringFixExtract : SqlStringFix
    {

        private static readonly string[] SearchPatterns ={
                               "extract\\s*\\(year\\s+from",
                               "extract\\s*\\(month\\s+from",
                               "extract\\s*\\(day\\s+from",
                               "extract\\s*\\(hour\\s+from",
                               "extract\\s*\\(minute\\s+from",
                               "extract\\s*\\(second\\s+from"
                                         };
        private static readonly string[] SearchReplacements ={
                               " year(",
                               " month(",
                               "day(",
                               "hour(",
                               "minute(",
                               "second("
                                         };
        private static Regex[] _regExpressions;


        public override string FixSql(string sql)
        {

            if (_regExpressions == null)
            {
                _regExpressions = new Regex[SearchPatterns.Length];

                for (int i = 0; i < SearchPatterns.Length; i++)
                {
                    _regExpressions[i] = new Regex(SearchPatterns[i],
                    RegexOptions.IgnoreCase
                    | RegexOptions.Singleline
                    | RegexOptions.CultureInvariant
                    );

                }
            }

            for (int i = 0; i < _regExpressions.Length; i++)
            {
                if (_regExpressions[i].IsMatch(sql))
                {
                    sql = _regExpressions[i].Replace(sql, SearchReplacements[i]);
                }

            }

            return sql;
        }
    }
}