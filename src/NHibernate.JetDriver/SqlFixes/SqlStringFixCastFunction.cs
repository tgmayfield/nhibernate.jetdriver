using System;
using System.Text.RegularExpressions;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Fix Cast function not called.
    ///   example test:  NHibernate.Test.Criteria.CriteriaQueryTest.CloningProjectionsTest
    /// </summary>
    public class SqlStringFixCastFunction : SqlStringFix
    {
        /// <summary>
        ///  Regular expression built for C# on: sex, jun 11, 2010, 12:14:08 
        ///  Using Expresso Version: 3.0.3634, http://www.ultrapico.com
        ///  
        ///  A description of the regular expression:
        ///  
        ///  cast\s*\(
        ///      cast
        ///      Whitespace, any number of repetitions
        ///      Literal (
        ///  [1]: A numbered capture group. [.*?]
        ///      Any character, any number of repetitions, as few as possible
        ///  \s+as\s+.*?\)
        ///      Whitespace, one or more repetitions
        ///      as
        ///      Whitespace, one or more repetitions
        ///      Any character, any number of repetitions, as few as possible
        ///      Literal )
        ///  
        ///
        /// </summary>
        public static Regex _regexLocate = new Regex(
              "cast\\s*\\((.*?)\\s+as\\s+.*?\\)",
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

            string sqlCast;

            foreach (Match match in matches)
            {
                sqlCast = match.Value;
                var expressionToCast = match.Groups[1].Value;
                sql = sql.Replace(match.Value, "(" + expressionToCast + ")");
            }

            return sql;

        }
    }
}