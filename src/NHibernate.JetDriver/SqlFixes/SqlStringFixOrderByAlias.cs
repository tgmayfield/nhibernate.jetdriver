using System;
using System.Text.RegularExpressions;

namespace NHibernate.JetDriver.SqlFixes
{
    /// <summary>Fix Cast function not called.
    ///   example test:  NHibernate.Test.Criteria.CriteriaQueryTest.CloningProjectionsTest
    /// </summary>
    public class SqlStringFixOrderByAlias : SqlStringFix
    {
        /// <summary>
        ///  Regular expression built for C# on: sex, dez 31, 2010, 04:35:12 
        ///  Using Expresso Version: 3.0.3634, http://www.ultrapico.com
        ///  
        ///  A description of the regular expression:
        ///  
        ///  y
        ///  [1]: A numbered capture group. [\d+]
        ///      Any digit, one or more repetitions
        ///  _ 
        ///      _
        ///      Space
        ///  [2]: A numbered capture group. [asc|desc]
        ///      Select from 2 alternatives
        ///          asc
        ///              asc
        ///          desc
        ///              desc
        ///  
        ///
        /// </summary>
        public static Regex _Regex = new Regex(
              "y(\\d+)_ (asc|desc)",
            RegexOptions.IgnoreCase
            | RegexOptions.Singleline
            | RegexOptions.CultureInvariant
            );

        public override string FixSql(string sql)
        {

            if (!_Regex.IsMatch(sql))
            {
                return sql;
            }

            var matches = _Regex.Matches(sql);

            string sqlOrderByParameter;

            foreach (Match match in matches)
            {
                sqlOrderByParameter = match.Value;
                var paramNumber = match.Groups[1].Value;
                var paramAscDesc = match.Groups[2].Value;
                sql = sql.Replace(match.Value, (Convert.ToInt32(paramNumber) + 1).ToString() + " " + paramAscDesc);
            }

            return sql;

        }
    }
}