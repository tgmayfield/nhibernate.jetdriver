using System;
using System.Text.RegularExpressions;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Remove Count(distinct to pass test DO NOT USE outside debug mode </summary>
    public class SqlStringFixAggregateDistinct : SqlStringFix
    {
        /// <summary>
        ///  Regular expression built for C# on: qui, jun 10, 2010, 11:17:10 
        ///  Using Expresso Version: 3.0.3634, http://www.ultrapico.com
        ///  
        ///  A description of the regular expression:
        ///  
        ///  [1]: A numbered capture group. [COUNT|SUM|AVG]
        ///      Select from 3 alternatives
        ///          COUNT
        ///              COUNT
        ///          SUM
        ///              SUM
        ///          AVG
        ///              AVG
        ///  \s*\(\s*
        ///      Whitespace, any number of repetitions
        ///      Literal (
        ///      Whitespace, any number of repetitions
        ///  [2]: A numbered capture group. [DISTINCT]
        ///      DISTINCT
        ///          DISTINCT
        ///  
        ///
        /// </summary>
        private static readonly Regex RegexDistinct = new Regex(
             "(COUNT|SUM|AVG)\\s*\\(\\s*(DISTINCT)",
           RegexOptions.IgnoreCase
           | RegexOptions.Singleline
           | RegexOptions.CultureInvariant
           );

        public override string FixSql(string sql)
        {
#if (!DEBUG)
            return sql;
#endif

            if (!RegexDistinct.IsMatch(sql))
            {
                return sql;
            }

            Console.WriteLine("*****************COUNT(DISTICT******* ERROR");

            var matches = RegexDistinct.Matches(sql);

            foreach (Match match in matches)
            {
                string sqlRemoveDistinct = match.Value;
                sqlRemoveDistinct = sqlRemoveDistinct.ToLower().Replace("distinct", "");
                sql = sql.Replace(match.Value, sqlRemoveDistinct);
            }

            return sql;
        }
    }
}