using System;
using System.Text.RegularExpressions;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Change "extract(month from " to "month(" 
    ///          ex test: AggregateWithBeforeYearFunction 
    /// </summary>
    public class SqlStringFixExtract : SqlStringFix
    {

        private static string[] _searchPatterns ={
                               "extract\\s*\\(year\\s+from",
                               "extract\\s*\\(month\\s+from",
                               "extract\\s*\\(day\\s+from",
                               "extract\\s*\\(hour\\s+from",
                               "extract\\s*\\(minute\\s+from",
                               "extract\\s*\\(second\\s+from"
                                         };
        private static string[] _searchReplacements ={
                               " year(",
                               " month(",
                               "day(",
                               "hour(",
                               "minute(",
                               "second("
                                         };
        private static Regex[] _regExpressions = null;


        public override string FixSql(string sql)
        {

            if (_regExpressions == null)
            {
                _regExpressions = new Regex[_searchPatterns.Length];

                for (int i = 0; i < _searchPatterns.Length; i++)
                {
                    _regExpressions[i] = new Regex(_searchPatterns[i],
                    RegexOptions.IgnoreCase
                    | RegexOptions.Singleline
                    | RegexOptions.CultureInvariant
                    );

                }
            }

            for (int i = 0; i < _regExpressions.Length; i++)
            {
                if (_regExpressions[i].IsMatch((string)sql))
                {
                    sql = _regExpressions[i].Replace(sql, _searchReplacements[i]);
                }

            }

            return sql;
        }
    }
}